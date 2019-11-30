using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    public partial class TXBSyntaxHelpForm : Form
    {
        public TXBSyntaxHelpForm()
        {
            InitializeComponent();
        }

        public void SetDescentGame(int descentGame)
        {
            helpTextBox.Text = GetHelpText(descentGame);
        }

        private string GetHelpText(int descentGame)
        {
            if (descentGame == 1)
            {
                return Properties.Resources.briefingHelpD1;
            }
            else if (descentGame == 2)
            {
                return Properties.Resources.briefingHelpD2;
            }
            else
            {
                return "";
            }
        }

        private void TXBSyntaxHelpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
