using System.Drawing;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class HighResScreenForm : Form
    {
        public HighResScreenForm()
        {
            InitializeComponent();
        }

        public void SetImage(Bitmap b)
        {
            this.screen.Image = b;
        }

        private void HighResScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
