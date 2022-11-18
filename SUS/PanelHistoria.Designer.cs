namespace SUS
{
    partial class PanelHistoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelHistoria));
            this.menu1 = new SUS.Menu();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.btnOrderHistory = new System.Windows.Forms.Button();
            this.btnActionHistory = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu1
            // 
            this.menu1.AutoSize = true;
            this.menu1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(984, 24);
            this.menu1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(243, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 30);
            this.panel1.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(590, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 30);
            this.label8.TabIndex = 3;
            this.label8.Text = "Data Wykonania";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(440, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 30);
            this.label7.TabIndex = 2;
            this.label7.Text = "Wykonawca";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(440, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nazwa Akcji";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelOrders
            // 
            this.panelOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelOrders.Location = new System.Drawing.Point(243, 60);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(722, 490);
            this.panelOrders.TabIndex = 5;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelFilters.Controls.Add(this.btnActionHistory);
            this.panelFilters.Controls.Add(this.btnClearHistory);
            this.panelFilters.Controls.Add(this.btnOrderHistory);
            this.panelFilters.Location = new System.Drawing.Point(20, 30);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(200, 126);
            this.panelFilters.TabIndex = 4;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearHistory.ForeColor = System.Drawing.Color.White;
            this.btnClearHistory.Location = new System.Drawing.Point(27, 84);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(150, 30);
            this.btnClearHistory.TabIndex = 4;
            this.btnClearHistory.Text = "Wyczyść historię";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // btnOrderHistory
            // 
            this.btnOrderHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnOrderHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderHistory.ForeColor = System.Drawing.Color.White;
            this.btnOrderHistory.Location = new System.Drawing.Point(27, 48);
            this.btnOrderHistory.Name = "btnOrderHistory";
            this.btnOrderHistory.Size = new System.Drawing.Size(150, 30);
            this.btnOrderHistory.TabIndex = 0;
            this.btnOrderHistory.Text = "Historia zamówień";
            this.btnOrderHistory.UseVisualStyleBackColor = true;
            this.btnOrderHistory.Click += new System.EventHandler(this.btnOrderHistory_Click);
            // 
            // btnActionHistory
            // 
            this.btnActionHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(212)))), ((int)(((byte)(113)))));
            this.btnActionHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActionHistory.ForeColor = System.Drawing.Color.White;
            this.btnActionHistory.Location = new System.Drawing.Point(27, 12);
            this.btnActionHistory.Name = "btnActionHistory";
            this.btnActionHistory.Size = new System.Drawing.Size(150, 30);
            this.btnActionHistory.TabIndex = 5;
            this.btnActionHistory.Text = "Historia akcji";
            this.btnActionHistory.UseVisualStyleBackColor = true;
            this.btnActionHistory.Click += new System.EventHandler(this.btnActionHistory_Click);
            // 
            // PanelHistoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelOrders);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.menu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "PanelHistoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUS : Historia";
            this.panel1.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Menu menu1;
        private Panel panel1;
        private Label label8;
        private Label label7;
        private Label label5;
        private Panel panelOrders;
        private Panel panelFilters;
        private Button btnClearHistory;
        private Button btnOrderHistory;
        private Button btnActionHistory;
    }
}