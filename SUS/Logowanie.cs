namespace SUS
{
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Hide();
            new PanelKonto().ShowDialog();
            Close();
        }
    }
}