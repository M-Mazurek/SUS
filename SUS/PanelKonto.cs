using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUS
{
    public partial class PanelKonto : Form
    {
        private CustomCombo? customCombo;
        public PanelKonto()
        {
            InitializeComponent();

            lbCurrentAcc.Text = "admin"; // change with current account login name

            panelCreateAcc.Visible = false;
            lbCreateAcc.Visible = false;
            mainPanel.Size = new(560, 190);
            MaximumSize = new(600, 285);
            MinimumSize = new(600, 285);
            Size = new(600, 285);

            CreateAdminOptions();
        }

        private void CreateAdminOptions()
        {
            MaximumSize = new(600, 450);
            MinimumSize = new(600, 450);
            Size = new(600, 450);
            mainPanel.Size = new(560, 360);
            panelCreateAcc.Visible = true;
            lbCreateAcc.Visible = true;

            customCombo = new CustomCombo()
            {
                Name = "accType",
                Location = new(50, 30),
                Size = new(150, 23),
                BackColor = Color.FromArgb(60, 60, 60),
                OuterBorderColor = Color.DimGray,
                InnerBorderColor = BackColor,
                ArrowBackgroundColor = Color.FromArgb(36, 36, 36),
                ForeColor = Color.White,
                Items = { "Pracownik", "Kierownik", "Administrator" }, // viable account types
            };
            panelCreateAcc.Controls.Add(customCombo);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Global.Logout();
            ExtensionMethods.SwitchForm(this, new Logowanie());
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtOldPass.Text) || String.IsNullOrWhiteSpace(txtNewPass.Text))
                return;

            if (txtOldPass.Text != "admin") // change "admin" with account password from database
                return;

            if (txtOldPass.Text == txtNewPass.Text) // same password dont change
                return;
            
            //txtNewPass.Text; // set account pass to new value
            MessageBox.Show("Pomyślnie zmieniono hasło.", "Zmieniono Hasło", MessageBoxButtons.OK);
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNewAccLogin.Text) || String.IsNullOrWhiteSpace(txtNewAccPass.Text))
                return;

            if (txtNewAccLogin.Text == "admin") // database check if has this user
                return;

            if (customCombo!.SelectedItem == null)
                return;

            if (MessageBox.Show($"Czy napewno chcesz utworzyć konto '{customCombo.SelectedItem}' o loginie '{txtNewAccLogin.Text}'?", "Utwórz konto", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // add account to database
            MessageBox.Show($"Pomyślnie utworzono konto '{customCombo.SelectedItem}' o loginie '{txtNewAccLogin.Text}'.", "Utworzono konto", MessageBoxButtons.OK);
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            if (lbCurrentAcc.Text == "admin") // keep admin safe )
                return;

            if (MessageBox.Show($"Czy napewno chcesz usunąć konto 'ADMIN' o loginie '{lbCurrentAcc.Text}'?", "Usuń konto", MessageBoxButtons.YesNo) == DialogResult.No) // change 'ADMIN' with current account type
                return;

            // delete account from database
            MessageBox.Show($"Pomyślnie usunięto konto 'ADMIN' o loginie '{lbCurrentAcc.Text}'.", "Usunięto konto", MessageBoxButtons.OK);
            ExtensionMethods.SwitchForm(this, new Logowanie());
        }
    }
}