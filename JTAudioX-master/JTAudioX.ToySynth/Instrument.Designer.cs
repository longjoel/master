namespace JTAudioX.ToySynth
{
    partial class Instrument
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FrequencySlider = new System.Windows.Forms.TrackBar();
            this.VolumeSlider = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SineRadioButton = new System.Windows.Forms.RadioButton();
            this.SquareRadioButton = new System.Windows.Forms.RadioButton();
            this.SawRadioButton = new System.Windows.Forms.RadioButton();
            this.TriangleRadioButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeSlider)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.FrequencySlider, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.VolumeSlider, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.KeyComboBox, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(147, 298);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frequency";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Volume";
            // 
            // FrequencySlider
            // 
            this.FrequencySlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FrequencySlider.Location = new System.Drawing.Point(3, 19);
            this.FrequencySlider.Maximum = 20000;
            this.FrequencySlider.Minimum = 20;
            this.FrequencySlider.Name = "FrequencySlider";
            this.FrequencySlider.Size = new System.Drawing.Size(141, 30);
            this.FrequencySlider.TabIndex = 3;
            this.FrequencySlider.Value = 20;
            // 
            // VolumeSlider
            // 
            this.VolumeSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.VolumeSlider.Location = new System.Drawing.Point(3, 76);
            this.VolumeSlider.Maximum = 100;
            this.VolumeSlider.Name = "VolumeSlider";
            this.VolumeSlider.Size = new System.Drawing.Size(141, 27);
            this.VolumeSlider.SmallChange = 10;
            this.VolumeSlider.TabIndex = 4;
            this.VolumeSlider.Value = 20;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Transform";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.SineRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.SquareRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.SawRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.TriangleRadioButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 124);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(141, 112);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // SineRadioButton
            // 
            this.SineRadioButton.AutoSize = true;
            this.SineRadioButton.Location = new System.Drawing.Point(3, 3);
            this.SineRadioButton.Name = "SineRadioButton";
            this.SineRadioButton.Size = new System.Drawing.Size(46, 17);
            this.SineRadioButton.TabIndex = 0;
            this.SineRadioButton.TabStop = true;
            this.SineRadioButton.Text = "Sine";
            this.SineRadioButton.UseVisualStyleBackColor = true;
            // 
            // SquareRadioButton
            // 
            this.SquareRadioButton.AutoSize = true;
            this.SquareRadioButton.Location = new System.Drawing.Point(55, 3);
            this.SquareRadioButton.Name = "SquareRadioButton";
            this.SquareRadioButton.Size = new System.Drawing.Size(59, 17);
            this.SquareRadioButton.TabIndex = 1;
            this.SquareRadioButton.TabStop = true;
            this.SquareRadioButton.Text = "Square";
            this.SquareRadioButton.UseVisualStyleBackColor = true;
            // 
            // SawRadioButton
            // 
            this.SawRadioButton.AutoSize = true;
            this.SawRadioButton.Location = new System.Drawing.Point(3, 26);
            this.SawRadioButton.Name = "SawRadioButton";
            this.SawRadioButton.Size = new System.Drawing.Size(46, 17);
            this.SawRadioButton.TabIndex = 2;
            this.SawRadioButton.TabStop = true;
            this.SawRadioButton.Text = "Saw";
            this.SawRadioButton.UseVisualStyleBackColor = true;
            // 
            // TriangleRadioButton
            // 
            this.TriangleRadioButton.AutoSize = true;
            this.TriangleRadioButton.Location = new System.Drawing.Point(55, 26);
            this.TriangleRadioButton.Name = "TriangleRadioButton";
            this.TriangleRadioButton.Size = new System.Drawing.Size(63, 17);
            this.TriangleRadioButton.TabIndex = 3;
            this.TriangleRadioButton.TabStop = true;
            this.TriangleRadioButton.Text = "Triangle";
            this.TriangleRadioButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Key";
            // 
            // KeyComboBox
            // 
            this.KeyComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyComboBox.FormattingEnabled = true;
            this.KeyComboBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.KeyComboBox.Location = new System.Drawing.Point(3, 261);
            this.KeyComboBox.Name = "KeyComboBox";
            this.KeyComboBox.Size = new System.Drawing.Size(141, 21);
            this.KeyComboBox.TabIndex = 8;
            // 
            // Instrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Instrument";
            this.Size = new System.Drawing.Size(147, 298);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeSlider)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar FrequencySlider;
        private System.Windows.Forms.TrackBar VolumeSlider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton SineRadioButton;
        private System.Windows.Forms.RadioButton SquareRadioButton;
        private System.Windows.Forms.RadioButton SawRadioButton;
        private System.Windows.Forms.RadioButton TriangleRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox KeyComboBox;
    }
}
