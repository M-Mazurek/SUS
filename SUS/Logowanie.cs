using System.Numerics;

namespace SUS
{
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
            wrongCredentials.Visible = false;
            wrongCredentials.ForeColor = Color.FromArgb(255, 255, 50 ,0);
            ExtensionMethods.StartAnim(this);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // credentials check /w database
            if (!Global.Login(txtLogin.Text, txtPassword.Text, out string err))
            {
                wrongCredentials.Text = err;
                wrongCredentials.Visible = true;
                return;
            }

            wrongCredentials.Visible = false;

            ExtensionMethods.SwitchForm(this, new PanelKonto());
        }
    }
}