using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Wahlregistrierung.Config;
using Wahlregistrierung.Enums;
using Wahlregistrierung.Models;
using Wahlregistrierung.Services;
using Wahlregistrierung.UI;

namespace Wahlregistrierung
{
    public partial class MainForm : Form
    {
        private readonly DatabaseService databaseService = new DatabaseService();
        private readonly CameraService cameraService = new CameraService();
        private readonly OcrService ocrService = new OcrService();
        private readonly Config.AppSettings settings = Config.AppSettings.CreateDefault();

        private readonly System.Windows.Forms.Timer cameraTimer = new System.Windows.Forms.Timer();
        private bool isCameraRunning = false;
        private bool isScanning = false;
        private string lastDetectedId = "";
        private int consecutiveMatches = 0;
        private DateTime cooldownUntil = DateTime.MinValue;
        private int ocrFrameSkipCounter = 0;
        private const int OcrEveryNFrames = 3;

        public MainForm()
        {
            InitializeComponent();
            picCameraPreview.SizeMode = PictureBoxSizeMode.Zoom;
            KeyPreview = true;
            AcceptButton = btnRegisterVote;
            Text = UiText.AppTitle;

            databaseService.InitializeDatabase();

            InitializeCameraTimer();
            ApplyUiText();
            RefreshUiState(startup: true);
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            RefreshUiState(startup: true);
        }

        private void InitializeCameraTimer()
        {
            cameraTimer.Interval = settings.ScanIntervalMs;
            cameraTimer.Tick += CameraTimer_Tick;
        }

        private void ApplyUiText()
        {
            lblName.Text = UiText.NamePlaceholder;
            lblElectionName.Text = UiText.NoActiveElection;
            lblParticipantCount.Text = UiText.ParticipantCountPrefix + "0";
            lblVoteCount.Text = UiText.VoteCountPrefix + "0";
            lblLastVote.Text = UiText.LastVotePrefix + "-";

            btnRegisterVote.Text = UiText.RegisterVote;

            tsbImportCsv.Text = UiText.CsvLoad;
            tsbNewElection.Text = UiText.NewElection;
            tsbStartCamera.Text = UiText.StartCamera;
            tsbStopCamera.Text = UiText.StopCamera;
            tsbScanId.Text = UiText.Scan;
            tsbRegisterVote.Text = UiText.RegisterVote;
            tsbUndoLastVote.Text = UiText.UndoVote;
            tsbExportCsv.Text = UiText.Export;
            tsbViewLog.Text = UiText.ScanLog;
            tsbSettings.Text = UiText.Settings;

            miFile.Text = UiText.MenuFile;
            miImportCsv.Text = UiText.CsvLoad;
            miNewElection.Text = UiText.NewElection;
            miExportCsv.Text = UiText.Export;
            miQuit.Text = UiText.Quit;

            miScan.Text = UiText.MenuScan;
            miStartCamera.Text = UiText.StartCamera;
            miStopCamera.Text = UiText.StopCamera;
            miStartScan.Text = UiText.Scan;
            miCancelScan.Text = "Scannen abbrechen";

            miActions.Text = UiText.MenuActions;
            miRegisterVote.Text = UiText.RegisterVote;
            miUndoLastVote.Text = UiText.UndoVote;
            miViewLog.Text = UiText.ScanLog;

            miSettingsMenu.Text = UiText.MenuSettings;
            miCameraMode.Text = UiText.MenuCameraMode;
            miCameraOff.Text = UiText.CameraOff;
            miCameraManual.Text = UiText.CameraManual;
            miCameraAuto.Text = UiText.CameraAuto;
            miOptions.Text = UiText.Options;

            miHelp.Text = UiText.MenuHelp;
            miInfo.Text = UiText.Info;
        }

