using BriefingStudio.UI;
using System;
using System.Windows.Forms;

namespace BriefingStudio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Properties.Settings.Default.Reload();
            Application.Run(new MainForm());
        }
    }
}
