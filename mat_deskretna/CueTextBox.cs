using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YOUR_NAMESPACE_HERE
{
    public class CueTextBox : TextBox
    {
        private static class NativeMethods
        {
            private const uint ECM_FIRST = 0x1500;
            internal const uint EM_SETCUEBANNER = ECM_FIRST + 1;

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, 
                                                    IntPtr wParam, string lParam);
        }

        private string cue;

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Cut text or hint text for TextBox, that displayed when TextBox is empty.")]
        public string Cue
        {
            get => cue;

            set
            {
                cue = value;
                UpdateCue();
            }
        }

        private void UpdateCue()
        {
            if (IsHandleCreated && cue != null)
            {
                NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, (IntPtr)1, cue);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateCue();
        }
    }
}