        private void RefreshUiState(bool startup = false)
        {
            ElectionInfo? election = databaseService.GetActiveElection();
            bool hasElection = election != null;

            if (hasElection)
                lblElectionName.Text = UiText.ActiveElectionPrefix + GetDisplayElectionName(election!.Name);
            else
                lblElectionName.Text = UiText.NoActiveElection;

            lblParticipantCount.Text = UiText.ParticipantCountPrefix + databaseService.GetActiveVoterCount();
            lblVoteCount.Text = UiText.VoteCountPrefix + databaseService.GetActiveVoteCount();

            UpdateLastVoteDisplay();
            UpdateCameraModeMenu();
            UpdateCommandStates(hasElection);

            if (hasElection)
            {
                if (startup)
                    SetStatus(UiText.StatusElectionLoaded, Color.DarkBlue);
                else if (settings.CameraMode == CameraScanMode.Automatic && isCameraRunning)
                    SetStatus(UiText.StatusAutoScanActive, Color.DarkBlue);
                else if (settings.CameraMode == CameraScanMode.ManualTrigger && isCameraRunning)
                    SetStatus(UiText.StatusScanReady, Color.DarkBlue);
            }
            else
            {
                lblName.Text = UiText.NamePlaceholder;
                SetStatus(UiText.StatusLoadCsv, Color.Black);
            }

            txtIdInput.Clear();
            txtIdInput.Focus();
        }

        private void UpdateCommandStates(bool hasElection)
        {
            bool canUseCamera = hasElection && settings.CameraMode != CameraScanMode.Off;

            btnRegisterVote.Enabled = hasElection;
            txtIdInput.Enabled = hasElection;

            tsbImportCsv.Enabled = !hasElection && !isScanning;
            miImportCsv.Enabled = !hasElection && !isScanning;

            tsbNewElection.Enabled = !isScanning;
            miNewElection.Enabled = !isScanning;

            tsbStartCamera.Enabled = canUseCamera && !isCameraRunning;
            miStartCamera.Enabled = canUseCamera && !isCameraRunning;

            tsbStopCamera.Enabled = isCameraRunning;
            miStopCamera.Enabled = isCameraRunning;

            bool canManualScan = hasElection && isCameraRunning && settings.CameraMode == CameraScanMode.ManualTrigger && !isScanning && DateTime.Now >= cooldownUntil;
            tsbScanId.Enabled = canManualScan;
            miStartScan.Enabled = canManualScan;
            miCancelScan.Enabled = isScanning;

            tsbRegisterVote.Enabled = hasElection && !isScanning;
            miRegisterVote.Enabled = hasElection && !isScanning;

            bool canUndo = hasElection && !isScanning && databaseService.GetActiveVoteCount() > 0;
            tsbUndoLastVote.Enabled = canUndo;
            miUndoLastVote.Enabled = canUndo;

            bool canExport = hasElection && !isScanning;
            tsbExportCsv.Enabled = canExport;
            miExportCsv.Enabled = canExport;

            bool canViewLog = hasElection && !isScanning;
            tsbViewLog.Enabled = canViewLog;
            miViewLog.Enabled = canViewLog;

            tsbSettings.Enabled = true;
            miOptions.Enabled = true;
        }

        private void UpdateCameraModeMenu()
        {
            miCameraOff.Checked = settings.CameraMode == CameraScanMode.Off;
            miCameraManual.Checked = settings.CameraMode == CameraScanMode.ManualTrigger;
            miCameraAuto.Checked = settings.CameraMode == CameraScanMode.Automatic;
        }

        private void UpdateLastVoteDisplay()
        {
            VoteInfo? lastVote = databaseService.GetLastVote();
            lblLastVote.Text = lastVote == null
                ? UiText.LastVotePrefix + "-"
                : $"{UiText.LastVotePrefix}{lastVote.Name} ({lastVote.VoteTime})";
        }

        private void SetStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        private string BuildElectionName(string csvPath)
        {
            string baseName = System.IO.Path.GetFileNameWithoutExtension(csvPath).Trim();
            if (string.IsNullOrWhiteSpace(baseName))
                baseName = "Wahl";
            return $"{baseName}_{DateTime.Now:yyyyMMdd_HHmm}";
        }

        private string GetDisplayElectionName(string electionName)
        {
            int lastUnderscore = electionName.LastIndexOf('_');
            if (lastUnderscore > 0)
            {
                string possibleTimestamp = electionName[(lastUnderscore + 1)..];
                if (possibleTimestamp.All(char.IsDigit))
                    electionName = electionName[..lastUnderscore];
            }
            return electionName.Replace("_", " ");
        }

