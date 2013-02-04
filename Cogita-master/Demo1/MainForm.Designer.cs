namespace Demo1
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.PrimaryViewGLControl = new OpenTK.GLControl();
            this.GameInstanceTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // PrimaryViewGLControl
            // 
            this.PrimaryViewGLControl.BackColor = System.Drawing.Color.Black;
            this.PrimaryViewGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrimaryViewGLControl.Location = new System.Drawing.Point(0, 0);
            this.PrimaryViewGLControl.Name = "PrimaryViewGLControl";
            this.PrimaryViewGLControl.Size = new System.Drawing.Size(504, 380);
            this.PrimaryViewGLControl.TabIndex = 0;
            this.PrimaryViewGLControl.VSync = true;
            // 
            // GameInstanceTimer
            // 
            this.GameInstanceTimer.Interval = 1;
            this.GameInstanceTimer.Tick += new System.EventHandler(this.GameInstanceTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 380);
            this.Controls.Add(this.PrimaryViewGLControl);
            this.Name = "MainForm";
            this.Text = "Cogita";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl PrimaryViewGLControl;
        private System.Windows.Forms.Timer GameInstanceTimer;
    }
}

