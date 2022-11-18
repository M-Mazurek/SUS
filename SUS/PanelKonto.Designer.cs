namespace SUS
{
    partial class PanelKonto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelKonto));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.lbCreateAcc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelCreateAcc = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewAccPass = new System.Windows.Forms.TextBox();
            this.txtNewAccLogin = new System.Windows.Forms.TextBox();
            this.btnAddAcc = new System.Windows.Forms.Button();
            this.panelLogout = new System.Windows.Forms.Panel();
            this.btnDeleteAcc = new System.Windows.Forms.Button();
            this.lbCurrentAcc = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelChangePass = new System.Windows.Forms.Panel();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.menu1 = new SUS.Menu();
            this.mainPanel.SuspendLayout();
            this.panelCreateAcc.SuspendLayout();
            this.panelLogout.SuspendLayout();
            this.panelChangePass.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.mainPanel.Controls.Add(this.lbCreateAcc);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.panelCreateAcc);
            this.mainPanel.Controls.Add(this.panelLogout);
            this.mainPanel.Controls.Add(this.panelChangePass);
            this.mainPanel.Location = new System.Drawing.Point(12, 37);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(560, 360);
            this.mainPanel.TabIndex = 2;
            // 
            // lbCreateAcc
            // 
            this.lbCreateAcc.AutoSize = true;
            this.lbCreateAcc.ForeColor = System.Drawing.Color.White;
            this.lbCreateAcc.Location = new System.Drawing.Point(33, 183);
            this.lbCreateAcc.Name = "lbCreateAcc";
            this.lbCreateAcc.Size = new System.Drawing.Size(111, 15);
            this.lbCreateAcc.TabIndex = 9;
            this.lbCreateAcc.Text = "Stwórz Nowe Konto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(33, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Zmień Hasło";
            // 
            // panelCreateAcc
            // 
            this.panelCreateAcc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelCreateAcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCreateAcc.Controls.Add(this.label1);
            this.panelCreateAcc.Controls.Add(this.txtNewAccPass);
            this.panelCreateAcc.Controls.Add(this.txtNewAccLogin);
            this.panelCreateAcc.Controls.Add(this.btnAddAcc);
            this.panelCreateAcc.Location = new System.Drawing.Point(20, 190);
            this.panelCreateAcc.Name = "panelCreateAcc";
            this.panelCreateAcc.Size = new System.Drawing.Size(520, 150);
            this.panelCreateAcc.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(50, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Wybierz Typ Konta";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNewAccPass
            // 
            this.txtNewAccPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtNewAccPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewAccPass.ForeColor = System.Drawing.Color.White;
            this.txtNewAccPass.Location = new System.Drawing.Point(320, 55);
            this.txtNewAccPass.Name = "txtNewAccPass";
            this.txtNewAccPass.PlaceholderText = "Hasło";
            this.txtNewAccPass.Size = new System.Drawing.Size(150, 16);
            this.txtNewAccPass.TabIndex = 6;
            this.txtNewAccPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNewAccLogin
            // 
            this.txtNewAccLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtNewAccLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewAccLogin.ForeColor = System.Drawing.Color.White;
            this.txtNewAccLogin.Location = new System.Drawing.Point(320, 30);
            this.txtNewAccLogin.Name = "txtNewAccLogin";
            this.txtNewAccLogin.PlaceholderText = "Login";
            this.txtNewAccLogin.Size = new System.Drawing.Size(150, 16);
            this.txtNewAccLogin.TabIndex = 5;
            this.txtNewAccLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAddAcc
            // 
            this.btnAddAcc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnAddAcc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAcc.ForeColor = System.Drawing.Color.White;
            this.btnAddAcc.Location = new System.Drawing.Point(320, 88);
            this.btnAddAcc.Name = "btnAddAcc";
            this.btnAddAcc.Size = new System.Drawing.Size(150, 30);
            this.btnAddAcc.TabIndex = 7;
            this.btnAddAcc.Text = "Dodaj Konto";
            this.btnAddAcc.UseVisualStyleBackColor = true;
            this.btnAddAcc.Click += new System.EventHandler(this.btnAddAcc_Click);
            // 
            // panelLogout
            // 
            this.panelLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelLogout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogout.Controls.Add(this.btnDeleteAcc);
            this.panelLogout.Controls.Add(this.lbCurrentAcc);
            this.panelLogout.Controls.Add(this.lb1);
            this.panelLogout.Controls.Add(this.btnLogout);
            this.panelLogout.Location = new System.Drawing.Point(290, 20);
            this.panelLogout.Name = "panelLogout";
            this.panelLogout.Size = new System.Drawing.Size(250, 150);
            this.panelLogout.TabIndex = 5;
            // 
            // btnDeleteAcc
            // 
            this.btnDeleteAcc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnDeleteAcc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAcc.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAcc.Location = new System.Drawing.Point(130, 88);
            this.btnDeleteAcc.Name = "btnDeleteAcc";
            this.btnDeleteAcc.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteAcc.TabIndex = 6;
            this.btnDeleteAcc.Text = "Usuń Konto";
            this.btnDeleteAcc.UseVisualStyleBackColor = true;
            this.btnDeleteAcc.Click += new System.EventHandler(this.btnDeleteAcc_Click);
            // 
            // lbCurrentAcc
            // 
            this.lbCurrentAcc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbCurrentAcc.ForeColor = System.Drawing.Color.White;
            this.lbCurrentAcc.Location = new System.Drawing.Point(50, 50);
            this.lbCurrentAcc.Name = "lbCurrentAcc";
            this.lbCurrentAcc.Size = new System.Drawing.Size(150, 21);
            this.lbCurrentAcc.TabIndex = 5;
            this.lbCurrentAcc.Text = "Admin";
            this.lbCurrentAcc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb1
            // 
            this.lb1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb1.ForeColor = System.Drawing.Color.White;
            this.lb1.Location = new System.Drawing.Point(50, 25);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(150, 23);
            this.lb1.TabIndex = 4;
            this.lb1.Text = "Aktualne konto";
            this.lb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(20, 88);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 30);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Wyloguj";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelChangePass
            // 
            this.panelChangePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelChangePass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChangePass.Controls.Add(this.txtOldPass);
            this.panelChangePass.Controls.Add(this.btnChangePass);
            this.panelChangePass.Controls.Add(this.txtNewPass);
            this.panelChangePass.Location = new System.Drawing.Point(20, 20);
            this.panelChangePass.Name = "panelChangePass";
            this.panelChangePass.Size = new System.Drawing.Size(250, 150);
            this.panelChangePass.TabIndex = 4;
            // 
            // txtOldPass
            // 
            this.txtOldPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtOldPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOldPass.ForeColor = System.Drawing.Color.White;
            this.txtOldPass.Location = new System.Drawing.Point(50, 30);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.PlaceholderText = "Stare Hasło";
            this.txtOldPass.Size = new System.Drawing.Size(150, 16);
            this.txtOldPass.TabIndex = 0;
            this.txtOldPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnChangePass
            // 
            this.btnChangePass.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePass.ForeColor = System.Drawing.Color.White;
            this.btnChangePass.Location = new System.Drawing.Point(50, 88);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(150, 30);
            this.btnChangePass.TabIndex = 2;
            this.btnChangePass.Text = "Zmień Hasło";
            this.btnChangePass.UseVisualStyleBackColor = true;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // txtNewPass
            // 
            this.txtNewPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtNewPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPass.ForeColor = System.Drawing.Color.White;
            this.txtNewPass.Location = new System.Drawing.Point(50, 55);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.PlaceholderText = "Nowe Hasło";
            this.txtNewPass.Size = new System.Drawing.Size(150, 16);
            this.txtNewPass.TabIndex = 1;
            this.txtNewPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menu1
            // 
            this.menu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(584, 23);
            this.menu1.TabIndex = 3;
            // 
            // PanelKonto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.menu1);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 450);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "PanelKonto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUS : Konto";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.panelCreateAcc.ResumeLayout(false);
            this.panelCreateAcc.PerformLayout();
            this.panelLogout.ResumeLayout(false);
            this.panelChangePass.ResumeLayout(false);
            this.panelChangePass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel mainPanel;
        private Menu menu1;
        private Button btnChangePass;
        private TextBox txtOldPass;
        private TextBox txtNewPass;
        private Button btnLogout;
        private Panel panelChangePass;
        private Panel panelLogout;
        private Label lb1;
        private Label lbCurrentAcc;
        private Panel panelCreateAcc;
        private Button btnAddAcc;
        private TextBox txtNewAccLogin;
        private TextBox txtNewAccPass;
        private Label label1;
        private Label label2;
        private Label lbCreateAcc;
        private Button btnDeleteAcc;
    }
}