        private bool TrySelectCsvFile(out string filePath)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "CSV-Dateien (*.csv)|*.csv";
            dialog.Title = "Teilnehmerliste auswählen";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                filePath = "";
                return false;
            }
            filePath = dialog.FileName;
            return true;
        }

        private void btnImportCsv_Click(object? sender, EventArgs e)
        {
            if (databaseService.HasActiveElection())
            {
                MessageBox.Show("Es ist bereits eine aktive Wahl vorhanden. Zum Zurücksetzen bitte 'Neue Wahl' verwenden.", "Aktive Wahl vorhanden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!TrySelectCsvFile(out string csvFilePath))
                return;
            try
            {
                int importedCount = databaseService.StartNewElectionFromCsv(csvFilePath, BuildElectionName(csvFilePath));
                SetStatus($"{importedCount} Teilnehmer geladen. Wahl gestartet.", Color.DarkBlue);
                RefreshUiState();
            }
            catch (Exception ex)
            {
                SetStatus(UiText.StatusCsvLoadFailed, Color.DarkRed);
                MessageBox.Show("Fehler beim Laden: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewElection_Click(object? sender, EventArgs e)
        {
            if (!TrySelectCsvFile(out string csvFilePath))
                return;
            if (databaseService.HasActiveElection())
            {
                var result = MessageBox.Show("Eine aktive Wahl ist bereits vorhanden. Beim Start einer neuen Wahl wird die bisherige aktive Wahl archiviert.\n\nFortfahren?", UiText.NewElection, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;
            }
            try
            {
                int importedCount = databaseService.StartNewElectionFromCsv(csvFilePath, BuildElectionName(csvFilePath));
                SetStatus($"{importedCount} Teilnehmer geladen. Neue Wahl gestartet.", Color.DarkBlue);
                RefreshUiState();
            }
            catch (Exception ex)
            {
                SetStatus(UiText.StatusNewElectionFailed, Color.DarkRed);
                MessageBox.Show("Fehler: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegisterVote_Click(object? sender, EventArgs e)
        {
            string inputId = txtIdInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(inputId))
            {
                lblName.Text = UiText.NamePlaceholder;
                SetStatus(UiText.StatusInvalidInput, Color.DarkRed);
                databaseService.LogScan(inputId, "INVALID", "Leere Eingabe");
                txtIdInput.Focus();
                return;
            }
            if (!inputId.All(char.IsDigit))
            {
                lblName.Text = UiText.NamePlaceholder;
                SetStatus(UiText.StatusDigitsOnly, Color.DarkRed);
                databaseService.LogScan(inputId, "INVALID", "Nicht numerische Eingabe");
                txtIdInput.SelectAll();
                txtIdInput.Focus();
                return;
            }

            string? voterName = databaseService.GetActiveVoterNameById(inputId);
            if (voterName == null)
            {
                lblName.Text = UiText.NamePlaceholder;
                SetStatus(UiText.StatusIdNotFound, Color.DarkRed);
                databaseService.LogScan(inputId, "NOT_FOUND");
                txtIdInput.SelectAll();
                txtIdInput.Focus();
                return;
            }

            lblName.Text = voterName;
            if (databaseService.HasAlreadyVoted(inputId))
            {
                SetStatus(UiText.StatusAlreadyVoted, Color.DarkOrange);
                databaseService.LogScan(inputId, "DUPLICATE", voterName);
                txtIdInput.Clear();
                txtIdInput.Focus();
                return;
            }

            try
            {
                databaseService.RegisterVote(inputId);
                databaseService.LogScan(inputId, "SUCCESS", voterName);
                databaseService.CreateBackupSnapshot();
                SetStatus(UiText.StatusVoteRegistered, Color.DarkGreen);
                lblVoteCount.Text = UiText.VoteCountPrefix + databaseService.GetActiveVoteCount();
                lblParticipantCount.Text = UiText.ParticipantCountPrefix + databaseService.GetActiveVoterCount();
                UpdateLastVoteDisplay();
                UpdateCommandStates(databaseService.HasActiveElection());
                txtIdInput.Clear();
                txtIdInput.Focus();
            }
            catch (Exception ex)
            {
                SetStatus(UiText.StatusVoteFailed, Color.DarkRed);
                MessageBox.Show("Fehler: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUndoLastVote_Click(object? sender, EventArgs e)
        {
            VoteInfo? lastVote = databaseService.GetLastVote();
            if (lastVote == null)
            {
                SetStatus(UiText.StatusNoVoteToUndo, Color.DarkRed);
                return;
            }
            var confirm = MessageBox.Show($"Letzte Stimme zurücknehmen?\n\n{lastVote.Name}\n{lastVote.VoteTime}", UiText.UndoVote, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;
            bool success = databaseService.UndoLastVote();
            if (!success)
            {
                SetStatus(UiText.StatusUndoFailed, Color.DarkRed);
                return;
            }
            databaseService.LogScan(lastVote.IdNumber, "UNDO", lastVote.Name);
            databaseService.CreateBackupSnapshot();
            SetStatus(UiText.StatusUndoOk, Color.DarkBlue);
            lblName.Text = lastVote.Name;
            lblVoteCount.Text = UiText.VoteCountPrefix + databaseService.GetActiveVoteCount();
            UpdateLastVoteDisplay();
            UpdateCommandStates(databaseService.HasActiveElection());
            txtIdInput.Clear();
            txtIdInput.Focus();
        }

        private void btnViewLog_Click(object? sender, EventArgs e)
        {
            if (!databaseService.HasActiveElection())
            {
                SetStatus(UiText.StatusNoActiveScanLog, Color.DarkRed);
                return;
            }
            using var logForm = new ScanLogForm(databaseService.GetActiveScanLog());
            logForm.ShowDialog(this);
        }

        private void btnExportCSV_Click(object? sender, EventArgs e)
        {
            if (!databaseService.HasActiveElection())
            {
                SetStatus(UiText.StatusNoExportWithoutElection, Color.DarkRed);
                return;
            }
            using var folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Export-Ordner auswählen";
            folderDialog.SelectedPath = AppPaths.ExportDirectory;
            if (folderDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                ExportResult export = databaseService.ExportActiveElectionData(folderDialog.SelectedPath);
                SetStatus(UiText.StatusExportOk, Color.DarkGreen);
                MessageBox.Show($"Export erfolgreich.\n\nOrdner:\n{export.FolderPath}\n\nDateien:\n{export.VotesFileName}\n{export.ScanLogFileName}\n{export.VotesHashFileName}\n{export.ScanLogHashFileName}", UiText.Export, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetStatus(UiText.StatusExportFailed, Color.DarkRed);
                MessageBox.Show("Fehler beim Export: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartCamera_Click(object? sender, EventArgs e)
        {
            if (settings.CameraMode == CameraScanMode.Off)
            {
                SetStatus(UiText.StatusCameraDisabled, Color.DarkRed);
                return;
            }
            bool started = cameraService.StartCamera(settings.CameraIndex);
            if (!started)
            {
                SetStatus(UiText.StatusCameraStartFailed, Color.DarkRed);
                MessageBox.Show(UiText.StatusCameraStartFailed, "Kamera", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            isCameraRunning = true;
            cameraTimer.Start();
            SetStatus(settings.CameraMode == CameraScanMode.Automatic ? UiText.StatusAutoScanActive : UiText.StatusScanReady, Color.DarkBlue);
            UpdateCommandStates(databaseService.HasActiveElection());
        }

        private void btnStopCamera_Click(object? sender, EventArgs e)
        {
            StopCameraAndScanning();
            SetStatus(UiText.StatusCameraStopped, Color.Black);
        }

        private void btnScanId_Click(object? sender, EventArgs e)
        {
            if (!databaseService.HasActiveElection())
            {
                SetStatus(UiText.StatusLoadCsv, Color.DarkRed);
                return;
            }
            if (!isCameraRunning)
            {
                SetStatus(UiText.StatusCameraRequired, Color.DarkRed);
                return;
            }
            if (settings.CameraMode != CameraScanMode.ManualTrigger)
            {
                SetStatus(UiText.StatusManualScanOnly, Color.DarkBlue);
                return;
            }
            if (DateTime.Now < cooldownUntil)
                return;
            StartScanningSession();
        }

        private void btnSettings_Click(object? sender, EventArgs e)
        {
            SetStatus(UiText.StatusSettingsPlaceholder, Color.DarkBlue);
            MessageBox.Show(UiText.StatusSettingsPlaceholder, UiText.Settings, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void miCameraOff_Click(object? sender, EventArgs e)
        {
            settings.CameraMode = CameraScanMode.Off;
            StopCameraAndScanning();
            UpdateCameraModeMenu();
            UpdateCommandStates(databaseService.HasActiveElection());
            SetStatus(UiText.StatusCameraDisabled, Color.Black);
        }

        private void miCameraManual_Click(object? sender, EventArgs e)
        {
            settings.CameraMode = CameraScanMode.ManualTrigger;
            UpdateCameraModeMenu();
            UpdateCommandStates(databaseService.HasActiveElection());
            SetStatus(UiText.StatusManualScanOnly, Color.DarkBlue);
        }

        private void miCameraAuto_Click(object? sender, EventArgs e)
        {
            settings.CameraMode = CameraScanMode.Automatic;
            UpdateCameraModeMenu();
            UpdateCommandStates(databaseService.HasActiveElection());
            if (isCameraRunning)
                SetStatus(UiText.StatusAutoScanActive, Color.DarkBlue);
        }

        private void miCancelScan_Click(object? sender, EventArgs e)
        {
            CancelScanningSession();
        }

        private void miInfo_Click(object? sender, EventArgs e)
        {
            MessageBox.Show($"{UiText.AppTitle}\n\nInterne Wahlregistrierung mit SQLite, CSV-Import und OCR-Scan.", UiText.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtIdInput_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && settings.CameraMode == CameraScanMode.ManualTrigger)
            {
                btnScanId_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                btnNewElection_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.F5 && tsbExportCsv.Enabled)
            {
                btnExportCSV_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.F6 && tsbViewLog.Enabled)
            {
                btnViewLog_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.F8 && tsbUndoLastVote.Enabled)
            {
                btnUndoLastVote_Click(this, EventArgs.Empty);
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (isScanning) CancelScanningSession();
                else
                {
                    txtIdInput.Clear();
                    lblName.Text = UiText.NamePlaceholder;
                    SetStatus(UiText.StatusInputReset, Color.Black);
                }
                e.Handled = true;
            }
        }

        private void CameraTimer_Tick(object? sender, EventArgs e)
        {
            if (!isCameraRunning)
                return;

            Bitmap? frame = cameraService.GetFrame();
            if (frame == null)
                return;

            using var mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(frame);

            // Define ROI ONCE
            int x = (int)(mat.Width * 0.15);
            int y = (int)(mat.Height * 0.32);
            int w = (int)(mat.Width * 0.70);
            int h = (int)(mat.Height * 0.22);

            var roi = new OpenCvSharp.Rect(x, y, w, h);

            // dark overlay outside scan area
            using (var original = mat.Clone())
            using (var darkened = mat.Clone())
            {
                // darken full frame
                OpenCvSharp.Cv2.AddWeighted(
                    darkened, 0.35,
                    OpenCvSharp.Mat.Zeros(darkened.Size(), darkened.Type()), 0.65,
                    0,
                    darkened
                );

                // restore the scan area from the original image
                using (var originalRoi = new OpenCvSharp.Mat(original, roi))
                using (var darkenedRoi = new OpenCvSharp.Mat(darkened, roi))
                {
                    originalRoi.CopyTo(darkenedRoi);
                }

                darkened.CopyTo(mat);
            }

            // green scan box
            OpenCvSharp.Cv2.Rectangle(mat, roi, OpenCvSharp.Scalar.LimeGreen, 2);
            OpenCvSharp.Cv2.PutText(
            mat,
            "ID hier halten",
            new OpenCvSharp.Point(roi.X + 10, roi.Y - 10),
            OpenCvSharp.HersheyFonts.HersheySimplex,
            0.8,
            OpenCvSharp.Scalar.LimeGreen,
            2
            );

            // Update preview
            var preview = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            var old = picCameraPreview.Image;
            picCameraPreview.Image = preview;
            old?.Dispose();

            if (DateTime.Now < cooldownUntil)
                return;

            bool shouldProcess =
                (settings.CameraMode == CameraScanMode.Automatic && databaseService.HasActiveElection()) ||
                isScanning;

            if (!shouldProcess)
                return;

            ocrFrameSkipCounter++;

            if (ocrFrameSkipCounter < OcrEveryNFrames)
                return;

            ocrFrameSkipCounter = 0;

            // Crop directly from SAME mat using SAME roi
            using var cropped = new OpenCvSharp.Mat(mat, roi);
            using var gray = new OpenCvSharp.Mat();
            using var enlarged = new OpenCvSharp.Mat();

            OpenCvSharp.Cv2.CvtColor(cropped, gray, OpenCvSharp.ColorConversionCodes.BGR2GRAY);
            OpenCvSharp.Cv2.Resize(gray, enlarged, new OpenCvSharp.Size(), 3, 3, OpenCvSharp.InterpolationFlags.Cubic);

            using var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(enlarged);

            // Debug image
            bitmap.Save("debug_ocr.png");

            string detectedId = ocrService.ReadIdFromImage(bitmap);

            if (string.IsNullOrWhiteSpace(detectedId))
            {
                if (isScanning)
                {
                    lastDetectedId = "";
                    consecutiveMatches = 0;
                    SetStatus("Scanne...", Color.DarkBlue);
                }
                return;
            }

            if (detectedId == lastDetectedId)
                consecutiveMatches++;
            else
            {
                lastDetectedId = detectedId;
                consecutiveMatches = 1;
            }

            SetStatus($"Scanne... {detectedId} ({consecutiveMatches}/{settings.ConsecutiveFramesRequired})", Color.DarkBlue);

            if (consecutiveMatches >= settings.ConsecutiveFramesRequired)
                CompleteSuccessfulScan(detectedId);
        }

        private void UpdatePreview(Bitmap frame)
        {
            Bitmap preview = (Bitmap)frame.Clone();
            var old = picCameraPreview.Image;
            picCameraPreview.Image = preview;
            old?.Dispose();
        }

        private void StartScanningSession()
        {
            ocrFrameSkipCounter = 0;
            if (!isCameraRunning)
            {
                SetStatus(UiText.StatusCameraRequired, Color.DarkRed);
                return;
            }
            if (DateTime.Now < cooldownUntil)
                return;
            isScanning = true;
            lastDetectedId = "";
            consecutiveMatches = 0;
            SetStatus(UiText.StatusScanning, Color.DarkBlue);
            UpdateCommandStates(databaseService.HasActiveElection());
        }

        private void CancelScanningSession()
        {
            isScanning = false;
            lastDetectedId = "";
            consecutiveMatches = 0;
            SetStatus(UiText.StatusScanCancelled, Color.Black);
            UpdateCommandStates(databaseService.HasActiveElection());
        }

        private void CompleteSuccessfulScan(string scannedId)
        {
            isScanning = false;
            lastDetectedId = "";
            consecutiveMatches = 0;
            cooldownUntil = DateTime.Now.AddMilliseconds(settings.CooldownMs);
            txtIdInput.Text = scannedId;
            btnRegisterVote_Click(this, EventArgs.Empty);
            UpdateCommandStates(databaseService.HasActiveElection());
        }

        private void StopCameraAndScanning()
        {
            isScanning = false;
            lastDetectedId = "";
            consecutiveMatches = 0;
            cameraTimer.Stop();
            cameraService.StopCamera();
            isCameraRunning = false;
            var old = picCameraPreview.Image;
            picCameraPreview.Image = null;
            old?.Dispose();
            UpdateCommandStates(databaseService.HasActiveElection());
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            StopCameraAndScanning();
            base.OnFormClosed(e);
        }
    }
}
