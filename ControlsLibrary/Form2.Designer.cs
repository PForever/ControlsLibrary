namespace ControlsLibrary
{
    partial class Form2
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
            this.defaultPage1 = new ControlsLibrary.DefaultPage();
            this.SuspendLayout();
            // 
            // defaultPage1
            // 
            this.defaultPage1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.defaultPage1.Location = new System.Drawing.Point(0, 400);
            this.defaultPage1.Name = "defaultPage1";
            this.defaultPage1.Size = new System.Drawing.Size(800, 50);
            this.defaultPage1.TabIndex = 0;
            this.defaultPage1.Paint += new System.Windows.Forms.PaintEventHandler(this.defaultPage1_Paint);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.defaultPage1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private DefaultPage defaultPage1;
    }
}