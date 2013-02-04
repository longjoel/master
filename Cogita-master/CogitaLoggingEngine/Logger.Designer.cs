namespace CogitaLoggingEngine
{
    partial class Logger
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
            this.LogItemsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LogReaderTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // LogItemsFlowLayoutPanel
            // 
            this.LogItemsFlowLayoutPanel.AutoScroll = true;
            this.LogItemsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogItemsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LogItemsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LogItemsFlowLayoutPanel.Name = "LogItemsFlowLayoutPanel";
            this.LogItemsFlowLayoutPanel.Size = new System.Drawing.Size(451, 366);
            this.LogItemsFlowLayoutPanel.TabIndex = 0;
            this.LogItemsFlowLayoutPanel.WrapContents = false;
            this.LogItemsFlowLayoutPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.LogItemsFlowLayoutPanel_ControlSetChanged);
            // 
            // LogReaderTimer
            // 
            this.LogReaderTimer.Tick += new System.EventHandler(this.LogReaderTimer_Tick);
            // 
            // Logger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 366);
            this.ControlBox = false;
            this.Controls.Add(this.LogItemsFlowLayoutPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Logger";
            this.ShowIcon = false;
            this.Text = "Cogita Logging";
            this.Load += new System.EventHandler(this.LoggingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel LogItemsFlowLayoutPanel;
        private System.Windows.Forms.Timer LogReaderTimer;
    }
}