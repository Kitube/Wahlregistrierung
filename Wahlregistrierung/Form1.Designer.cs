namespace Wahlregistrierung
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            mainLayout = new TableLayoutPanel();
            lblTitle = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblName = new Label();
            lblVoterNumber = new Label();
            txtIdInput = new TextBox();
            lblStatus = new Label();
            btnLayout = new TableLayoutPanel();
            btnImportCsv = new Button();
            btnRegisterVote = new Button();
            btnQuit = new Button();
            mainLayout.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            btnLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(tableLayoutPanel1, 0, 1);
            mainLayout.Controls.Add(btnLayout, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            mainLayout.Size = new Size(584, 461);
            mainLayout.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(578, 100);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Wahlregistrierung";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.Controls.Add(lblName, 1, 1);
            tableLayoutPanel1.Controls.Add(lblVoterNumber, 0, 0);
            tableLayoutPanel1.Controls.Add(txtIdInput, 1, 0);
            tableLayoutPanel1.Controls.Add(lblStatus, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 103);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(578, 265);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.WhiteSmoke;
            lblName.BorderStyle = BorderStyle.FixedSingle;
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.Location = new Point(205, 60);
            lblName.Name = "lblName";
            lblName.Size = new Size(370, 80);
            lblName.TabIndex = 3;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.Click += lblName_Click;
            // 
            // lblVoterNumber
            // 
            lblVoterNumber.AutoSize = true;
            lblVoterNumber.Dock = DockStyle.Fill;
            lblVoterNumber.Location = new Point(3, 0);
            lblVoterNumber.Name = "lblVoterNumber";
            lblVoterNumber.Size = new Size(196, 60);
            lblVoterNumber.TabIndex = 0;
            lblVoterNumber.Text = "Wählernummer";
            lblVoterNumber.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtIdInput
            // 
            txtIdInput.Dock = DockStyle.Fill;
            txtIdInput.Location = new Point(205, 3);
            txtIdInput.Name = "txtIdInput";
            txtIdInput.Size = new Size(370, 23);
            txtIdInput.TabIndex = 1;
            txtIdInput.TextChanged += txtIdInput_TextChanged;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(205, 140);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(370, 40);
            lblStatus.TabIndex = 4;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLayout
            // 
            btnLayout.ColumnCount = 3;
            btnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            btnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            btnLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            btnLayout.Controls.Add(btnImportCsv, 0, 0);
            btnLayout.Controls.Add(btnRegisterVote, 1, 0);
            btnLayout.Controls.Add(btnQuit, 2, 0);
            btnLayout.Dock = DockStyle.Fill;
            btnLayout.Location = new Point(3, 374);
            btnLayout.Name = "btnLayout";
            btnLayout.RowCount = 1;
            btnLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            btnLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            btnLayout.Size = new Size(578, 84);
            btnLayout.TabIndex = 2;
            // 
            // btnImportCsv
            // 
            btnImportCsv.Dock = DockStyle.Fill;
            btnImportCsv.Location = new Point(3, 3);
            btnImportCsv.Name = "btnImportCsv";
            btnImportCsv.Size = new Size(184, 78);
            btnImportCsv.TabIndex = 0;
            btnImportCsv.Text = "CSV Hochladen";
            btnImportCsv.UseVisualStyleBackColor = true;
            btnImportCsv.Click += btnImportCsv_Click;
            // 
            // btnRegisterVote
            // 
            btnRegisterVote.Dock = DockStyle.Fill;
            btnRegisterVote.Location = new Point(193, 3);
            btnRegisterVote.Name = "btnRegisterVote";
            btnRegisterVote.Size = new Size(190, 78);
            btnRegisterVote.TabIndex = 1;
            btnRegisterVote.Text = "Wahl speichern";
            btnRegisterVote.UseVisualStyleBackColor = true;
            btnRegisterVote.Click += btnRegisterVote_Click;
            // 
            // btnQuit
            // 
            btnQuit.Dock = DockStyle.Fill;
            btnQuit.Location = new Point(389, 3);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(186, 78);
            btnQuit.TabIndex = 2;
            btnQuit.Text = "Quit";
            btnQuit.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            AcceptButton = btnRegisterVote;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 461);
            Controls.Add(mainLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wahlregistrierung";
            Load += Form1_Load;
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            btnLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public TableLayoutPanel mainLayout;
        private Label lblTitle;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblVoterNumber;
        private TextBox txtIdInput;
        private Label lblName;
        private Label lblStatus;
        private TableLayoutPanel btnLayout;
        private Button btnImportCsv;
        private Button btnRegisterVote;
        private Button btnQuit;
    }
}
