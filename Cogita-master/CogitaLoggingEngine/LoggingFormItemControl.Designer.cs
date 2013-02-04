namespace CogitaLoggingEngine
{
    partial class LoggingFormItemControl
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
            this.MessageContainerGroupBox = new System.Windows.Forms.GroupBox();
            this.LoggingMessageText = new System.Windows.Forms.Label();
            this.MessageContainerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageContainerGroupBox
            // 
            this.MessageContainerGroupBox.AutoSize = true;
            this.MessageContainerGroupBox.Controls.Add(this.LoggingMessageText);
            this.MessageContainerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageContainerGroupBox.Location = new System.Drawing.Point(0, 0);
            this.MessageContainerGroupBox.Name = "MessageContainerGroupBox";
            this.MessageContainerGroupBox.Size = new System.Drawing.Size(400, 50);
            this.MessageContainerGroupBox.TabIndex = 0;
            this.MessageContainerGroupBox.TabStop = false;
            // 
            // LoggingMessageText
            // 
            this.LoggingMessageText.AutoSize = true;
            this.LoggingMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoggingMessageText.Location = new System.Drawing.Point(3, 16);
            this.LoggingMessageText.Name = "LoggingMessageText";
            this.LoggingMessageText.Size = new System.Drawing.Size(35, 13);
            this.LoggingMessageText.TabIndex = 0;
            this.LoggingMessageText.Text = "label1";
            // 
            // LoggingFormItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MessageContainerGroupBox);
            this.MinimumSize = new System.Drawing.Size(400, 50);
            this.Name = "LoggingFormItemControl";
            this.Size = new System.Drawing.Size(400, 50);
            this.MessageContainerGroupBox.ResumeLayout(false);
            this.MessageContainerGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox MessageContainerGroupBox;
        private System.Windows.Forms.Label LoggingMessageText;
    }
}
