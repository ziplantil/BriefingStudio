using System;
using System.Windows.Forms;

namespace BriefingStudio.UI
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

        private void UpdateCheckboxes()
        {
            Properties.Settings.Default.Reload();
            allowD2XColorsCheckBox.Checked = Properties.Settings.Default.allowD2XColors;
            addEndSectionCheckBox.Checked = Properties.Settings.Default.addEndSection;
            showBriefingBoxCheckBox.Checked = Properties.Settings.Default.showBriefingBox;
            fadeTransitionsCheckBox.Checked = Properties.Settings.Default.fadeTransitions;
            upscaleLowResCheckBox.Checked = Properties.Settings.Default.upscaleLowRes;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UpdateCheckboxes();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            UpdateCheckboxes();
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

        private void fadeTransitionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.fadeTransitions = fadeTransitionsCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void upscaleLowResCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.upscaleLowRes = upscaleLowResCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
