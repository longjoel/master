namespace NahrwallEditor
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblxMousePos = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblyMousePos = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glViewMain = new OpenTK.GLControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxWalls = new System.Windows.Forms.CheckBox();
            this.cbxCorridors = new System.Windows.Forms.CheckBox();
            this.cbxPlatforms = new System.Windows.Forms.CheckBox();
            this.cbxEntities = new System.Windows.Forms.CheckBox();
            this.grpAddEntity = new System.Windows.Forms.GroupBox();
            this.flwAddEntity = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddEntity = new System.Windows.Forms.Button();
            this.cmbAddEntity = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grpAddEntity.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtZoom,
            this.toolStripStatusLabel1,
            this.lblxMousePos,
            this.toolStripStatusLabel2,
            this.lblyMousePos});
            this.statusStrip1.Location = new System.Drawing.Point(0, 482);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(747, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtZoom
            // 
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.Size = new System.Drawing.Size(22, 17);
            this.txtZoom.Text = "1.0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel1.Text = "- X:";
            // 
            // lblxMousePos
            // 
            this.lblxMousePos.Name = "lblxMousePos";
            this.lblxMousePos.Size = new System.Drawing.Size(118, 17);
            this.lblxMousePos.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel2.Text = "- Y:";
            // 
            // lblyMousePos
            // 
            this.lblyMousePos.Name = "lblyMousePos";
            this.lblyMousePos.Size = new System.Drawing.Size(118, 17);
            this.lblyMousePos.Text = "toolStripStatusLabel3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton1.Text = "Add Geometry";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(112, 22);
            this.toolStripButton2.Text = "Show/Hide Texture";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(97, 22);
            this.toolStripButton3.Text = "Show/Hide XML";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(155, 22);
            this.toolStripButton4.Text = "Show/Hide Resource Editor";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glViewMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.grpAddEntity);
            this.splitContainer1.Size = new System.Drawing.Size(747, 433);
            this.splitContainer1.SplitterDistance = 545;
            this.splitContainer1.TabIndex = 4;
            // 
            // glViewMain
            // 
            this.glViewMain.BackColor = System.Drawing.Color.Black;
            this.glViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glViewMain.Location = new System.Drawing.Point(0, 0);
            this.glViewMain.Name = "glViewMain";
            this.glViewMain.Size = new System.Drawing.Size(545, 433);
            this.glViewMain.TabIndex = 1;
            this.glViewMain.VSync = false;
            this.glViewMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.glViewMain_KeyPress);
            this.glViewMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glViewMain_MouseClick);
            this.glViewMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glViewMain_MouseDown);
            this.glViewMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glViewMain_MouseMove);
            this.glViewMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glViewMain_MouseUp);
            this.glViewMain.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glViewMain_MouseWheel);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visible Items";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cbxWalls);
            this.flowLayoutPanel1.Controls.Add(this.cbxCorridors);
            this.flowLayoutPanel1.Controls.Add(this.cbxPlatforms);
            this.flowLayoutPanel1.Controls.Add(this.cbxEntities);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(192, 81);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cbxWalls
            // 
            this.cbxWalls.AutoSize = true;
            this.cbxWalls.Location = new System.Drawing.Point(3, 3);
            this.cbxWalls.Name = "cbxWalls";
            this.cbxWalls.Size = new System.Drawing.Size(52, 17);
            this.cbxWalls.TabIndex = 0;
            this.cbxWalls.Text = "Walls";
            this.cbxWalls.UseVisualStyleBackColor = true;
            this.cbxWalls.CheckedChanged += new System.EventHandler(this.cbxWalls_CheckedChanged);
            // 
            // cbxCorridors
            // 
            this.cbxCorridors.AutoSize = true;
            this.cbxCorridors.Location = new System.Drawing.Point(3, 26);
            this.cbxCorridors.Name = "cbxCorridors";
            this.cbxCorridors.Size = new System.Drawing.Size(67, 17);
            this.cbxCorridors.TabIndex = 1;
            this.cbxCorridors.Text = "Corridors";
            this.cbxCorridors.UseVisualStyleBackColor = true;
            this.cbxCorridors.CheckedChanged += new System.EventHandler(this.cbxCorridors_CheckedChanged);
            // 
            // cbxPlatforms
            // 
            this.cbxPlatforms.AutoSize = true;
            this.cbxPlatforms.Location = new System.Drawing.Point(3, 49);
            this.cbxPlatforms.Name = "cbxPlatforms";
            this.cbxPlatforms.Size = new System.Drawing.Size(69, 17);
            this.cbxPlatforms.TabIndex = 2;
            this.cbxPlatforms.Text = "Platforms";
            this.cbxPlatforms.UseVisualStyleBackColor = true;
            this.cbxPlatforms.CheckedChanged += new System.EventHandler(this.cbxPlatforms_CheckedChanged);
            // 
            // cbxEntities
            // 
            this.cbxEntities.AutoSize = true;
            this.cbxEntities.Location = new System.Drawing.Point(78, 3);
            this.cbxEntities.Name = "cbxEntities";
            this.cbxEntities.Size = new System.Drawing.Size(60, 17);
            this.cbxEntities.TabIndex = 3;
            this.cbxEntities.Text = "Entities";
            this.cbxEntities.UseVisualStyleBackColor = true;
            this.cbxEntities.CheckedChanged += new System.EventHandler(this.cbxEntities_CheckedChanged);
            // 
            // grpAddEntity
            // 
            this.grpAddEntity.Controls.Add(this.flwAddEntity);
            this.grpAddEntity.Controls.Add(this.btnAddEntity);
            this.grpAddEntity.Controls.Add(this.cmbAddEntity);
            this.grpAddEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAddEntity.Location = new System.Drawing.Point(0, 0);
            this.grpAddEntity.Name = "grpAddEntity";
            this.grpAddEntity.Size = new System.Drawing.Size(198, 260);
            this.grpAddEntity.TabIndex = 0;
            this.grpAddEntity.TabStop = false;
            this.grpAddEntity.Text = "Add Entity";
            // 
            // flwAddEntity
            // 
            this.flwAddEntity.AutoScroll = true;
            this.flwAddEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwAddEntity.Location = new System.Drawing.Point(3, 37);
            this.flwAddEntity.Name = "flwAddEntity";
            this.flwAddEntity.Size = new System.Drawing.Size(192, 194);
            this.flwAddEntity.TabIndex = 6;
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddEntity.Location = new System.Drawing.Point(3, 231);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(192, 26);
            this.btnAddEntity.TabIndex = 4;
            this.btnAddEntity.Text = "Add Entity";
            this.btnAddEntity.UseVisualStyleBackColor = true;
            this.btnAddEntity.Click += new System.EventHandler(this.btnAddEntity_Click);
            // 
            // cmbAddEntity
            // 
            this.cmbAddEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbAddEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAddEntity.FormattingEnabled = true;
            this.cmbAddEntity.Location = new System.Drawing.Point(3, 16);
            this.cmbAddEntity.Name = "cmbAddEntity";
            this.cmbAddEntity.Size = new System.Drawing.Size(192, 21);
            this.cmbAddEntity.TabIndex = 0;
            this.cmbAddEntity.SelectedIndexChanged += new System.EventHandler(this.cmbAddEntity_SelectedIndexChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 504);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Nahrwall Level Editor";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMain_Paint);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.grpAddEntity.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtZoom;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private OpenTK.GLControl glViewMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblxMousePos;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblyMousePos;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpAddEntity;
        private System.Windows.Forms.ComboBox cmbAddEntity;
        private System.Windows.Forms.FlowLayoutPanel flwAddEntity;
        private System.Windows.Forms.Button btnAddEntity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox cbxWalls;
        private System.Windows.Forms.CheckBox cbxCorridors;
        private System.Windows.Forms.CheckBox cbxPlatforms;
        private System.Windows.Forms.CheckBox cbxEntities;
    }
}