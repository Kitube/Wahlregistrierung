Install required NuGet packages:
- Microsoft.Data.Sqlite
- CsvHelper

These code files assume your existing WinForms designer contains the following controls
with EXACT names:

Labels:
- lblElectionName
- lblName
- lblStatus
- lblParticipantCount
- lblVoteCount
- lblLastVote

TextBox:
- txtIdInput

Buttons:
- btnImportCsv
- btnNewElection
- btnRegisterVote
- btnUndoLastVote
- btnViewLog
- btnExportCSV
- btnQuit

Event wiring in the designer (exact method names):
- Form Load -> MainForm_Load
- Form KeyDown -> MainForm_KeyDown
- txtIdInput KeyPress -> txtIdInput_KeyPress
- btnImportCsv Click -> btnImportCsv_Click
- btnNewElection Click -> btnNewElection_Click
- btnRegisterVote Click -> btnRegisterVote_Click
- btnUndoLastVote Click -> btnUndoLastVote_Click
- btnViewLog Click -> btnViewLog_Click
- btnExportCSV Click -> btnExportCSV_Click
- btnQuit Click -> btnQuit_Click

Also set on the form:
- KeyPreview = true

CSV requirements:
- Header row must include: IdNumber,Name
- IdNumber must contain digits only
- Name must not be empty
- Duplicate IdNumber values are rejected
- Names with commas are supported when the CSV uses standard quoting, e.g.:
  1001,"Müller, Max"

Notes:
- Remove old unused handlers from your project to avoid duplicate wiring.
- The app stores data in:
  Data\voting.db
  Exports\
  Backups\
  under the application folder.
