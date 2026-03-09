using System;
using System.IO;

namespace Wahlregistrierung
{
    public static class AppPaths
    {
        public static string BaseDirectory => AppContext.BaseDirectory;
        public static string DataDirectory => Path.Combine(BaseDirectory, "Data");
        public static string ExportDirectory => Path.Combine(BaseDirectory, "Exports");
        public static string BackupDirectory => Path.Combine(BaseDirectory, "Backups");
        public static string TessdataDirectory => Path.Combine(BaseDirectory, "tessdata");
        public static string DatabaseFilePath => Path.Combine(DataDirectory, "voting.db");

        public static void EnsureDirectories()
        {
            Directory.CreateDirectory(DataDirectory);
            Directory.CreateDirectory(ExportDirectory);
            Directory.CreateDirectory(BackupDirectory);
            Directory.CreateDirectory(TessdataDirectory);
        }
    }
}
