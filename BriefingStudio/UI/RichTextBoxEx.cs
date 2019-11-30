using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BriefingStudio.UI
{
    class RichTextBoxEx : RichTextBox
    {
        private const int WM_SETREDRAW = 0x0b;
        private const int WM_NOTIFY = 0x4e;
        private const int WM_USER = 0x400;
        private const int WM_REFLECT = 0x2000;
        private const int EM_GETEVENTMASK = (WM_USER + 59);
        private const int EM_SETEVENTMASK = (WM_USER + 69);
        private const int EN_CLIPFORMAT = 0x712;
        private const int ENM_CLIPFORMAT = 0x00000080;
        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }

        public event EventHandler Pasted;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            IntPtr eventMask = SendMessage(this.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
            SendMessage(this.Handle, EM_SETEVENTMASK, 0, (IntPtr)(eventMask.ToInt64() | ENM_CLIPFORMAT));
        }

        public void SuspendDrawing()
        {
            SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }

        public void ResumeDrawing()
        {
            SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            // rich text paste notification Win8+
            // force update
            if (m.Msg == WM_REFLECT + WM_NOTIFY)
            {
                var notify = (NMHDR)Marshal.PtrToStructure(m.LParam, typeof(NMHDR));
                if (notify.code == EN_CLIPFORMAT)
                {
                    Pasted?.Invoke(this, new EventArgs());
                }
            }
            base.WndProc(ref m);
            /*

        private static uint HIWORD(UInt32 inp)
        {
            return (inp >> 16);
        }

        private const int WM_COMMAND = 0x111;
            if (m.Msg == (WM_REFLECT + WM_COMMAND))
            {
                //System.Diagnostics.Debug.WriteLine("WM_COMMAND; cmd=" + HIWORD((uint)m.WParam.ToInt32()).ToString("X"));
                // rich text paste notification Win8+
                // force update
                if (HIWORD((uint)m.WParam.ToInt32()) == EN_CLIPFORMAT)
                {
                    Pasted?.Invoke(this, new EventArgs());
                }
            }

            */
        }
    }
}
