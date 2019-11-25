using System;
using System.Windows.Forms;

namespace BriefingStudio
{
    public partial class TXBEditorForm : Form
    {
        public delegate byte[] FindFile(string filename);
        public delegate void PlayBriefing(string text, int n);
        public delegate void StopBriefing();
        public delegate void SaveBriefing(string filename, string contents);

        private FindFile findFile;
        private PlayBriefing playBriefing;
        private StopBriefing stopBriefing;
        private SaveBriefing saveBriefing;

        public TXBEditorForm()
        {
            InitializeComponent();
        }

        public void SetDelegates(FindFile findFile, PlayBriefing playBriefing, StopBriefing stopBriefing, SaveBriefing saveBriefing)
        {
            this.findFile = findFile;
            this.playBriefing = playBriefing;
            this.stopBriefing = stopBriefing;
            this.saveBriefing = saveBriefing;
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            LoadFile(briefingNameTextBox.Text);
            this.Text = "TXB Editor";
        }

        public void LoadFile(string fn)
        {
            briefingNameTextBox.Text = fn;
            byte[] txb = findFile(fn);
            string text = txb != null ? TXBConverter.DecodeTXB(txb) : "";
            txbBox.Text = text.Replace("\n", "\r\n");
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopBriefing();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            playBriefing(txbBox.Text, (int)sequenceNumericUpDown.Value);
        }

        private void jumpButton_Click(object sender, EventArgs e)
        {
            int index = Briefing.FindSequenceIndex(txbBox.Text, (int)sequenceNumericUpDown.Value);
            if (index < txbBox.Text.Length)
            {
                txbBox.Focus();
                txbBox.SelectionStart = index;
                txbBox.SelectionLength = 0;
                txbBox.ScrollToCaret();
            }
        }

        private void TXBEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        internal void SetCanSave(bool v)
        {
            saveButton.Enabled = v;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                saveBriefing(briefingNameTextBox.Text, txbBox.Text);
                this.Text = "TXB Editor";
                LoadFile(briefingNameTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Saving failed\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TXBEditorForm_Load(object sender, EventArgs e)
        {
            this.Text = "TXB Editor";
        }

        private void txbBox_TextChanged(object sender, EventArgs e)
        {
            this.Text = "TXB Editor (UNSAVED)";
        }
    }
}
