using System;
using System.Windows.Forms;

namespace BriefingStudio
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            allowD2XColorsCheckBox.Checked = Properties.Settings.Default.allowD2XColors;
            addEndSectionCheckBox.Checked = Properties.Settings.Default.addEndSection;
            showBriefingBoxCheckBox.Checked = Properties.Settings.Default.showBriefingBox;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }

        private void allowD2XColorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.allowD2XColors = allowD2XColorsCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void addEndSectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.addEndSection = addEndSectionCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void showBriefingBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.showBriefingBox = showBriefingBoxCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
