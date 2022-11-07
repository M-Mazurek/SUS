namespace SUS
{
    partial class Menu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.zamowieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magazynyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korektyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zamowieniaToolStripMenuItem,
            this.magazynyToolStripMenuItem,
            this.korektyToolStripMenuItem,
            this.historiaToolStripMenuItem,
            this.kontoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(1000, 23);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripMenuItem zamowieniaToolStripMenuItem;
        private ToolStripMenuItem magazynyToolStripMenuItem;
        private ToolStripMenuItem korektyToolStripMenuItem;
        private ToolStripMenuItem historiaToolStripMenuItem;
        private ToolStripMenuItem kontoToolStripMenuItem;
        private MenuStrip menuStrip1;
    }
}
