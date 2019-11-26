using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BriefingStudio
{
    public partial class BannerCreatorForm : Form
    {
        public delegate byte[] FindFile(string filename);

        private FindFile findFile;

        public BannerCreatorForm()
        {
            InitializeComponent();
        }

        public void SetFindFile(FindFile findFile)
        {
            this.findFile = findFile;
        }

        private void makeButton_Click(object sender, EventArgs e)
        {
            if (textTextBox.Text.Length > 0 && fontTextBox.Text.Length > 0)
            {
                bannerSaveFileDialog.ShowDialog();
            }
        }

        private void bannerSaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            byte[] fntData = findFile(fontTextBox.Text);
            if (fntData == null)
            {
                MessageBox.Show(this, "Cannot find the given font", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string text = textTextBox.Text;
            FNTFont font = new FNTFont(new MemoryStream(fntData));
            Bitmap result = new Bitmap(font.MeasureWidth(text), font.GetCharHeight());

            int x = 0;
            foreach (char c in text)
            {
                font.DrawCharacterRaw(result, c, Color.Green, ref x, 0);
            }
            result.Save(bannerSaveFileDialog.FileName);
        }

        private void BannerCreatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
