using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Data.Sqlite;
using Wahlregistrierung.Models;

namespace Wahlregistrierung.Services
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService()
        {
            AppPaths.EnsureDirectories();
            connectionString = $"Data Source={AppPaths.DatabaseFilePath}";
        }

        public void InitializeDatabase()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Elections (
                    ElectionId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    IsActive INTEGER NOT NULL DEFAULT 1
                );

                CREATE TABLE IF NOT EXISTS Voters (
                    ElectionId INTEGER NOT NULL,
                    IdNumber TEXT NOT NULL,
                    Name TEXT NOT NULL,
                    PRIMARY KEY (ElectionId, IdNumber),
                    FOREIGN KEY (ElectionId) REFERENCES Elections(ElectionId)
                );

                CREATE TABLE IF NOT EXISTS Votes (
                    VoteId INTEGER PRIMARY KEY AUTOINCREMENT,
                    ElectionId INTEGER NOT NULL,
                    IdNumber TEXT NOT NULL,
                    VoteTime TEXT NOT NULL,
                    UNIQUE (ElectionId, IdNumber),
                    FOREIGN KEY (ElectionId) REFERENCES Elections(ElectionId)
                );

                CREATE TABLE IF NOT EXISTS ScanLog (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ElectionId INTEGER NULL,
                    ScanTime TEXT NOT NULL,
                    InputValue TEXT NOT NULL,
                    Result TEXT NOT NULL,
                    Note TEXT
                );
            ";
            command.ExecuteNonQuery();
        }

        public ElectionInfo? GetActiveElection()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT ElectionId, Name, CreatedAt, IsActive
                FROM Elections
                WHERE IsActive = 1
                ORDER BY ElectionId DESC
                LIMIT 1;";

            using var reader = command.ExecuteReader();
            if (!reader.Read())
                return null;

            return new ElectionInfo
            {
                ElectionId = reader.GetInt32(0),
                Name = reader.GetString(1),
                CreatedAt = reader.GetString(2),
                IsActive = reader.GetInt32(3) == 1
            };
        }

        public bool HasActiveElection() => GetActiveElection() != null;

        public int StartNewElectionFromCsv(string csvFilePath, string electionName)
        {
            if (string.IsNullOrWhiteSpace(csvFilePath))
                throw new ArgumentException("CSV-Dateipfad fehlt.");
            if (!File.Exists(csvFilePath))
                throw new FileNotFoundException("CSV-Datei wurde nicht gefunden.", csvFilePath);

            List<Voter> voters = ReadAndValidateCsv(csvFilePath);

            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var deactivateCommand = connection.CreateCommand();
            deactivateCommand.Transaction = transaction;
            deactivateCommand.CommandText = "UPDATE Elections SET IsActive = 0 WHERE IsActive = 1;";
            deactivateCommand.ExecuteNonQuery();

            var createElectionCommand = connection.CreateCommand();
            createElectionCommand.Transaction = transaction;
            createElectionCommand.CommandText = @"
                INSERT INTO Elections (Name, CreatedAt, IsActive)
                VALUES ($name, $createdAt, 1);
                SELECT last_insert_rowid();";
            createElectionCommand.Parameters.AddWithValue("$name", electionName);
            createElectionCommand.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int electionId = Convert.ToInt32(createElectionCommand.ExecuteScalar());

            foreach (var voter in voters)
            {
                var insertVoterCommand = connection.CreateCommand();
                insertVoterCommand.Transaction = transaction;
                insertVoterCommand.CommandText = @"
                    INSERT INTO Voters (ElectionId, IdNumber, Name)
                    VALUES ($electionId, $id, $name);";
                insertVoterCommand.Parameters.AddWithValue("$electionId", electionId);
                insertVoterCommand.Parameters.AddWithValue("$id", voter.IdNumber);
                insertVoterCommand.Parameters.AddWithValue("$name", voter.Name);
                insertVoterCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return voters.Count;
        }

        private List<Voter> ReadAndValidateCsv(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath, Encoding.UTF8);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header?.Trim() ?? "",
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim
            };

            using var csv = new CsvReader(reader, config);
            List<Voter> records;
            try
            {
                records = csv.GetRecords<Voter>().ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("CSV-Datei konnte nicht korrekt gelesen werden. Bitte Format prüfen.", ex);
            }

            if (records.Count == 0)
                throw new InvalidOperationException("Die CSV-Datei enthält keine Teilnehmerdaten.");

            var seenIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var validated = new List<Voter>();
            int rowNumber = 1;

            foreach (var voter in records)
            {
                rowNumber++;
                string id = voter.IdNumber?.Trim() ?? "";
                string name = voter.Name?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(id))
                    throw new InvalidOperationException($"CSV-Fehler in Zeile {rowNumber}: IdNumber ist leer.");
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidOperationException($"CSV-Fehler in Zeile {rowNumber}: Name ist leer.");
                if (!id.All(char.IsDigit))
                    throw new InvalidOperationException($"CSV-Fehler in Zeile {rowNumber}: IdNumber darf nur Ziffern enthalten.");
                if (!seenIds.Add(id))
                    throw new InvalidOperationException($"CSV-Fehler in Zeile {rowNumber}: Doppelte IdNumber '{id}'.");

                validated.Add(new Voter { IdNumber = id, Name = name });
            }

            return validated;
        }

        public int GetActiveVoterCount()
        {
            var election = GetActiveElection();
            if (election == null) return 0;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Voters WHERE ElectionId = $electionId;";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public int GetActiveVoteCount()
        {
            var election = GetActiveElection();
            if (election == null) return 0;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Votes WHERE ElectionId = $electionId;";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public string? GetActiveVoterNameById(string idNumber)
        {
            var election = GetActiveElection();
            if (election == null) return null;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT Name FROM Voters WHERE ElectionId = $electionId AND IdNumber = $id;";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            command.Parameters.AddWithValue("$id", idNumber);
            var result = command.ExecuteScalar();
            return result?.ToString();
        }

        public bool HasAlreadyVoted(string idNumber)
        {
            var election = GetActiveElection();
            if (election == null) return false;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM Votes WHERE ElectionId = $electionId AND IdNumber = $id;";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            command.Parameters.AddWithValue("$id", idNumber);
            long count = Convert.ToInt64(command.ExecuteScalar());
            return count > 0;
        }

        public void RegisterVote(string idNumber)
        {
            var election = GetActiveElection() ?? throw new InvalidOperationException("Keine aktive Wahl vorhanden.");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Votes (ElectionId, IdNumber, VoteTime)
                VALUES ($electionId, $id, $voteTime);";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            command.Parameters.AddWithValue("$id", idNumber);
            command.Parameters.AddWithValue("$voteTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            command.ExecuteNonQuery();
        }

        public VoteInfo? GetLastVote()
        {
            var election = GetActiveElection();
            if (election == null) return null;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Votes.VoteId, Votes.IdNumber, Voters.Name, Votes.VoteTime
                FROM Votes
                INNER JOIN Voters
                    ON Votes.ElectionId = Voters.ElectionId
                   AND Votes.IdNumber = Voters.IdNumber
                WHERE Votes.ElectionId = $electionId
                ORDER BY Votes.VoteId DESC
                LIMIT 1;";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            return new VoteInfo
            {
                VoteId = reader.GetInt32(0),
                IdNumber = reader.GetString(1),
                Name = reader.GetString(2),
                VoteTime = reader.GetString(3)
            };
        }

        public bool UndoLastVote()
        {
            var lastVote = GetLastVote();
            if (lastVote == null) return false;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Votes WHERE VoteId = $voteId;";
            command.Parameters.AddWithValue("$voteId", lastVote.VoteId);
            return command.ExecuteNonQuery() > 0;
        }

        public void LogScan(string inputValue, string result, string note = "")
        {
            var election = GetActiveElection();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO ScanLog (ElectionId, ScanTime, InputValue, Result, Note)
                VALUES ($electionId, $scanTime, $inputValue, $result, $note);";
            if (election == null) command.Parameters.AddWithValue("$electionId", DBNull.Value);
            else command.Parameters.AddWithValue("$electionId", election.ElectionId);
            command.Parameters.AddWithValue("$scanTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("$inputValue", inputValue);
            command.Parameters.AddWithValue("$result", result);
            command.Parameters.AddWithValue("$note", note);
            command.ExecuteNonQuery();
        }

        public List<ScanLogEntry> GetActiveScanLog()
        {
            var election = GetActiveElection();
            var result = new List<ScanLogEntry>();
            if (election == null) return result;
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT ScanTime, InputValue, Result, COALESCE(Note, '')
                FROM ScanLog
                WHERE ElectionId = $electionId
                ORDER BY Id DESC;";
            command.Parameters.AddWithValue("$electionId", election.ElectionId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new ScanLogEntry
                {
                    ScanTime = reader.GetString(0),
                    InputValue = reader.GetString(1),
                    Result = reader.GetString(2),
                    Note = reader.GetString(3)
                });
            }
            return result;
        }

        public ExportResult ExportActiveElectionData(string folderPath)
        {
            var election = GetActiveElection() ?? throw new InvalidOperationException("Keine aktive Wahl vorhanden.");
            Directory.CreateDirectory(folderPath);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string votesFileName = $"votes_{timestamp}.csv";
            string scanLogFileName = $"scanlog_{timestamp}.csv";
            string votesFilePath = Path.Combine(folderPath, votesFileName);
            string scanLogFilePath = Path.Combine(folderPath, scanLogFileName);
            ExportVotesToCsv(election.ElectionId, votesFilePath);
            ExportScanLogToCsv(election.ElectionId, scanLogFilePath);
            string votesHash = WriteSha256File(votesFilePath);
            string scanLogHash = WriteSha256File(scanLogFilePath);
            return new ExportResult
            {
                FolderPath = folderPath,
                VotesFileName = Path.GetFileName(votesFilePath),
                ScanLogFileName = Path.GetFileName(scanLogFilePath),
                VotesHashFileName = Path.GetFileName(votesHash),
                ScanLogHashFileName = Path.GetFileName(scanLogHash)
            };
        }

        public void CreateBackupSnapshot()
        {
            var election = GetActiveElection();
            if (election == null) return;
            string folder = Path.Combine(AppPaths.BackupDirectory, $"Election_{election.ElectionId}");
            Directory.CreateDirectory(folder);
            string votesFilePath = Path.Combine(folder, "votes_current.csv");
            string scanLogFilePath = Path.Combine(folder, "scanlog_current.csv");
            ExportVotesToCsv(election.ElectionId, votesFilePath);
            ExportScanLogToCsv(election.ElectionId, scanLogFilePath);
            WriteSha256File(votesFilePath);
            WriteSha256File(scanLogFilePath);
        }

        private void ExportVotesToCsv(int electionId, string filePath)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Voters.IdNumber, Voters.Name, Votes.VoteTime
                FROM Votes
                INNER JOIN Voters
                    ON Votes.ElectionId = Voters.ElectionId
                   AND Votes.IdNumber = Voters.IdNumber
                WHERE Votes.ElectionId = $electionId
                ORDER BY Votes.VoteId ASC;";
            command.Parameters.AddWithValue("$electionId", electionId);
            using var reader = command.ExecuteReader();
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.WriteLine("IdNumber,Name,VoteTime");
            while (reader.Read())
            {
                string idNumber = EscapeCsv(reader.GetString(0));
                string name = EscapeCsv(reader.GetString(1));
                string voteTime = EscapeCsv(reader.GetString(2));
                writer.WriteLine($"{idNumber},{name},{voteTime}");
            }
        }

        private void ExportScanLogToCsv(int electionId, string filePath)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT ScanTime, InputValue, Result, COALESCE(Note, '')
                FROM ScanLog
                WHERE ElectionId = $electionId
                ORDER BY Id ASC;";
            command.Parameters.AddWithValue("$electionId", electionId);
            using var reader = command.ExecuteReader();
            using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.WriteLine("ScanTime,InputValue,Result,Note");
            while (reader.Read())
            {
                string scanTime = EscapeCsv(reader.GetString(0));
                string inputValue = EscapeCsv(reader.GetString(1));
                string result = EscapeCsv(reader.GetString(2));
                string note = EscapeCsv(reader.GetString(3));
                writer.WriteLine($"{scanTime},{inputValue},{result},{note}");
            }
        }

        private string WriteSha256File(string filePath)
        {
            using var sha = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            byte[] hash = sha.ComputeHash(stream);
            string hashHex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            string hashFilePath = filePath + ".sha256";
            File.WriteAllText(hashFilePath, hashHex);
            return hashFilePath;
        }

        private string EscapeCsv(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}
