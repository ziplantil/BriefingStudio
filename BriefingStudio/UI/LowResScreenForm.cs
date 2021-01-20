using System.Drawing;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class LowResScreenForm : Form
    {
        private bool _upscale = false;

        public bool Upscale
        {
            get => _upscale;
            set
            {
                _upscale = value;
                this.ClientSize = value ? new Size(640, 400) : new Size(320, 200);
            }
        }


        public LowResScreenForm()
        {
            InitializeComponent();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            Upscale = Properties.Settings.Default.upscaleLowRes;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "upscaleLowRes")
            {
                Upscale = Properties.Settings.Default.upscaleLowRes;
            }
        }

        public void SetImage(Bitmap b)
        {
            if (Upscale)
            {
                Bitmap i = new Bitmap(640, 400);
                using (Graphics g = Graphics.FromImage(i))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImage(b, new Rectangle(0, 0, 640, 400));
                }
                this.screen.Image = i;
            }
            else
                this.screen.Image = b;
            this.Invalidate();
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
