namespace JTAudioX.ToySynth
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ChannelContainerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.channel1 = new JTAudioX.ToySynth.Instrument();
            this.channel2 = new JTAudioX.ToySynth.Instrument();
            this.channel3 = new JTAudioX.ToySynth.Instrument();
            this.channel4 = new JTAudioX.ToySynth.Instrument();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.ChannelContainerPanel.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ChannelContainerPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 351);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ChannelContainerPanel
            // 
            this.ChannelContainerPanel.Controls.Add(this.channel1);
            this.ChannelContainerPanel.Controls.Add(this.channel2);
            this.ChannelContainerPanel.Controls.Add(this.channel3);
            this.ChannelContainerPanel.Controls.Add(this.channel4);
            this.ChannelContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChannelContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.ChannelContainerPanel.Name = "ChannelContainerPanel";
            this.ChannelContainerPanel.Size = new System.Drawing.Size(771, 291);
            this.ChannelContainerPanel.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel2.Controls.Add(this.StartStopButton);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 300);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(771, 48);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // channel1
            // 
            this.channel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.channel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.channel1.Frequency = 20F;
            this.channel1.Location = new System.Drawing.Point(3, 3);
            this.channel1.Name = "channel1";
            this.channel1.Size = new System.Drawing.Size(151, 288);
            this.channel1.TabIndex = 0;
            this.channel1.Volume = 0.2F;
            // 
            // channel2
            // 
            this.channel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.channel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.channel2.Frequency = 20F;
            this.channel2.Location = new System.Drawing.Point(160, 3);
            this.channel2.Name = "channel2";
            this.channel2.Size = new System.Drawing.Size(151, 288);
            this.channel2.TabIndex = 1;
            this.channel2.Volume = 0.2F;
            // 
            // channel3
            // 
            this.channel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.channel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.channel3.Frequency = 20F;
            this.channel3.Location = new System.Drawing.Point(317, 3);
            this.channel3.Name = "channel3";
            this.channel3.Size = new System.Drawing.Size(151, 288);
            this.channel3.TabIndex = 2;
            this.channel3.Volume = 0.2F;
            // 
            // channel4
            // 
            this.channel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.channel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.channel4.Frequency = 20F;
            this.channel4.Location = new System.Drawing.Point(474, 3);
            this.channel4.Name = "channel4";
            this.channel4.Size = new System.Drawing.Size(151, 288);
            this.channel4.TabIndex = 3;
            this.channel4.Volume = 0.2F;
            // 
            // StartStopButton
            // 
            this.StartStopButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StartStopButton.Location = new System.Drawing.Point(3, 3);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(75, 23);
            this.StartStopButton.TabIndex = 0;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 351);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ChannelContainerPanel.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel ChannelContainerPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Instrument channel1;
        private Instrument channel2;
        private Instrument channel3;
        private Instrument channel4;
        private System.Windows.Forms.Button StartStopButton;
    }
}

