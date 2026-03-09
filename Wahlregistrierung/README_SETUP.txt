REQUIRED NUGET PACKAGES
- Microsoft.Data.Sqlite
- CsvHelper
- OpenCvSharp4
- OpenCvSharp4.runtime.win
- Tesseract

REQUIRED EXTRA FILE
Create this folder in the project root:
tessdata/

Put this file inside:
eng.traineddata

In Visual Studio, for eng.traineddata set:
- Build Action = Content
- Copy to Output Directory = Copy if newer

IMPORTANT
Do NOT overwrite MainForm.Designer.cs unless you know exactly what you are doing.
Add the controls in the designer and wire them to the methods below.

PROJECT FILES TO ADD
- AppPaths.cs
- Enums/CameraScanMode.cs
- Config/AppSettings.cs
- UI/UiText.cs
- Models/*.cs
- Services/DatabaseService.cs
- Services/CameraService.cs
- Services/OcrService.cs
- ScanLogForm.cs
- MainForm.cs (replace your current code-behind, not the Designer file)

REQUIRED DESIGNER CONTROL NAMES
BODY CONTROLS
Labels:
- lblElectionName
- lblName
- lblStatus
- lblParticipantCount
- lblVoteCount
- lblLastVote
TextBox:
- txtIdInput
PictureBox:
- picCameraPreview
Body button:
- btnRegisterVote

TOOLSTRIP BUTTONS
- tsbImportCsv
- tsbNewElection
- tsbStartCamera
- tsbStopCamera
- tsbScanId
- tsbRegisterVote
- tsbUndoLastVote
- tsbExportCsv
- tsbViewLog
- tsbSettings

MENU ITEMS
File:
- miFile
- miImportCsv
- miNewElection
- miExportCsv
- miQuit
Scan:
- miScan
- miStartCamera
- miStopCamera
- miStartScan
- miCancelScan
Actions:
- miActions
- miRegisterVote
- miUndoLastVote
- miViewLog
Settings:
- miSettingsMenu
- miCameraMode
- miCameraOff
- miCameraManual
- miCameraAuto
- miOptions
Help:
- miHelp
- miInfo

EVENT MAPPING
FORM
- Load -> MainForm_Load
- KeyDown -> MainForm_KeyDown
- KeyPreview = true
- AcceptButton = btnRegisterVote
TEXTBOX
- txtIdInput KeyPress -> txtIdInput_KeyPress
BODY BUTTON
- btnRegisterVote Click -> btnRegisterVote_Click
TOOLSTRIP BUTTONS
- tsbImportCsv Click -> btnImportCsv_Click
- tsbNewElection Click -> btnNewElection_Click
- tsbStartCamera Click -> btnStartCamera_Click
- tsbStopCamera Click -> btnStopCamera_Click
- tsbScanId Click -> btnScanId_Click
- tsbRegisterVote Click -> btnRegisterVote_Click
- tsbUndoLastVote Click -> btnUndoLastVote_Click
- tsbExportCsv Click -> btnExportCSV_Click
- tsbViewLog Click -> btnViewLog_Click
- tsbSettings Click -> btnSettings_Click
MENU ITEMS
- miImportCsv Click -> btnImportCsv_Click
- miNewElection Click -> btnNewElection_Click
- miExportCsv Click -> btnExportCSV_Click
- miQuit Click -> btnQuit_Click
- miStartCamera Click -> btnStartCamera_Click
- miStopCamera Click -> btnStopCamera_Click
- miStartScan Click -> btnScanId_Click
- miCancelScan Click -> miCancelScan_Click
- miRegisterVote Click -> btnRegisterVote_Click
- miUndoLastVote Click -> btnUndoLastVote_Click
- miViewLog Click -> btnViewLog_Click
- miCameraOff Click -> miCameraOff_Click
- miCameraManual Click -> miCameraManual_Click
- miCameraAuto Click -> miCameraAuto_Click
- miOptions Click -> btnSettings_Click
- miInfo Click -> miInfo_Click

OCR FLOW
- Camera Off
- Manual Trigger: Hold card in front of webcam, press Space or click Scannen. OCR runs until the same numeric ID is detected in 3 consecutive frames. Then the vote is registered automatically.
- Automatic: OCR continuously watches frames and auto-registers after stable 3-frame detection.

DATA LOCATIONS
- Data\voting.db
- Exports\
- Backups\
