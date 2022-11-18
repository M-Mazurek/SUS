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
            lbCurrentAcc.Text = Global.CurrentUser.Login; // change with current account login name

            panelCreateAcc.Visible = false;
            lbCreateAcc.Visible = false;
            mainPanel.Size = new(560, 190);
            MaximumSize = new(600, 285);
            MinimumSize = new(600, 285);
            Size = new(600, 285);

            CreateAdminOptions();
            ExtensionMethods.StartAnim(this);
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
                OuterBorderColor = Color.DimGray, //Color.FromArgb(99, 212, 113)
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
            {
                MessageBox.Show($"Błąd przy zmianie hasła. Błąd:\nNie wpisane dane.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            if (txtOldPass.Text != Global.GetPass(Global.CurrentUser.Login)) // wrong old password
            {
                MessageBox.Show($"Błąd przy zmianie hasła. Błąd:\nNiepoprawne stare hasło.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            if (txtOldPass.Text == txtNewPass.Text) // same password dont change
            {
                MessageBox.Show($"Błąd przy zmianie hasła. Błąd:\nHasła są jednakowe.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            //txtNewPass.Text; // set account pass to new value
            string error;
            if (!Global.SetPass(txtNewPass.Text, out error)) 
            {
                MessageBox.Show($"Błąd przy zmianie hasła. Błąd:\n{error}", "Błąd", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Pomyślnie zmieniono hasło.", "Zmieniono Hasło", MessageBoxButtons.OK);
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNewAccLogin.Text) || String.IsNullOrWhiteSpace(txtNewAccPass.Text)) 
            {
                MessageBox.Show($"Błąd przy tworzeniu konta. Błąd:\nNie wpisane dane.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            if (Global.HasUser(txtNewAccLogin.Text)) // database check if has this user
            {
                MessageBox.Show($"Błąd przy tworzeniu konta. Błąd:\nUżytkownik o takim loginie już istnieje.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            if (customCombo!.SelectedItem == null) 
            {
                MessageBox.Show($"Błąd przy tworzeniu konta. Błąd:\nNie wybrano typu konta.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show($"Czy napewno chcesz utworzyć konto typu '{customCombo.SelectedItem}' o loginie '{txtNewAccLogin.Text}'?", "Utwórz konto", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // add account to database
            string error;
            if (!Global.Register(txtNewAccLogin.Text, txtNewAccPass.Text, customCombo.SelectedIndex, out error))
            {
                MessageBox.Show($"Błąd przy tworzeniu hasła. Błąd:\n{error}", "Błąd", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show($"Pomyślnie utworzono konto '{customCombo.SelectedItem}' o loginie '{txtNewAccLogin.Text}'.", "Utworzono konto", MessageBoxButtons.OK);
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            if (lbCurrentAcc.Text == "admin") // keep admin safe ){}
            {
                MessageBox.Show($"Błąd przy usuwaniu konta. Błąd:\nNie możesz usunąć podstawowego konta.", "Błąd", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show($"Czy napewno chcesz usunąć konto o typie '{Global.CurrentUser.Type}' o loginie '{Global.CurrentUser.Login}'?", "Usuń konto", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // delete account from database
            Global.DeleteUser(Global.CurrentUser.Login);
            MessageBox.Show($"Pomyślnie usunięto konto o typie '{Global.CurrentUser.Type}' o loginie '{Global.CurrentUser.Login}'.", "Usunięto konto", MessageBoxButtons.OK);
            ExtensionMethods.SwitchForm(this, new Logowanie());
        }
    }
}