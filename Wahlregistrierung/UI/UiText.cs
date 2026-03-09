namespace Wahlregistrierung.UI
{
    public static class UiText
    {
        public const string AppTitle = "Wahlregistrierung";
        public const string CsvLoad = "CSV laden";
        public const string NewElection = "Neue Wahl";
        public const string StartCamera = "Kamera starten";
        public const string StopCamera = "Kamera stoppen";
        public const string Scan = "Scannen";
        public const string RegisterVote = "Registrieren";
        public const string UndoVote = "Rückgängig";
        public const string Export = "Export";
        public const string ScanLog = "Scan-Log";
        public const string Settings = "Einstellungen";
        public const string Quit = "Beenden";
        public const string Info = "Info";
        public const string MenuFile = "Datei";
        public const string MenuScan = "Scan";
        public const string MenuActions = "Aktionen";
        public const string MenuSettings = "Einstellungen";
        public const string MenuHelp = "Hilfe";
        public const string MenuCameraMode = "Kamera-Modus";
        public const string CameraOff = "Aus";
        public const string CameraManual = "Manuell (Leertaste)";
        public const string CameraAuto = "Automatisch";
        public const string Options = "Optionen...";
        public const string NoActiveElection = "Keine aktive Wahl";
        public const string ActiveElectionPrefix = "Aktive Wahl: ";
        public const string NamePlaceholder = "Name wird hier angezeigt";
        public const string ParticipantCountPrefix = "Teilnehmer: ";
        public const string VoteCountPrefix = "Abgegebene Stimmen: ";
        public const string LastVotePrefix = "Letzte Stimme: ";
        public const string StatusLoadCsv = "Bitte CSV laden oder neue Wahl starten.";
        public const string StatusElectionLoaded = "Aktive Wahl geladen.";
        public const string StatusCameraStopped = "Kamera gestoppt.";
        public const string StatusScanReady = "Bereit – Leertaste zum Scannen.";
        public const string StatusScanning = "Scanne...";
        public const string StatusScanCancelled = "Scan abgebrochen.";
        public const string StatusVoteRegistered = "Stimme registriert.";
        public const string StatusAlreadyVoted = "Bereits abgestimmt.";
        public const string StatusIdNotFound = "ID nicht gefunden.";
        public const string StatusInvalidInput = "Ungültige Eingabe.";
        public const string StatusDigitsOnly = "Nur Ziffern sind erlaubt.";
        public const string StatusInputReset = "Eingabe zurückgesetzt.";
        public const string StatusExportOk = "Export erfolgreich abgeschlossen.";
        public const string StatusExportFailed = "Export fehlgeschlagen.";
        public const string StatusCsvLoadFailed = "Fehler beim Laden der CSV.";
        public const string StatusNewElectionFailed = "Fehler beim Start der neuen Wahl.";
        public const string StatusVoteFailed = "Fehler bei der Stimmregistrierung.";
        public const string StatusUndoOk = "Letzte Stimme wurde zurückgenommen.";
        public const string StatusUndoFailed = "Die letzte Stimme konnte nicht zurückgenommen werden.";
        public const string StatusNoVoteToUndo = "Keine Stimme zum Zurücknehmen vorhanden.";
        public const string StatusNoActiveScanLog = "Kein aktiver Scan-Log vorhanden.";
        public const string StatusNoExportWithoutElection = "Kein Export möglich, da keine aktive Wahl vorhanden ist.";
        public const string StatusCameraDisabled = "Kamera ist deaktiviert.";
        public const string StatusCameraRequired = "Bitte zuerst die Kamera starten.";
        public const string StatusCameraStartFailed = "Kamera konnte nicht gestartet werden.";
        public const string StatusManualScanOnly = "Manueller Scan aktiv – Leertaste drücken.";
        public const string StatusAutoScanActive = "Automatische Erkennung aktiv.";
        public const string StatusSettingsPlaceholder = "Einstellungsdialog noch nicht implementiert.";
    }
}
