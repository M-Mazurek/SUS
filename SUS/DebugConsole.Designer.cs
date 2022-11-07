namespace SUS
{
    partial class DebugConsole
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
            this.bar = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.RichTextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.errors = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar.Font = new System.Drawing.Font("Trebuchet MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.bar.ForeColor = System.Drawing.Color.GreenYellow;
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(200, 23);
            this.bar.TabIndex = 0;
            this.bar.Text = "Debug Console";
            this.bar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.output.ForeColor = System.Drawing.Color.GreenYellow;
            this.output.Location = new System.Drawing.Point(0, 23);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(200, 377);
            this.output.TabIndex = 1;
            this.output.TabStop = false;
            this.output.Text = "";
            this.output.MouseDown += new System.Windows.Forms.MouseEventHandler(this.output_MouseDown);
            // 
            // input
            // 
            this.input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.input.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.input.ForeColor = System.Drawing.Color.GreenYellow;
            this.input.Location = new System.Drawing.Point(0, 374);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(200, 26);
            this.input.TabIndex = 2;
            this.input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_KeyDown);
            // 
            // errors
            // 
            this.errors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.errors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.errors.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errors.ForeColor = System.Drawing.Color.OrangeRed;
            this.errors.Location = new System.Drawing.Point(0, 337);
            this.errors.Name = "errors";
            this.errors.Size = new System.Drawing.Size(200, 37);
            this.errors.TabIndex = 3;
            this.errors.Text = "Błędy konsoli będą wyświetlane na tym panelu.";
            this.errors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DebugConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(200, 400);
            this.Controls.Add(this.errors);
            this.Controls.Add(this.input);
            this.Controls.Add(this.output);
            this.Controls.Add(this.bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DebugConsole";
            this.Text = "DebugConsole";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label bar;
        private RichTextBox output;
        private TextBox input;
        private Label errors;
    }
}