using System.Runtime.InteropServices;

namespace SUS
{
    public partial class DebugConsole : Form
    {
        public static DebugConsole Instance { get; private set; } = new DebugConsole();
        private List<(string Text, bool IsInput)> _textCache = new();

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
            Say("Konsola gotowa.");
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

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (!ParseCommand(input.Text, out string err))
            {
                errors.Text = err;
                return;
            }

            Say(input.Text, true);
            input.Clear();

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void Say(string text, bool isInput = false, bool fromCache = false)
        {
            output.AppendText($"{(isInput ? "-" : ">")} {text}{Environment.NewLine}");

            if (_textCache.Count == 0 || fromCache)
                return;
            foreach (var item in _textCache)
            {
                Say(item.Text, item.IsInput, true);
            }
            _textCache.Clear();
        }

        private void SayLater(string text, bool isInput = false)
        {
            _textCache.Add((text, isInput));
        }

        private bool ParseCommand(string cmd, out string err)
        {
            string[] args = cmd.Split(' ');
            string c = args[0];
            args = args[1..];

            switch (c)
            {
                case "lt":
                    return ParseLT(args, out err);
                default:
                    err = $"Komenda \"{c}\" nie istnieje.";
                    return false;
            }
        }

        private bool ParseLT(string[] args, out string err)
        {
            if (args.Length == 0)
            {
                err = "Nie podano argumentu <login>.";
                return false;
            }
            if (args.Length == 1)
            {
                err = "Nie podano argumentu <hasło>.";
                return false;
            }
            if (args.Length > 2)
            {
                err = "Komenda \"lt\" oczekuje 2 argumentów.";
                return false;
            }

            if (Global.Login(args[0], args[1], out err))
                SayLater("Test logowania zakończono powodzeniem.");
            else
                SayLater(err);
            return true;
        }
    }
}
