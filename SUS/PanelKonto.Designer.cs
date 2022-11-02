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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.zamowieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magazynyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korektyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zamowieniaToolStripMenuItem,
            this.magazynyToolStripMenuItem,
            this.korektyToolStripMenuItem,
            this.historiaToolStripMenuItem,
            this.kontoToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menu.Size = new System.Drawing.Size(984, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // zamowieniaToolStripMenuItem
            // 
            this.zamowieniaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.zamowieniaToolStripMenuItem.Name = "zamowieniaToolStripMenuItem";
            this.zamowieniaToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.zamowieniaToolStripMenuItem.Text = "Zamówienia";
            // 
            // magazynyToolStripMenuItem
            // 
            this.magazynyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.magazynyToolStripMenuItem.Name = "magazynyToolStripMenuItem";
            this.magazynyToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.magazynyToolStripMenuItem.Text = "Magazyny";
            // 
            // korektyToolStripMenuItem
            // 
            this.korektyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.korektyToolStripMenuItem.Name = "korektyToolStripMenuItem";
            this.korektyToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.korektyToolStripMenuItem.Text = "Korekty";
            // 
            // historiaToolStripMenuItem
            // 
            this.historiaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.historiaToolStripMenuItem.Name = "historiaToolStripMenuItem";
            this.historiaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.historiaToolStripMenuItem.Text = "Historia";
            // 
            // kontoToolStripMenuItem
            // 
            this.kontoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.kontoToolStripMenuItem.Name = "kontoToolStripMenuItem";
            this.kontoToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.kontoToolStripMenuItem.Text = "Konto";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 512);
            this.panel1.TabIndex = 2;
            // 
            // PanelKonto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "PanelKonto";
            this.Text = "SUS : Konto";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menu;
        private ToolStripMenuItem zamowieniaToolStripMenuItem;
        private ToolStripMenuItem magazynyToolStripMenuItem;
        private ToolStripMenuItem korektyToolStripMenuItem;
        private ToolStripMenuItem historiaToolStripMenuItem;
        private ToolStripMenuItem kontoToolStripMenuItem;
        private Panel panel1;
    }
}