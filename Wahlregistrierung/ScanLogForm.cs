using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Wahlregistrierung.Models;

namespace Wahlregistrierung
{
    public class ScanLogForm : Form
    {
        private readonly DataGridView grid = new DataGridView();

        public ScanLogForm(List<ScanLogEntry> entries)
        {
            Text = "Scan-Log";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(900, 500);
            MinimumSize = new Size(700, 400);

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.DataSource = entries;

            Controls.Add(grid);
        }
    }
}
