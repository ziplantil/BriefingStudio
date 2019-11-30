using BriefingStudio.Logic;
using BriefingStudio.Logic.Formats;
using System;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class TXBEditorForm : Form
    {
        public delegate byte[] FindFile(string filename);
        public delegate void PlayBriefing(string text, int n, bool instant);
        public delegate void StopBriefing();
        public delegate void SaveBriefing(string filename, string contents);
        private TXBSyntaxHelpForm txbSyntaxHelpForm = new TXBSyntaxHelpForm();

        private FindFile findFile;
        private PlayBriefing playBriefing;
        private StopBriefing stopBriefing;
        private SaveBriefing saveBriefing;
        private bool _unsaved;
        internal bool fromCompile;

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

        public bool Unsaved
        {
            get
            {
                return _unsaved;
            }
            set
            {
                _unsaved = value;
                this.UpdateTitle();
            }
        }

        private void UpdateTitle()
        {
            this.Text = (Unsaved ? "*" : "") + "TXB Editor";
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            LoadFile(briefingNameTextBox.Text);
            Unsaved = false;
            fromCompile = false;
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
            playBriefing(txbBox.Text, (int)sequenceNumericUpDown.Value, false);
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
                Unsaved = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Saving failed\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TXBEditorForm_Load(object sender, EventArgs e)
        {
            Unsaved = false;
            fromCompile = false;
        }

        private void txbBox_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Modified)
            {
                Unsaved = true;
                fromCompile = false;
            }
        }

        internal void ImportText(string text)
        {
            Unsaved = true;
            fromCompile = true;
            txbBox.Text = text;
        }

        private void syntaxHelpButton_Click(object sender, EventArgs e)
        {
            txbSyntaxHelpForm.Show();
            txbSyntaxHelpForm.BringToFront();
        }

        public void SetDescentGame(int descentGame)
        {
            txbSyntaxHelpForm.SetDescentGame(descentGame);
        }

        internal void ModifyText(string v)
        {
            if (txbBox.Text != v)
            {
                txbBox.Text = v;
            }
        }
    }
}
