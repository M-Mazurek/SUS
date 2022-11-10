using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;

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

        delegate bool ParseCommandDelegate(string[] args, out string err);

        public DebugConsole()
        {
            if (Instance is not null)
                throw new Exception("Only one debug console instance can be present at the same time.");
            ShowInTaskbar = false;
            InitializeComponent();
            HideCaret(output.Handle);
            Say("Konsola gotowa.");
        }

        void bar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        void output_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(output.Handle);
        }

        void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemtilde)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode != Keys.Enter)
                return;
            if (!ParseCommand(input.Text, out string err))
            {
                errors.Text = err;
                return;
            }

            Say(input.Text, true);
            input.Clear();
            errors.Text = "";

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        void Say(string text, bool isInput = false, bool fromCache = false)
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

        void SayLater(string text, bool isInput = false)
        {
            _textCache.Add((text, isInput));
        }

        bool ParseCommand(string cmd, out string err)
        {
            string[] args = cmd.Split(' ');
            string c = args[0];
            args = args[1..];

            var info = typeof(DebugConsole).GetMethod($"Parse{c.ToUpper()}",
                                                                     BindingFlags.NonPublic | BindingFlags.Instance);
            if (info is null)
            {
                err = $"Komenda \"{c}\" nie istnieje.";
                return false;
            }
            ParseCommandDelegate del = (ParseCommandDelegate)Delegate.CreateDelegate(typeof(ParseCommandDelegate),
                                                                         this,
                                                                         info);
            return del(args, out err);
        }
#pragma warning disable IDE0051
        bool ParseLT(string[] args, out string err)
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

        bool ParseRE(string[] args, out string err)
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
            if (args.Length == 2)
            {
                err = "Nie podano argumentu <typ>.";
                return false;
            }
            if (args.Length > 3)
            {
                err = "Komenda \"re\" oczekuje 2 argumentów.";
                return false;
            }
            if (!int.TryParse(args[2], out int type))
            {
                err = "Argument <typ> musi być liczbą całkowitą.";
                return false;
            }

            if (Global.Register(args[0], args[1], type, out err))
                SayLater("Zarejestrowano nowe konto.");
            else
                SayLater(err);
            return true;
        }

        bool ParseSP(string[] args, out string err)
        {
            if (args.Length == 0)
            {
                err = "Nie podano argumentu <hasło>.";
                return false;
            }
            if (args.Length > 1)
            {
                err = "Komenda \"sp\" oczekuje 1 argumentu.";
                return false;
            }

            if (Global.SetPass(args[0], out err))
                SayLater("Zmieniono hasło.");
            else
                SayLater(err);
            return true;
        }

        bool ParseAS(string[] args, out string err)
        {
            if (args.Length == 0)
            {
                err = "Nie podano argumentu <nazwa>.";
                return false;
            }
            if (args.Length > 1)
            {
                err = "Komenda \"as\" oczekuje 1 argumentu.";
                return false;
            }

            if (Global.AddSeller(args[0], out err))
                SayLater("Dodano nowego sprzedawcę.");
            else
                SayLater(err);
            return true;
        }

        bool ParseLS(string[] args, out string err)
        {
            err = String.Empty;
            if (args.Length > 0)
            {
                err = "Komenda \"ls\" nie oczekuje żadnych argumentów.";
                return false;
            }

            Global.GetSellers().Select(x => $"{x.Id} - {x.Name}").ToList().ForEach(x => SayLater(x));
            return true;
        }

        bool ParseAW(string[] args, out string err)
        {
            if (args.Length == 0)
            {
                err = "Nie podano argumentu <nazwa>.";
                return false;
            }
            if (args.Length == 1)
            {
                err = "Nie podano argumentu <id_sprzedawcy>.";
                return false;
            }
            if (args.Length == 2)
            {
                err = "Nie podano argumentu <cena>.";
                return false;
            }
            if (args.Length > 3)
            {
                err = "Komenda \"aw\" oczekuje 3 argumentów.";
                return false;
            }
            if (!int.TryParse(args[1], out int sellerId))
            {
                err = "Id sprzedawcy musi być liczbą całkowitą";
                return false;
            }
            //args[2] = args[2].Replace('.', ',');
            if (!float.TryParse(args[2], 
                                NumberStyles.AllowDecimalPoint, 
                                CultureInfo.InvariantCulture.NumberFormat, 
                                out float price))
            {
                err = "Cena musi być liczbą zmiennoprzecinkową.";
                return false;
            }

            if (Global.AddWare(args[0], price, sellerId, out err))
                SayLater("Dodano nowy towar.");
            else
                SayLater(err);
            return true;
        }

        bool ParseLW(string[] args, out string err)
        {
            err = String.Empty;
            if (args.Length == 0)
            {
                err = "Nie podano argumentu <id_sprzedawcy>";
                return false;
            }
            if (args.Length > 1)
            {
                err = "Komenda \"lw\" oczekuje 1 argumentu.";
                return false;
            }
            if (!int.TryParse(args[0], out int sellerId))
            {
                err = "Id sprzedawcy musi być liczbą całkowitą";
                return false;
            }

            Global.GetWaresFrom(sellerId).Select(x => $"{x.Id} - {x.Name} [{x.Price}]").ToList().ForEach(x => SayLater(x));
            return true;
        }
    }
}