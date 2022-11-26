namespace SUS
{
    partial class PanelKorekty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelKorekty));
            this.menu1 = new SUS.Menu();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbData = new System.Windows.Forms.Label();
            this.lbKorektaZamow = new System.Windows.Forms.Label();
            this.lbFirma = new System.Windows.Forms.Label();
            this.lbNrKorekty = new System.Windows.Forms.Label();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTowar = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu1
            // 
            this.menu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(984, 23);
            this.menu1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel1.Controls.Add(this.lbData);
            this.panel1.Controls.Add(this.lbKorektaZamow);
            this.panel1.Controls.Add(this.lbFirma);
            this.panel1.Controls.Add(this.lbNrKorekty);
            this.panel1.Location = new System.Drawing.Point(243, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 30);
            this.panel1.TabIndex = 3;
            // 
            // lbData
            // 
            this.lbData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbData.ForeColor = System.Drawing.Color.White;
            this.lbData.Location = new System.Drawing.Point(590, 0);
            this.lbData.Name = "lbData";
            this.lbData.Size = new System.Drawing.Size(132, 30);
            this.lbData.TabIndex = 3;
            this.lbData.Text = "Data Zamówienia";
            this.lbData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbKorektaZamow
            // 
            this.lbKorektaZamow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbKorektaZamow.ForeColor = System.Drawing.Color.White;
            this.lbKorektaZamow.Location = new System.Drawing.Point(120, 0);
            this.lbKorektaZamow.Name = "lbKorektaZamow";
            this.lbKorektaZamow.Size = new System.Drawing.Size(160, 30);
            this.lbKorektaZamow.TabIndex = 2;
            this.lbKorektaZamow.Text = "Korekta do Zamówienia";
            this.lbKorektaZamow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFirma
            // 
            this.lbFirma.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbFirma.ForeColor = System.Drawing.Color.White;
            this.lbFirma.Location = new System.Drawing.Point(280, 0);
            this.lbFirma.Name = "lbFirma";
            this.lbFirma.Size = new System.Drawing.Size(310, 30);
            this.lbFirma.TabIndex = 1;
            this.lbFirma.Text = "Firma";
            this.lbFirma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNrKorekty
            // 
            this.lbNrKorekty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbNrKorekty.ForeColor = System.Drawing.Color.White;
            this.lbNrKorekty.Location = new System.Drawing.Point(0, 0);
            this.lbNrKorekty.Name = "lbNrKorekty";
            this.lbNrKorekty.Size = new System.Drawing.Size(120, 30);
            this.lbNrKorekty.TabIndex = 0;
            this.lbNrKorekty.Text = "Numer Korekty";
            this.lbNrKorekty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelOrders
            // 
            this.panelOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelOrders.Location = new System.Drawing.Point(243, 60);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(722, 490);
            this.panelOrders.TabIndex = 4;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelFilters.Controls.Add(this.btnFilter);
            this.panelFilters.Controls.Add(this.dtpEnd);
            this.panelFilters.Controls.Add(this.dtpStart);
            this.panelFilters.Controls.Add(this.label3);
            this.panelFilters.Controls.Add(this.label2);
            this.panelFilters.Controls.Add(this.txtTowar);
            this.panelFilters.Controls.Add(this.btnCreate);
            this.panelFilters.Controls.Add(this.label1);
            this.panelFilters.Location = new System.Drawing.Point(20, 30);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(200, 520);
            this.panelFilters.TabIndex = 5;
            // 
            // btnFilter
            // 
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(27, 442);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(150, 30);
            this.btnFilter.TabIndex = 21;
            this.btnFilter.Text = "Filtruj";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEnd.Location = new System.Drawing.Point(0, 247);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 23);
            this.dtpEnd.TabIndex = 20;
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStart.Location = new System.Drawing.Point(0, 147);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 23);
            this.dtpStart.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Wybierz Datę Końcową";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Wybierz Datę Początkową";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTowar
            // 
            this.txtTowar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtTowar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTowar.ForeColor = System.Drawing.Color.White;
            this.txtTowar.Location = new System.Drawing.Point(15, 45);
            this.txtTowar.Name = "txtTowar";
            this.txtTowar.Size = new System.Drawing.Size(170, 23);
            this.txtTowar.TabIndex = 16;
            this.txtTowar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTowar.TextChanged += new System.EventHandler(this.txtTowar_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(27, 478);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(150, 30);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Utwórz korektę";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numer zamówienia";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelKorekty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelOrders);
            this.Controls.Add(this.menu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "PanelKorekty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUS : Korekty";
            this.panel1.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Menu menu1;
        private Panel panel1;
        private Label lbData;
        private Label lbKorektaZamow;
        private Label lbFirma;
        private Label lbNrKorekty;
        private Panel panelOrders;
        private Panel panelFilters;
        private Button btnCreate;
        private Label label1;
        private TextBox txtTowar;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private Label label3;
        private Label label2;
        private Button btnFilter;
    }
}