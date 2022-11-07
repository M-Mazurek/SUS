using System.Runtime.InteropServices;

namespace SUS
{
    public partial class DebugConsole : Form
    {
        public static DebugConsole Instance { get; private set; } = new DebugConsole();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public DebugConsole()
        {
            if (Instance is not null)
                throw new Exception("Only one debug console instance can be present at the same time.");
            ShowInTaskbar = false;
            InitializeComponent();
            HideCaret(output.Handle);
        }

        private void bar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void output_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(output.Handle);
        }
    }
}
