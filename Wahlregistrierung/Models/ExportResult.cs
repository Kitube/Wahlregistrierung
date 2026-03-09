namespace Wahlregistrierung.Models
{
    public class ExportResult
    {
        public string FolderPath { get; set; } = "";
        public string VotesFileName { get; set; } = "";
        public string ScanLogFileName { get; set; } = "";
        public string VotesHashFileName { get; set; } = "";
        public string ScanLogHashFileName { get; set; } = "";
    }
}
