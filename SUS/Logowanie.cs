namespace SUS
{
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
            menu.Renderer = new Renderer(); //override menuStrip with our functions
            MenuStripFunctions.CreateEvents(menu);
        }
    }
}