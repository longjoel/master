namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.textGrid1 = new TextGridControl.TextGrid();
            this.SuspendLayout();
            // 
            // textGrid1
            // 
            this.textGrid1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textGrid1.Font = new System.Drawing.Font("Courier New", 12F);
            this.textGrid1.Location = new System.Drawing.Point(0, 0);
            this.textGrid1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textGrid1.Name = "textGrid1";
            this.textGrid1.Size = new System.Drawing.Size(489, 315);
            this.textGrid1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 315);
            this.Controls.Add(this.textGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private TextGridControl.TextGrid textGrid1;
    }
}

