namespace NahrwallEditor
{
    partial class FrmTextureManipulator
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glViewTexture = new OpenTK.GLControl();
            this.flpItems = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glViewTexture);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flpItems);
            this.splitContainer1.Size = new System.Drawing.Size(643, 469);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.TabIndex = 0;
            // 
            // glViewTexture
            // 
            this.glViewTexture.BackColor = System.Drawing.Color.Black;
            this.glViewTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glViewTexture.Location = new System.Drawing.Point(0, 0);
            this.glViewTexture.Name = "glViewTexture";
            this.glViewTexture.Size = new System.Drawing.Size(384, 469);
            this.glViewTexture.TabIndex = 0;
            this.glViewTexture.VSync = false;
            this.glViewTexture.Paint += new System.Windows.Forms.PaintEventHandler(this.glViewTexture_Paint);
            // 
            // flpItems
            // 
            this.flpItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpItems.Location = new System.Drawing.Point(0, 0);
            this.flpItems.Name = "flpItems";
            this.flpItems.Size = new System.Drawing.Size(255, 469);
            this.flpItems.TabIndex = 0;
            // 
            // FrmTextureManipulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 469);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmTextureManipulator";
            this.ShowInTaskbar = false;
            this.Text = "Texture Manipulator";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private OpenTK.GLControl glViewTexture;
        private System.Windows.Forms.FlowLayoutPanel flpItems;

    }
}