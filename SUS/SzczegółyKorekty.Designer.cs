namespace SUS
{
    partial class SzczegółyKorekty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SzczegółyKorekty));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbSuma = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbCena = new System.Windows.Forms.Label();
            this.lbIlosc = new System.Windows.Forms.Label();
            this.lbCenaSz = new System.Windows.Forms.Label();
            this.lbTowar = new System.Windows.Forms.Label();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtKorektaId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menu1 = new SUS.Menu();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel2.Controls.Add(this.lbSuma);
            this.panel2.Location = new System.Drawing.Point(243, 520);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(722, 30);
            this.panel2.TabIndex = 9;
            // 
            // lbSuma
            // 
            this.lbSuma.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbSuma.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbSuma.ForeColor = System.Drawing.Color.White;
            this.lbSuma.Location = new System.Drawing.Point(0, 0);
            this.lbSuma.Name = "lbSuma";
            this.lbSuma.Size = new System.Drawing.Size(722, 30);
            this.lbSuma.TabIndex = 0;
            this.lbSuma.Text = "Suma:";
            this.lbSuma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel1.Controls.Add(this.lbCena);
            this.panel1.Controls.Add(this.lbIlosc);
            this.panel1.Controls.Add(this.lbCenaSz);
            this.panel1.Controls.Add(this.lbTowar);
            this.panel1.Location = new System.Drawing.Point(243, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 30);
            this.panel1.TabIndex = 8;
            // 
            // lbCena
            // 
            this.lbCena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbCena.ForeColor = System.Drawing.Color.White;
            this.lbCena.Location = new System.Drawing.Point(602, 0);
            this.lbCena.Name = "lbCena";
            this.lbCena.Size = new System.Drawing.Size(120, 30);
            this.lbCena.TabIndex = 3;
            this.lbCena.Text = "Cena";
            this.lbCena.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbIlosc
            // 
            this.lbIlosc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbIlosc.ForeColor = System.Drawing.Color.White;
            this.lbIlosc.Location = new System.Drawing.Point(505, 0);
            this.lbIlosc.Name = "lbIlosc";
            this.lbIlosc.Size = new System.Drawing.Size(100, 30);
            this.lbIlosc.TabIndex = 2;
            this.lbIlosc.Text = "Ilość";
            this.lbIlosc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCenaSz
            // 
            this.lbCenaSz.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbCenaSz.ForeColor = System.Drawing.Color.White;
            this.lbCenaSz.Location = new System.Drawing.Point(378, 0);
            this.lbCenaSz.Name = "lbCenaSz";
            this.lbCenaSz.Size = new System.Drawing.Size(130, 30);
            this.lbCenaSz.TabIndex = 1;
            this.lbCenaSz.Text = "Cena za Sztukę";
            this.lbCenaSz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTowar
            // 
            this.lbTowar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbTowar.ForeColor = System.Drawing.Color.White;
            this.lbTowar.Location = new System.Drawing.Point(0, 0);
            this.lbTowar.Name = "lbTowar";
            this.lbTowar.Size = new System.Drawing.Size(380, 30);
            this.lbTowar.TabIndex = 0;
            this.lbTowar.Text = "Towar";
            this.lbTowar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelOrders
            // 
            this.panelOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelOrders.Location = new System.Drawing.Point(243, 60);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(722, 490);
            this.panelOrders.TabIndex = 7;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelFilters.Controls.Add(this.btnConfirm);
            this.panelFilters.Controls.Add(this.btnBack);
            this.panelFilters.Controls.Add(this.txtKorektaId);
            this.panelFilters.Controls.Add(this.label2);
            this.panelFilters.Controls.Add(this.txtState);
            this.panelFilters.Controls.Add(this.txtDate);
            this.panelFilters.Controls.Add(this.txtCompany);
            this.panelFilters.Controls.Add(this.label4);
            this.panelFilters.Controls.Add(this.label3);
            this.panelFilters.Controls.Add(this.label1);
            this.panelFilters.Location = new System.Drawing.Point(20, 30);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(200, 520);
            this.panelFilters.TabIndex = 6;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(27, 277);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(150, 30);
            this.btnConfirm.TabIndex = 12;
            this.btnConfirm.Text = "Potwierdź";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Visible = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(27, 475);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(150, 30);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Powrót";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtKorektaId
            // 
            this.txtKorektaId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtKorektaId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKorektaId.Enabled = false;
            this.txtKorektaId.ForeColor = System.Drawing.Color.White;
            this.txtKorektaId.Location = new System.Drawing.Point(15, 358);
            this.txtKorektaId.Name = "txtKorektaId";
            this.txtKorektaId.Size = new System.Drawing.Size(170, 23);
            this.txtKorektaId.TabIndex = 8;
            this.txtKorektaId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Numer Korekty";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtState
            // 
            this.txtState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtState.Enabled = false;
            this.txtState.ForeColor = System.Drawing.Color.White;
            this.txtState.Location = new System.Drawing.Point(15, 258);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(170, 23);
            this.txtState.TabIndex = 6;
            this.txtState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDate.Enabled = false;
            this.txtDate.ForeColor = System.Drawing.Color.White;
            this.txtDate.Location = new System.Drawing.Point(15, 158);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(170, 23);
            this.txtDate.TabIndex = 5;
            this.txtDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCompany
            // 
            this.txtCompany.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompany.Enabled = false;
            this.txtCompany.ForeColor = System.Drawing.Color.White;
            this.txtCompany.Location = new System.Drawing.Point(15, 58);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(170, 23);
            this.txtCompany.TabIndex = 4;
            this.txtCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Stan Zamówienia";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data zamówienia";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa Firmy";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menu1
            // 
            this.menu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(984, 23);
            this.menu1.TabIndex = 10;
            // 
            // SzczegółyKorekty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.menu1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelOrders);
            this.Controls.Add(this.panelFilters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "SzczegółyKorekty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUS : Szczegóły Korekty";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel2;
        private Label lbSuma;
        private Panel panel1;
        private Label lbCena;
        private Label lbIlosc;
        private Label lbCenaSz;
        private Label lbTowar;
        private Panel panelOrders;
        private Panel panelFilters;
        private Button btnConfirm;
        private Button btnBack;
        private TextBox txtKorektaId;
        private Label label2;
        private TextBox txtState;
        private TextBox txtDate;
        private TextBox txtCompany;
        private Label label4;
        private Label label3;
        private Label label1;
        private Menu menu1;
    }
}