namespace Wahlregistrierung
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainLayout = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblStatus = new Label();
            picCameraPreview = new PictureBox();
            lblName = new Label();
            lblVoterNumber = new Label();
            txtIdInput = new TextBox();
            lblVoteCount = new Label();
            lblLastVote = new Label();
            lblParticipantCount = new Label();
            lblTitle = new Label();
            lblElectionName = new Label();
            btnRegisterVote = new Button();
            menuStrip1 = new MenuStrip();
            miFile = new ToolStripMenuItem();
            miImportCsv = new ToolStripMenuItem();
            miNewElection = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            miExportCsv = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            miQuit = new ToolStripMenuItem();
            miScan = new ToolStripMenuItem();
            miStartCamera = new ToolStripMenuItem();
            miStopCamera = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            miStartScan = new ToolStripMenuItem();
            miCancelScan = new ToolStripMenuItem();
            miActions = new ToolStripMenuItem();
            miRegisterVote = new ToolStripMenuItem();
            miUndoLastVote = new ToolStripMenuItem();
            miViewLog = new ToolStripMenuItem();
            miSettingsMenu = new ToolStripMenuItem();
            miCameraMode = new ToolStripMenuItem();
            miCameraOff = new ToolStripMenuItem();
            miCameraManual = new ToolStripMenuItem();
            miCameraAuto = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            miOptions = new ToolStripMenuItem();
            miHelp = new ToolStripMenuItem();
            miInfo = new ToolStripMenuItem();
            toolStripMain = new ToolStrip();
            tsbImportCsv = new ToolStripButton();
            tsbNewElection = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            tsbStartCamera = new ToolStripButton();
            tsbStopCamera = new ToolStripButton();
            tsbScanId = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            tsbRegisterVote = new ToolStripButton();
            tsbUndoLastVote = new ToolStripButton();
            toolStripSeparator7 = new ToolStripSeparator();
            tsbExportCsv = new ToolStripButton();
            tsbViewLog = new ToolStripButton();
            toolStripSeparator8 = new ToolStripSeparator();
            tsbSettings = new ToolStripButton();
            mainLayout.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCameraPreview).BeginInit();
            menuStrip1.SuspendLayout();
            toolStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(tableLayoutPanel1, 0, 2);
            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(lblElectionName, 0, 1);
            mainLayout.Controls.Add(btnRegisterVote, 0, 3);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 24);
            mainLayout.Margin = new Padding(6);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(12);
            mainLayout.RowCount = 4;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.Size = new Size(474, 512);
            mainLayout.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblStatus, 0, 1);
            tableLayoutPanel1.Controls.Add(picCameraPreview, 0, 4);
            tableLayoutPanel1.Controls.Add(lblName, 1, 1);
            tableLayoutPanel1.Controls.Add(lblVoterNumber, 0, 0);
            tableLayoutPanel1.Controls.Add(txtIdInput, 1, 0);
            tableLayoutPanel1.Controls.Add(lblVoteCount, 1, 2);
            tableLayoutPanel1.Controls.Add(lblLastVote, 1, 3);
            tableLayoutPanel1.Controls.Add(lblParticipantCount, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(15, 105);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(444, 332);
            tableLayoutPanel1.TabIndex = 17;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 40);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(134, 40);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picCameraPreview
            // 
            tableLayoutPanel1.SetColumnSpan(picCameraPreview, 2);
            picCameraPreview.Dock = DockStyle.Fill;
            picCameraPreview.Location = new Point(3, 143);
            picCameraPreview.Name = "picCameraPreview";
            picCameraPreview.Size = new Size(438, 186);
            picCameraPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picCameraPreview.TabIndex = 8;
            picCameraPreview.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.WhiteSmoke;
            lblName.BorderStyle = BorderStyle.FixedSingle;
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.Location = new Point(143, 40);
            lblName.Name = "lblName";
            lblName.Size = new Size(298, 40);
            lblName.TabIndex = 3;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVoterNumber
            // 
            lblVoterNumber.AutoSize = true;
            lblVoterNumber.Dock = DockStyle.Fill;
            lblVoterNumber.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVoterNumber.ImageAlign = ContentAlignment.TopCenter;
            lblVoterNumber.Location = new Point(3, 0);
            lblVoterNumber.Name = "lblVoterNumber";
            lblVoterNumber.Size = new Size(134, 40);
            lblVoterNumber.TabIndex = 0;
            lblVoterNumber.Text = "Wählernummer";
            lblVoterNumber.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtIdInput
            // 
            txtIdInput.Dock = DockStyle.Fill;
            txtIdInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdInput.Location = new Point(144, 4);
            txtIdInput.Margin = new Padding(4);
            txtIdInput.Name = "txtIdInput";
            txtIdInput.Size = new Size(296, 29);
            txtIdInput.TabIndex = 1;
            // 
            // lblVoteCount
            // 
            lblVoteCount.AutoSize = true;
            lblVoteCount.Dock = DockStyle.Fill;
            lblVoteCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVoteCount.ImageAlign = ContentAlignment.MiddleRight;
            lblVoteCount.Location = new Point(143, 80);
            lblVoteCount.Name = "lblVoteCount";
            lblVoteCount.Size = new Size(298, 30);
            lblVoteCount.TabIndex = 3;
            lblVoteCount.Text = "Abgegebene Stimmen: 0";
            lblVoteCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLastVote
            // 
            lblLastVote.AutoSize = true;
            lblLastVote.Dock = DockStyle.Fill;
            lblLastVote.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastVote.ForeColor = Color.DimGray;
            lblLastVote.Location = new Point(143, 110);
            lblLastVote.Name = "lblLastVote";
            lblLastVote.Size = new Size(298, 30);
            lblLastVote.TabIndex = 5;
            lblLastVote.Text = "Letzte Stimme: -";
            lblLastVote.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblParticipantCount
            // 
            lblParticipantCount.AutoSize = true;
            lblParticipantCount.Dock = DockStyle.Fill;
            lblParticipantCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblParticipantCount.Location = new Point(3, 80);
            lblParticipantCount.Name = "lblParticipantCount";
            lblParticipantCount.Size = new Size(134, 30);
            lblParticipantCount.TabIndex = 6;
            lblParticipantCount.Text = "Teilnehmer: 0";
            lblParticipantCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = SystemColors.ControlLight;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(15, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(444, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Wahlregistrierung";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblElectionName
            // 
            lblElectionName.AutoSize = true;
            lblElectionName.Dock = DockStyle.Fill;
            lblElectionName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblElectionName.Location = new Point(15, 72);
            lblElectionName.Name = "lblElectionName";
            lblElectionName.Size = new Size(444, 30);
            lblElectionName.TabIndex = 19;
            lblElectionName.Text = "Keine aktive Wahl";
            lblElectionName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRegisterVote
            // 
            btnRegisterVote.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnRegisterVote.Location = new Point(15, 443);
            btnRegisterVote.MaximumSize = new Size(150, 60);
            btnRegisterVote.Name = "btnRegisterVote";
            btnRegisterVote.Size = new Size(150, 54);
            btnRegisterVote.TabIndex = 20;
            btnRegisterVote.Text = "Registrieren";
            btnRegisterVote.UseVisualStyleBackColor = true;
            btnRegisterVote.Click += btnRegisterVote_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { miFile, miScan, miActions, miSettingsMenu, miHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(474, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // miFile
            // 
            miFile.DropDownItems.AddRange(new ToolStripItem[] { miImportCsv, miNewElection, toolStripSeparator1, miExportCsv, toolStripSeparator2, miQuit });
            miFile.Name = "miFile";
            miFile.Size = new Size(46, 20);
            miFile.Text = "Datei";
            // 
            // miImportCsv
            // 
            miImportCsv.Name = "miImportCsv";
            miImportCsv.Size = new Size(132, 22);
            miImportCsv.Text = "CSV laden";
            miImportCsv.Click += btnImportCsv_Click;
            // 
            // miNewElection
            // 
            miNewElection.Name = "miNewElection";
            miNewElection.Size = new Size(132, 22);
            miNewElection.Text = "Neue Wahl";
            miNewElection.Click += btnNewElection_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(129, 6);
            // 
            // miExportCsv
            // 
            miExportCsv.Name = "miExportCsv";
            miExportCsv.Size = new Size(132, 22);
            miExportCsv.Text = "Export";
            miExportCsv.Click += btnExportCSV_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(129, 6);
            // 
            // miQuit
            // 
            miQuit.Name = "miQuit";
            miQuit.Size = new Size(132, 22);
            miQuit.Text = "Beenden";
            miQuit.Click += btnQuit_Click;
            // 
            // miScan
            // 
            miScan.DropDownItems.AddRange(new ToolStripItem[] { miStartCamera, miStopCamera, toolStripSeparator3, miStartScan, miCancelScan });
            miScan.Name = "miScan";
            miScan.Size = new Size(44, 20);
            miScan.Text = "Scan";
            // 
            // miStartCamera
            // 
            miStartCamera.Name = "miStartCamera";
            miStartCamera.Size = new Size(178, 22);
            miStartCamera.Text = "Kamera starten";
            miStartCamera.Click += btnStartCamera_Click;
            // 
            // miStopCamera
            // 
            miStopCamera.Name = "miStopCamera";
            miStopCamera.Size = new Size(178, 22);
            miStopCamera.Text = "Kamera stoppen";
            miStopCamera.Click += btnStopCamera_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(175, 6);
            // 
            // miStartScan
            // 
            miStartScan.Name = "miStartScan";
            miStartScan.Size = new Size(178, 22);
            miStartScan.Text = "Scannen starten";
            miStartScan.Click += btnScanId_Click;
            // 
            // miCancelScan
            // 
            miCancelScan.Name = "miCancelScan";
            miCancelScan.Size = new Size(178, 22);
            miCancelScan.Text = "Scannen abbrechen";
            miCancelScan.Click += miCancelScan_Click;
            // 
            // miActions
            // 
            miActions.DropDownItems.AddRange(new ToolStripItem[] { miRegisterVote, miUndoLastVote, miViewLog });
            miActions.Name = "miActions";
            miActions.Size = new Size(67, 20);
            miActions.Text = "Aktionen";
            // 
            // miRegisterVote
            // 
            miRegisterVote.Name = "miRegisterVote";
            miRegisterVote.Size = new Size(187, 22);
            miRegisterVote.Text = "Registrieren";
            miRegisterVote.Click += btnRegisterVote_Click;
            // 
            // miUndoLastVote
            // 
            miUndoLastVote.Name = "miUndoLastVote";
            miUndoLastVote.Size = new Size(187, 22);
            miUndoLastVote.Text = "Letzte Stimme zurück";
            miUndoLastVote.Click += btnUndoLastVote_Click;
            // 
            // miViewLog
            // 
            miViewLog.Name = "miViewLog";
            miViewLog.Size = new Size(187, 22);
            miViewLog.Text = "Scan-Log";
            miViewLog.Click += btnViewLog_Click;
            // 
            // miSettingsMenu
            // 
            miSettingsMenu.DropDownItems.AddRange(new ToolStripItem[] { miCameraMode, toolStripSeparator4, miOptions });
            miSettingsMenu.Name = "miSettingsMenu";
            miSettingsMenu.Size = new Size(90, 20);
            miSettingsMenu.Text = "Einstellungen";
            // 
            // miCameraMode
            // 
            miCameraMode.DropDownItems.AddRange(new ToolStripItem[] { miCameraOff, miCameraManual, miCameraAuto });
            miCameraMode.Name = "miCameraMode";
            miCameraMode.Size = new Size(156, 22);
            miCameraMode.Text = "Kamera-Modus";
            // 
            // miCameraOff
            // 
            miCameraOff.Name = "miCameraOff";
            miCameraOff.Size = new Size(175, 22);
            miCameraOff.Text = "Aus";
            miCameraOff.Click += miCameraOff_Click;
            // 
            // miCameraManual
            // 
            miCameraManual.Name = "miCameraManual";
            miCameraManual.Size = new Size(175, 22);
            miCameraManual.Text = "Manuell (Leertaste)";
            miCameraManual.Click += miCameraManual_Click;
            // 
            // miCameraAuto
            // 
            miCameraAuto.Name = "miCameraAuto";
            miCameraAuto.Size = new Size(175, 22);
            miCameraAuto.Text = "Automatisch";
            miCameraAuto.Click += miCameraAuto_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(153, 6);
            // 
            // miOptions
            // 
            miOptions.Name = "miOptions";
            miOptions.Size = new Size(156, 22);
            miOptions.Text = "Optionen";
            miOptions.Click += btnSettings_Click;
            // 
            // miHelp
            // 
            miHelp.DropDownItems.AddRange(new ToolStripItem[] { miInfo });
            miHelp.Name = "miHelp";
            miHelp.Size = new Size(44, 20);
            miHelp.Text = "Hilfe";
            // 
            // miInfo
            // 
            miInfo.Name = "miInfo";
            miInfo.Size = new Size(95, 22);
            miInfo.Text = "Info";
            miInfo.Click += miInfo_Click;
            // 
            // toolStripMain
            // 
            toolStripMain.Items.AddRange(new ToolStripItem[] { tsbImportCsv, tsbNewElection, toolStripSeparator5, tsbStartCamera, tsbStopCamera, tsbScanId, toolStripSeparator6, tsbRegisterVote, tsbUndoLastVote, toolStripSeparator7, tsbExportCsv, tsbViewLog, toolStripSeparator8, tsbSettings });
            toolStripMain.Location = new Point(0, 24);
            toolStripMain.Name = "toolStripMain";
            toolStripMain.Size = new Size(474, 25);
            toolStripMain.TabIndex = 2;
            toolStripMain.TabStop = true;
            toolStripMain.Text = "Main Tool Strip";
            // 
            // tsbImportCsv
            // 
            tsbImportCsv.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbImportCsv.Image = (Image)resources.GetObject("tsbImportCsv.Image");
            tsbImportCsv.ImageTransparentColor = Color.Magenta;
            tsbImportCsv.Name = "tsbImportCsv";
            tsbImportCsv.Size = new Size(23, 22);
            tsbImportCsv.Text = "CSV laden";
            tsbImportCsv.Click += btnImportCsv_Click;
            // 
            // tsbNewElection
            // 
            tsbNewElection.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbNewElection.Image = (Image)resources.GetObject("tsbNewElection.Image");
            tsbNewElection.ImageTransparentColor = Color.Magenta;
            tsbNewElection.Name = "tsbNewElection";
            tsbNewElection.Size = new Size(23, 22);
            tsbNewElection.Text = "Neue Wahl";
            tsbNewElection.Click += btnNewElection_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // tsbStartCamera
            // 
            tsbStartCamera.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbStartCamera.Image = (Image)resources.GetObject("tsbStartCamera.Image");
            tsbStartCamera.ImageTransparentColor = Color.Magenta;
            tsbStartCamera.Name = "tsbStartCamera";
            tsbStartCamera.Size = new Size(23, 22);
            tsbStartCamera.Text = "Kamera an";
            tsbStartCamera.ToolTipText = "Start Kamera";
            tsbStartCamera.Click += btnStartCamera_Click;
            // 
            // tsbStopCamera
            // 
            tsbStopCamera.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbStopCamera.Image = (Image)resources.GetObject("tsbStopCamera.Image");
            tsbStopCamera.ImageTransparentColor = Color.Magenta;
            tsbStopCamera.Name = "tsbStopCamera";
            tsbStopCamera.Size = new Size(23, 22);
            tsbStopCamera.Text = "Kamera aus";
            tsbStopCamera.ToolTipText = "Stop Kamera";
            tsbStopCamera.Click += btnStopCamera_Click;
            // 
            // tsbScanId
            // 
            tsbScanId.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbScanId.Image = (Image)resources.GetObject("tsbScanId.Image");
            tsbScanId.ImageTransparentColor = Color.Magenta;
            tsbScanId.Name = "tsbScanId";
            tsbScanId.Size = new Size(23, 22);
            tsbScanId.Text = "Scannen";
            tsbScanId.Click += btnScanId_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // tsbRegisterVote
            // 
            tsbRegisterVote.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRegisterVote.Image = (Image)resources.GetObject("tsbRegisterVote.Image");
            tsbRegisterVote.ImageTransparentColor = Color.Magenta;
            tsbRegisterVote.Name = "tsbRegisterVote";
            tsbRegisterVote.Size = new Size(23, 22);
            tsbRegisterVote.Text = "Registrieren";
            tsbRegisterVote.Click += btnRegisterVote_Click;
            // 
            // tsbUndoLastVote
            // 
            tsbUndoLastVote.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbUndoLastVote.Image = (Image)resources.GetObject("tsbUndoLastVote.Image");
            tsbUndoLastVote.ImageTransparentColor = Color.Magenta;
            tsbUndoLastVote.Name = "tsbUndoLastVote";
            tsbUndoLastVote.Size = new Size(23, 22);
            tsbUndoLastVote.Text = "Rückgängig";
            tsbUndoLastVote.Click += btnUndoLastVote_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 25);
            // 
            // tsbExportCsv
            // 
            tsbExportCsv.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbExportCsv.Image = (Image)resources.GetObject("tsbExportCsv.Image");
            tsbExportCsv.ImageTransparentColor = Color.Magenta;
            tsbExportCsv.Name = "tsbExportCsv";
            tsbExportCsv.Size = new Size(23, 22);
            tsbExportCsv.Text = "Export";
            tsbExportCsv.Click += btnExportCSV_Click;
            // 
            // tsbViewLog
            // 
            tsbViewLog.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbViewLog.Image = (Image)resources.GetObject("tsbViewLog.Image");
            tsbViewLog.ImageTransparentColor = Color.Magenta;
            tsbViewLog.Name = "tsbViewLog";
            tsbViewLog.Size = new Size(23, 22);
            tsbViewLog.Text = "Scan-Log";
            tsbViewLog.Click += btnViewLog_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 25);
            // 
            // tsbSettings
            // 
            tsbSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbSettings.Image = (Image)resources.GetObject("tsbSettings.Image");
            tsbSettings.ImageTransparentColor = Color.Magenta;
            tsbSettings.Name = "tsbSettings";
            tsbSettings.Size = new Size(23, 22);
            tsbSettings.Text = "Einstellungen";
            tsbSettings.Click += btnSettings_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 536);
            Controls.Add(toolStripMain);
            Controls.Add(mainLayout);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(490, 575);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wahlregistrierung";
            KeyDown += MainForm_KeyDown;
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCameraPreview).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStripMain.ResumeLayout(false);
            toolStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TableLayoutPanel mainLayout;
        private Label lblTitle;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblName;
        private Label lblVoterNumber;
        private TextBox txtIdInput;
        private Label lblVoteCount;
        private Label lblLastVote;
        private Label lblElectionName;
        private Label lblParticipantCount;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem miFile;
        private ToolStripMenuItem miNewElection;
        private ToolStripMenuItem miImportCsv;
        private ToolStripMenuItem miExportCsv;
        private ToolStripMenuItem miQuit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem miScan;
        private ToolStripMenuItem miStartCamera;
        private ToolStripMenuItem miStopCamera;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem miStartScan;
        private ToolStripMenuItem miCancelScan;
        private ToolStripMenuItem miActions;
        private ToolStripMenuItem miRegisterVote;
        private ToolStripMenuItem miUndoLastVote;
        private ToolStripMenuItem miViewLog;
        private ToolStripMenuItem miSettingsMenu;
        private ToolStripMenuItem miCameraMode;
        private ToolStripMenuItem miCameraOff;
        private ToolStripMenuItem miCameraManual;
        private ToolStripMenuItem miCameraAuto;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem miOptions;
        private ToolStripMenuItem miHelp;
        private ToolStripMenuItem miInfo;
        private ToolStrip toolStripMain;
        private ToolStripButton tsbImportCsv;
        private ToolStripButton tsbNewElection;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton tsbStartCamera;
        private ToolStripButton tsbScanId;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton tsbRegisterVote;
        private ToolStripButton tsbUndoLastVote;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton tsbExportCsv;
        private ToolStripButton tsbViewLog;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton tsbSettings;
        private Button btnRegisterVote;
        private PictureBox picCameraPreview;
        private ToolStripButton tsbStopCamera;
        private Label lblStatus;
    }
}
