using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Wahlregistrierung
{
    public partial class frmMain : Form
    {
        private List<Voter> voters = new List<Voter>();
        private HashSet<string> votedIds = new HashSet<string>();
        public frmMain()
        {
            InitializeComponent();
            btnRegisterVote.Enabled = false;
            lblStatus.Text = "Bitte CSV laden.";
            lblName.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                openFileDialog.Title = "Teilnehmerliste auswählen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        voters.Clear();

                        var lines = File.ReadAllLines(openFileDialog.FileName);

                        foreach (var line in lines.Skip(1)) // skip header
                        {
                            var parts = line.Split(',');

                            if (parts.Length >= 2)
                            {
                                voters.Add(new Voter
                                {
                                    IdNumber = parts[0].Trim(),
                                    Name = parts[1].Trim()
                                });
                            }
                        }

                        MessageBox.Show($"{voters.Count} Teilnehmer geladen.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim Laden: " + ex.Message);
                    }
                }
            }
        }

        private void lblLog_Click(object sender, EventArgs e)
        {

        }

        private void txtIdInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void btnImportCsv_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog.Title = "Teilnehmerliste auswählen";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                voters.Clear();

                var lines = File.ReadAllLines(openFileDialog.FileName);

                foreach (var line in lines.Skip(1)) // header skip
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        voters.Add(new Voter
                        {
                            IdNumber = parts[0].Trim(),
                            Name = parts[1].Trim()
                        });
                    }
                }

                votedIds.Clear();
                btnRegisterVote.Enabled = true;
                lblStatus.Text = $"{voters.Count} Teilnehmer geladen.";
                lblName.Text = "";
                txtIdInput.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden: " + ex.Message);
            }
        }

        private void btnRegisterVote_Click(object sender, EventArgs e)
        {
            string inputId = txtIdInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputId))
            {
                lblStatus.Text = "Ungültige Eingabe.";
                lblName.Text = "";
                return;
            }

            var voter = voters.FirstOrDefault(v => v.IdNumber == inputId);

            if (voter == null)
            {
                lblStatus.Text = "ID nicht gefunden.";
                lblName.Text = "";
                return;
            }

            lblName.Text = voter.Name; // <-- you will see the name now

            if (votedIds.Contains(inputId))
            {
                lblStatus.Text = "Bereits abgestimmt.";
                return;
            }

            votedIds.Add(inputId);
            lblStatus.Text = "Stimme registriert.";
            txtIdInput.Clear();
            txtIdInput.Focus();
        }
    }
}
