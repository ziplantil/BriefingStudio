using System.Drawing;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class LowResScreenForm : Form
    {
        public LowResScreenForm()
        {
            InitializeComponent();
        }

        public void SetImage(Bitmap b)
        {
            this.screen.Image = b;
        }

        private void LowResScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
