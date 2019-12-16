using BriefingStudio.Logic.Formats;
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

namespace BriefingStudio.UI
{
    public partial class BannerCreatorForm : Form
    {
        public delegate byte[] FindFile(string filename);

        private FindFile findFile;
        private bool testing;

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
                testing = false;
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
            Bitmap result;

            if (testing)
            {
                int cw = Math.Max(8, font.GetCharWidth('\0')) * 2;
                result = new Bitmap(cw * 16, (font.GetCharHeight() + 3) * 16);
                Graphics gdi = Graphics.FromImage(result);
                int x = 0, lx, ly, lw;
                Pen pen = new Pen(Color.Red);
                for (int c = 0; c < 256; ++c)
                {
                    lx = (c % 16) * cw;
                    ly = (font.GetCharHeight() + 3) * (c / 16);
                    x = lx;
                    font.ResetKerning();
                    font.DrawCharacterRaw(result, (char)c, Color.Green, ref x, ly);

                    x = lx;
                    ly += font.GetCharHeight();
                    lw = font.GetCharWidth((char)c);
                    gdi.DrawLine(pen, lx, ly, lx + lw, ly);
                }
            }
            else
            {
                result = new Bitmap(font.MeasureWidth(text), font.GetCharHeight());
                int x = 0;
                foreach (char c in text)
                {
                    font.DrawCharacterRaw(result, c, Color.Green, ref x, 0);
                }
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

        private void testFontButton_Click(object sender, EventArgs e)
        {
            testing = true;
            bannerSaveFileDialog.ShowDialog();
        }
    }
}
