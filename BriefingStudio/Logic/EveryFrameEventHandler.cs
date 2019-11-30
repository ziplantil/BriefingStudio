using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace BriefingStudio.Logic
{
    class EveryFrameEventHandler
    {
        // based on Tom Miller's Render Loop
        public event EventHandler IdleFrame;

        internal void Bind()
        {
            System.Windows.Forms.Application.Idle += this.OnApplicationIdle;
        }

        private void OnApplicationIdle(object sender, EventArgs e)
        {
            while (AppStillIdle)
            {
                IdleFrame?.Invoke(this, e);
                Thread.Sleep(5);
            }
        }

        private bool AppStillIdle
        {
            get
            {
                Message msg;
                return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr hWnd;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
    }
}
