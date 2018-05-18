namespace AMLogicFlashTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FlashTab = new System.Windows.Forms.TabPage();
            this.FlashPanel = new System.Windows.Forms.Panel();
            this.FlashOutput = new System.Windows.Forms.RichTextBox();
            this.FlashOptions = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Flash = new System.Windows.Forms.Button();
            this.Browse = new System.Windows.Forms.Button();
            this.ImgPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FlashRecovery = new System.Windows.Forms.RadioButton();
            this.FlashBoot = new System.Windows.Forms.RadioButton();
            this.RebootOptions = new System.Windows.Forms.GroupBox();
            this.RebootRecovery = new System.Windows.Forms.Button();
            this.RebootSystem = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PortProgress = new System.Windows.Forms.Label();
            this.FlashAfterPort = new System.Windows.Forms.CheckBox();
            this.ExtractFromPackage = new System.Windows.Forms.RadioButton();
            this.ExtractPackage = new System.Windows.Forms.GroupBox();
            this.SelectRecoveryUpgrade = new System.Windows.Forms.Button();
            this.UpgradePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortProgressBar = new System.Windows.Forms.ProgressBar();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortRecovery = new System.Windows.Forms.Button();
            this.LocalRecovery = new System.Windows.Forms.GroupBox();
            this.SelectRecoveryLocal = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.RecoveryPath = new System.Windows.Forms.TextBox();
            this.PullFromLocal = new System.Windows.Forms.RadioButton();
            this.PullFromDevice = new System.Windows.Forms.RadioButton();
            this.PullRecovery = new System.Windows.Forms.GroupBox();
            this.AllowChanging = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BlockPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.BackupPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.BackupKernel = new System.Windows.Forms.CheckBox();
            this.BackupRecovery = new System.Windows.Forms.CheckBox();
            this.BackupZip = new System.Windows.Forms.Button();
            this.BackupFolder = new System.Windows.Forms.Button();
            this.BackupLabel = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ConnectedLabel = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.ShowConnect = new System.Windows.Forms.Button();
            this.FlashBlock = new System.Windows.Forms.TextBox();
            this.FlashCustom = new System.Windows.Forms.RadioButton();
            this.AllowCustomBlockFlashing = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.FlashTab.SuspendLayout();
            this.FlashPanel.SuspendLayout();
            this.FlashOptions.SuspendLayout();
            this.RebootOptions.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.ExtractPackage.SuspendLayout();
            this.LocalRecovery.SuspendLayout();
            this.PullRecovery.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.BackupPanel.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FlashTab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 425);
            this.tabControl1.TabIndex = 0;
            // 
            // FlashTab
            // 
            this.FlashTab.BackColor = System.Drawing.SystemColors.Control;
            this.FlashTab.Controls.Add(this.FlashPanel);
            this.FlashTab.Location = new System.Drawing.Point(4, 22);
            this.FlashTab.Name = "FlashTab";
            this.FlashTab.Padding = new System.Windows.Forms.Padding(3);
            this.FlashTab.Size = new System.Drawing.Size(792, 399);
            this.FlashTab.TabIndex = 0;
            this.FlashTab.Text = "Kernel/Recovery";
            // 
            // FlashPanel
            // 
            this.FlashPanel.Controls.Add(this.FlashOutput);
            this.FlashPanel.Controls.Add(this.FlashOptions);
            this.FlashPanel.Controls.Add(this.RebootOptions);
            this.FlashPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlashPanel.Location = new System.Drawing.Point(3, 3);
            this.FlashPanel.Name = "FlashPanel";
            this.FlashPanel.Size = new System.Drawing.Size(786, 393);
            this.FlashPanel.TabIndex = 21;
            // 
            // FlashOutput
            // 
            this.FlashOutput.BackColor = System.Drawing.Color.White;
            this.FlashOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FlashOutput.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FlashOutput.Dock = System.Windows.Forms.DockStyle.Right;
            this.FlashOutput.ForeColor = System.Drawing.Color.Black;
            this.FlashOutput.Location = new System.Drawing.Point(532, 0);
            this.FlashOutput.Name = "FlashOutput";
            this.FlashOutput.ReadOnly = true;
            this.FlashOutput.Size = new System.Drawing.Size(254, 393);
            this.FlashOutput.TabIndex = 20;
            this.FlashOutput.Text = "Output";
            // 
            // FlashOptions
            // 
            this.FlashOptions.Controls.Add(this.FlashCustom);
            this.FlashOptions.Controls.Add(this.FlashBlock);
            this.FlashOptions.Controls.Add(this.label6);
            this.FlashOptions.Controls.Add(this.Flash);
            this.FlashOptions.Controls.Add(this.Browse);
            this.FlashOptions.Controls.Add(this.ImgPath);
            this.FlashOptions.Controls.Add(this.label5);
            this.FlashOptions.Controls.Add(this.label4);
            this.FlashOptions.Controls.Add(this.label2);
            this.FlashOptions.Controls.Add(this.FlashRecovery);
            this.FlashOptions.Controls.Add(this.FlashBoot);
            this.FlashOptions.ForeColor = System.Drawing.Color.Black;
            this.FlashOptions.Location = new System.Drawing.Point(5, 3);
            this.FlashOptions.Name = "FlashOptions";
            this.FlashOptions.Size = new System.Drawing.Size(521, 304);
            this.FlashOptions.TabIndex = 19;
            this.FlashOptions.TabStop = false;
            this.FlashOptions.Text = "Flash options";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(369, 65);
            this.label6.TabIndex = 24;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // Flash
            // 
            this.Flash.ForeColor = System.Drawing.Color.Black;
            this.Flash.Location = new System.Drawing.Point(5, 278);
            this.Flash.Name = "Flash";
            this.Flash.Size = new System.Drawing.Size(75, 23);
            this.Flash.TabIndex = 23;
            this.Flash.Text = "Flash";
            this.Flash.UseVisualStyleBackColor = true;
            this.Flash.Click += new System.EventHandler(this.Flash_Click);
            // 
            // Browse
            // 
            this.Browse.ForeColor = System.Drawing.Color.Black;
            this.Browse.Location = new System.Drawing.Point(4, 155);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 23);
            this.Browse.TabIndex = 22;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // ImgPath
            // 
            this.ImgPath.BackColor = System.Drawing.Color.White;
            this.ImgPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImgPath.Enabled = false;
            this.ImgPath.ForeColor = System.Drawing.Color.Black;
            this.ImgPath.Location = new System.Drawing.Point(86, 157);
            this.ImgPath.Name = "ImgPath";
            this.ImgPath.Size = new System.Drawing.Size(334, 20);
            this.ImgPath.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(4, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "And the location of the .img file is -";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "I am flashing -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(418, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "WARNING: Flashing wrong files that are not for your device could damage or destro" +
    "y it.";
            // 
            // FlashRecovery
            // 
            this.FlashRecovery.AutoSize = true;
            this.FlashRecovery.ForeColor = System.Drawing.Color.Black;
            this.FlashRecovery.Location = new System.Drawing.Point(6, 82);
            this.FlashRecovery.Name = "FlashRecovery";
            this.FlashRecovery.Size = new System.Drawing.Size(71, 17);
            this.FlashRecovery.TabIndex = 17;
            this.FlashRecovery.TabStop = true;
            this.FlashRecovery.Text = "Recovery";
            this.FlashRecovery.UseVisualStyleBackColor = true;
            // 
            // FlashBoot
            // 
            this.FlashBoot.AutoSize = true;
            this.FlashBoot.ForeColor = System.Drawing.Color.Black;
            this.FlashBoot.Location = new System.Drawing.Point(6, 59);
            this.FlashBoot.Name = "FlashBoot";
            this.FlashBoot.Size = new System.Drawing.Size(47, 17);
            this.FlashBoot.TabIndex = 16;
            this.FlashBoot.TabStop = true;
            this.FlashBoot.Text = "Boot";
            this.FlashBoot.UseVisualStyleBackColor = true;
            // 
            // RebootOptions
            // 
            this.RebootOptions.Controls.Add(this.RebootRecovery);
            this.RebootOptions.Controls.Add(this.RebootSystem);
            this.RebootOptions.ForeColor = System.Drawing.Color.Black;
            this.RebootOptions.Location = new System.Drawing.Point(5, 313);
            this.RebootOptions.Name = "RebootOptions";
            this.RebootOptions.Size = new System.Drawing.Size(521, 60);
            this.RebootOptions.TabIndex = 18;
            this.RebootOptions.TabStop = false;
            this.RebootOptions.Text = "Reboot options";
            // 
            // RebootRecovery
            // 
            this.RebootRecovery.ForeColor = System.Drawing.Color.Black;
            this.RebootRecovery.Location = new System.Drawing.Point(149, 19);
            this.RebootRecovery.Name = "RebootRecovery";
            this.RebootRecovery.Size = new System.Drawing.Size(135, 23);
            this.RebootRecovery.TabIndex = 14;
            this.RebootRecovery.Text = "Reboot recovery";
            this.RebootRecovery.UseVisualStyleBackColor = true;
            this.RebootRecovery.Click += new System.EventHandler(this.RebootRecovery_Click);
            // 
            // RebootSystem
            // 
            this.RebootSystem.ForeColor = System.Drawing.Color.Black;
            this.RebootSystem.Location = new System.Drawing.Point(7, 19);
            this.RebootSystem.Name = "RebootSystem";
            this.RebootSystem.Size = new System.Drawing.Size(136, 23);
            this.RebootSystem.TabIndex = 13;
            this.RebootSystem.Text = "Reboot";
            this.RebootSystem.UseVisualStyleBackColor = true;
            this.RebootSystem.Click += new System.EventHandler(this.RebootSystem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.PortProgress);
            this.tabPage2.Controls.Add(this.FlashAfterPort);
            this.tabPage2.Controls.Add(this.ExtractFromPackage);
            this.tabPage2.Controls.Add(this.ExtractPackage);
            this.tabPage2.Controls.Add(this.PortProgressBar);
            this.tabPage2.Controls.Add(this.PortLabel);
            this.tabPage2.Controls.Add(this.PortRecovery);
            this.tabPage2.Controls.Add(this.LocalRecovery);
            this.tabPage2.Controls.Add(this.PullFromLocal);
            this.tabPage2.Controls.Add(this.PullFromDevice);
            this.tabPage2.Controls.Add(this.PullRecovery);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Porting TWRP";
            // 
            // PortProgress
            // 
            this.PortProgress.AutoSize = true;
            this.PortProgress.Location = new System.Drawing.Point(517, 395);
            this.PortProgress.Name = "PortProgress";
            this.PortProgress.Size = new System.Drawing.Size(0, 13);
            this.PortProgress.TabIndex = 22;
            // 
            // FlashAfterPort
            // 
            this.FlashAfterPort.AutoSize = true;
            this.FlashAfterPort.Location = new System.Drawing.Point(8, 362);
            this.FlashAfterPort.Name = "FlashAfterPort";
            this.FlashAfterPort.Size = new System.Drawing.Size(110, 17);
            this.FlashAfterPort.TabIndex = 21;
            this.FlashAfterPort.Text = "Flash after porting";
            this.FlashAfterPort.UseVisualStyleBackColor = true;
            // 
            // ExtractFromPackage
            // 
            this.ExtractFromPackage.AutoSize = true;
            this.ExtractFromPackage.Location = new System.Drawing.Point(11, 273);
            this.ExtractFromPackage.Name = "ExtractFromPackage";
            this.ExtractFromPackage.Size = new System.Drawing.Size(14, 13);
            this.ExtractFromPackage.TabIndex = 20;
            this.ExtractFromPackage.UseVisualStyleBackColor = true;
            this.ExtractFromPackage.CheckedChanged += new System.EventHandler(this.ExtractFromPackage_CheckedChanged);
            // 
            // ExtractPackage
            // 
            this.ExtractPackage.Controls.Add(this.SelectRecoveryUpgrade);
            this.ExtractPackage.Controls.Add(this.UpgradePath);
            this.ExtractPackage.Controls.Add(this.label1);
            this.ExtractPackage.Enabled = false;
            this.ExtractPackage.Location = new System.Drawing.Point(31, 255);
            this.ExtractPackage.Name = "ExtractPackage";
            this.ExtractPackage.Size = new System.Drawing.Size(360, 53);
            this.ExtractPackage.TabIndex = 19;
            this.ExtractPackage.TabStop = false;
            this.ExtractPackage.Text = "Extract from upgrade package";
            // 
            // SelectRecoveryUpgrade
            // 
            this.SelectRecoveryUpgrade.Location = new System.Drawing.Point(261, 25);
            this.SelectRecoveryUpgrade.Name = "SelectRecoveryUpgrade";
            this.SelectRecoveryUpgrade.Size = new System.Drawing.Size(75, 23);
            this.SelectRecoveryUpgrade.TabIndex = 6;
            this.SelectRecoveryUpgrade.Text = "Select";
            this.SelectRecoveryUpgrade.UseVisualStyleBackColor = true;
            this.SelectRecoveryUpgrade.Click += new System.EventHandler(this.SelectRecoveryUpgrade_Click);
            // 
            // UpgradePath
            // 
            this.UpgradePath.Enabled = false;
            this.UpgradePath.Location = new System.Drawing.Point(35, 27);
            this.UpgradePath.Name = "UpgradePath";
            this.UpgradePath.ReadOnly = true;
            this.UpgradePath.Size = new System.Drawing.Size(220, 20);
            this.UpgradePath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "File";
            // 
            // PortProgressBar
            // 
            this.PortProgressBar.Location = new System.Drawing.Point(149, 390);
            this.PortProgressBar.Name = "PortProgressBar";
            this.PortProgressBar.Size = new System.Drawing.Size(362, 23);
            this.PortProgressBar.TabIndex = 18;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(149, 366);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(0, 13);
            this.PortLabel.TabIndex = 17;
            // 
            // PortRecovery
            // 
            this.PortRecovery.Location = new System.Drawing.Point(6, 390);
            this.PortRecovery.Name = "PortRecovery";
            this.PortRecovery.Size = new System.Drawing.Size(132, 23);
            this.PortRecovery.TabIndex = 15;
            this.PortRecovery.Text = "Create TWRP";
            this.PortRecovery.UseVisualStyleBackColor = true;
            this.PortRecovery.Click += new System.EventHandler(this.PortRecovery_Click);
            // 
            // LocalRecovery
            // 
            this.LocalRecovery.Controls.Add(this.SelectRecoveryLocal);
            this.LocalRecovery.Controls.Add(this.label9);
            this.LocalRecovery.Controls.Add(this.RecoveryPath);
            this.LocalRecovery.Location = new System.Drawing.Point(31, 113);
            this.LocalRecovery.Name = "LocalRecovery";
            this.LocalRecovery.Size = new System.Drawing.Size(360, 53);
            this.LocalRecovery.TabIndex = 12;
            this.LocalRecovery.TabStop = false;
            this.LocalRecovery.Text = "Select local recovery";
            // 
            // SelectRecoveryLocal
            // 
            this.SelectRecoveryLocal.Location = new System.Drawing.Point(261, 22);
            this.SelectRecoveryLocal.Name = "SelectRecoveryLocal";
            this.SelectRecoveryLocal.Size = new System.Drawing.Size(75, 23);
            this.SelectRecoveryLocal.TabIndex = 3;
            this.SelectRecoveryLocal.Text = "Select";
            this.SelectRecoveryLocal.UseVisualStyleBackColor = true;
            this.SelectRecoveryLocal.Click += new System.EventHandler(this.SelectRecoveryLocal_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "File";
            // 
            // RecoveryPath
            // 
            this.RecoveryPath.Enabled = false;
            this.RecoveryPath.Location = new System.Drawing.Point(35, 24);
            this.RecoveryPath.Name = "RecoveryPath";
            this.RecoveryPath.ReadOnly = true;
            this.RecoveryPath.Size = new System.Drawing.Size(220, 20);
            this.RecoveryPath.TabIndex = 1;
            // 
            // PullFromLocal
            // 
            this.PullFromLocal.AutoSize = true;
            this.PullFromLocal.Checked = true;
            this.PullFromLocal.Location = new System.Drawing.Point(11, 131);
            this.PullFromLocal.Name = "PullFromLocal";
            this.PullFromLocal.Size = new System.Drawing.Size(14, 13);
            this.PullFromLocal.TabIndex = 14;
            this.PullFromLocal.TabStop = true;
            this.PullFromLocal.UseVisualStyleBackColor = true;
            this.PullFromLocal.CheckedChanged += new System.EventHandler(this.PullFromLocal_CheckedChanged);
            // 
            // PullFromDevice
            // 
            this.PullFromDevice.AutoSize = true;
            this.PullFromDevice.Location = new System.Drawing.Point(11, 202);
            this.PullFromDevice.Name = "PullFromDevice";
            this.PullFromDevice.Size = new System.Drawing.Size(14, 13);
            this.PullFromDevice.TabIndex = 13;
            this.PullFromDevice.UseVisualStyleBackColor = true;
            this.PullFromDevice.CheckedChanged += new System.EventHandler(this.PullFromDevice_CheckedChanged);
            // 
            // PullRecovery
            // 
            this.PullRecovery.Controls.Add(this.AllowChanging);
            this.PullRecovery.Controls.Add(this.label8);
            this.PullRecovery.Controls.Add(this.BlockPath);
            this.PullRecovery.Enabled = false;
            this.PullRecovery.Location = new System.Drawing.Point(31, 184);
            this.PullRecovery.Name = "PullRecovery";
            this.PullRecovery.Size = new System.Drawing.Size(360, 53);
            this.PullRecovery.TabIndex = 11;
            this.PullRecovery.TabStop = false;
            this.PullRecovery.Text = "Pull recovery from device";
            // 
            // AllowChanging
            // 
            this.AllowChanging.AutoSize = true;
            this.AllowChanging.Location = new System.Drawing.Point(238, 26);
            this.AllowChanging.Name = "AllowChanging";
            this.AllowChanging.Size = new System.Drawing.Size(116, 17);
            this.AllowChanging.TabIndex = 3;
            this.AllowChanging.Text = "Change block path";
            this.AllowChanging.UseVisualStyleBackColor = true;
            this.AllowChanging.CheckedChanged += new System.EventHandler(this.AllowChanging_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Recovery block path";
            // 
            // BlockPath
            // 
            this.BlockPath.Enabled = false;
            this.BlockPath.Location = new System.Drawing.Point(118, 24);
            this.BlockPath.Name = "BlockPath";
            this.BlockPath.Size = new System.Drawing.Size(113, 20);
            this.BlockPath.TabIndex = 1;
            this.BlockPath.Text = "/dev/block/recovery";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(503, 78);
            this.label7.TabIndex = 10;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Flash zip(Experimental)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(407, 78);
            this.label10.TabIndex = 12;
            this.label10.Text = resources.GetString("label10.Text");
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.BackupPanel);
            this.tabPage4.Controls.Add(this.BackupLabel);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(792, 399);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Backup";
            // 
            // BackupPanel
            // 
            this.BackupPanel.Controls.Add(this.label3);
            this.BackupPanel.Controls.Add(this.BackupKernel);
            this.BackupPanel.Controls.Add(this.BackupRecovery);
            this.BackupPanel.Controls.Add(this.BackupZip);
            this.BackupPanel.Controls.Add(this.BackupFolder);
            this.BackupPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackupPanel.Location = new System.Drawing.Point(3, 3);
            this.BackupPanel.Name = "BackupPanel";
            this.BackupPanel.Size = new System.Drawing.Size(786, 393);
            this.BackupPanel.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(347, 39);
            this.label3.TabIndex = 11;
            this.label3.Text = "This option allows you to backup your kernel/recovery from your device.\r\n\r\nSelect" +
    " what to backup and how to backup.";
            // 
            // BackupKernel
            // 
            this.BackupKernel.AutoSize = true;
            this.BackupKernel.Location = new System.Drawing.Point(14, 59);
            this.BackupKernel.Name = "BackupKernel";
            this.BackupKernel.Size = new System.Drawing.Size(95, 17);
            this.BackupKernel.TabIndex = 0;
            this.BackupKernel.Text = "Backup kernel";
            this.BackupKernel.UseVisualStyleBackColor = true;
            // 
            // BackupRecovery
            // 
            this.BackupRecovery.AutoSize = true;
            this.BackupRecovery.Location = new System.Drawing.Point(14, 82);
            this.BackupRecovery.Name = "BackupRecovery";
            this.BackupRecovery.Size = new System.Drawing.Size(107, 17);
            this.BackupRecovery.TabIndex = 1;
            this.BackupRecovery.Text = "Backup recovery";
            this.BackupRecovery.UseVisualStyleBackColor = true;
            // 
            // BackupZip
            // 
            this.BackupZip.Location = new System.Drawing.Point(14, 138);
            this.BackupZip.Name = "BackupZip";
            this.BackupZip.Size = new System.Drawing.Size(133, 23);
            this.BackupZip.TabIndex = 3;
            this.BackupZip.Text = "Backup to flashable ZIP";
            this.BackupZip.UseVisualStyleBackColor = true;
            this.BackupZip.Click += new System.EventHandler(this.BackupZip_Click);
            // 
            // BackupFolder
            // 
            this.BackupFolder.Location = new System.Drawing.Point(14, 109);
            this.BackupFolder.Name = "BackupFolder";
            this.BackupFolder.Size = new System.Drawing.Size(133, 23);
            this.BackupFolder.TabIndex = 2;
            this.BackupFolder.Text = "Backup to folder";
            this.BackupFolder.UseVisualStyleBackColor = true;
            this.BackupFolder.Click += new System.EventHandler(this.BackupFolder_Click);
            // 
            // BackupLabel
            // 
            this.BackupLabel.AutoSize = true;
            this.BackupLabel.Location = new System.Drawing.Point(11, 170);
            this.BackupLabel.Name = "BackupLabel";
            this.BackupLabel.Size = new System.Drawing.Size(0, 13);
            this.BackupLabel.TabIndex = 12;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.AllowCustomBlockFlashing);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(792, 399);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Advanced";
            // 
            // ConnectedLabel
            // 
            this.ConnectedLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ConnectedLabel.ForeColor = System.Drawing.Color.Red;
            this.ConnectedLabel.Location = new System.Drawing.Point(722, 0);
            this.ConnectedLabel.Name = "ConnectedLabel";
            this.ConnectedLabel.Size = new System.Drawing.Size(78, 25);
            this.ConnectedLabel.TabIndex = 1;
            this.ConnectedLabel.Text = "Not connected";
            this.ConnectedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.ShowConnect);
            this.StatusPanel.Controls.Add(this.ConnectedLabel);
            this.StatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusPanel.Location = new System.Drawing.Point(0, 425);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(800, 25);
            this.StatusPanel.TabIndex = 2;
            // 
            // ShowConnect
            // 
            this.ShowConnect.Dock = System.Windows.Forms.DockStyle.Left;
            this.ShowConnect.Location = new System.Drawing.Point(0, 0);
            this.ShowConnect.Name = "ShowConnect";
            this.ShowConnect.Size = new System.Drawing.Size(75, 25);
            this.ShowConnect.TabIndex = 2;
            this.ShowConnect.Text = "Connect";
            this.ShowConnect.UseVisualStyleBackColor = true;
            this.ShowConnect.Click += new System.EventHandler(this.ShowConnect_Click);
            // 
            // FlashBlock
            // 
            this.FlashBlock.Enabled = false;
            this.FlashBlock.Location = new System.Drawing.Point(110, 107);
            this.FlashBlock.Name = "FlashBlock";
            this.FlashBlock.Size = new System.Drawing.Size(160, 20);
            this.FlashBlock.TabIndex = 25;
            this.FlashBlock.Text = "/dev/block/";
            // 
            // FlashCustom
            // 
            this.FlashCustom.AutoSize = true;
            this.FlashCustom.Enabled = false;
            this.FlashCustom.ForeColor = System.Drawing.Color.Black;
            this.FlashCustom.Location = new System.Drawing.Point(6, 108);
            this.FlashCustom.Name = "FlashCustom";
            this.FlashCustom.Size = new System.Drawing.Size(98, 17);
            this.FlashCustom.TabIndex = 26;
            this.FlashCustom.TabStop = true;
            this.FlashCustom.Text = "A custom block";
            this.FlashCustom.UseVisualStyleBackColor = true;
            // 
            // AllowCustomBlockFlashing
            // 
            this.AllowCustomBlockFlashing.AutoSize = true;
            this.AllowCustomBlockFlashing.Location = new System.Drawing.Point(9, 7);
            this.AllowCustomBlockFlashing.Name = "AllowCustomBlockFlashing";
            this.AllowCustomBlockFlashing.Size = new System.Drawing.Size(156, 17);
            this.AllowCustomBlockFlashing.TabIndex = 0;
            this.AllowCustomBlockFlashing.Text = "Allow custom block flashing";
            this.AllowCustomBlockFlashing.UseVisualStyleBackColor = true;
            this.AllowCustomBlockFlashing.CheckedChanged += new System.EventHandler(this.AllowCustomBlockFlashing_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Amlogic Flash Tool";
            this.tabControl1.ResumeLayout(false);
            this.FlashTab.ResumeLayout(false);
            this.FlashPanel.ResumeLayout(false);
            this.FlashOptions.ResumeLayout(false);
            this.FlashOptions.PerformLayout();
            this.RebootOptions.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ExtractPackage.ResumeLayout(false);
            this.ExtractPackage.PerformLayout();
            this.LocalRecovery.ResumeLayout(false);
            this.LocalRecovery.PerformLayout();
            this.PullRecovery.ResumeLayout(false);
            this.PullRecovery.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.BackupPanel.ResumeLayout(false);
            this.BackupPanel.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage FlashTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox FlashOptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Flash;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox ImgPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton FlashRecovery;
        private System.Windows.Forms.RadioButton FlashBoot;
        private System.Windows.Forms.GroupBox RebootOptions;
        private System.Windows.Forms.Button RebootRecovery;
        private System.Windows.Forms.Button RebootSystem;
        private System.Windows.Forms.RadioButton ExtractFromPackage;
        private System.Windows.Forms.GroupBox ExtractPackage;
        private System.Windows.Forms.ProgressBar PortProgressBar;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button PortRecovery;
        private System.Windows.Forms.GroupBox LocalRecovery;
        private System.Windows.Forms.Button SelectRecoveryLocal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox RecoveryPath;
        private System.Windows.Forms.RadioButton PullFromLocal;
        private System.Windows.Forms.RadioButton PullFromDevice;
        private System.Windows.Forms.GroupBox PullRecovery;
        private System.Windows.Forms.CheckBox AllowChanging;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox BlockPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox FlashOutput;
        private System.Windows.Forms.Button SelectRecoveryUpgrade;
        private System.Windows.Forms.TextBox UpgradePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox FlashAfterPort;
        private System.Windows.Forms.Label PortProgress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BackupZip;
        private System.Windows.Forms.Button BackupFolder;
        private System.Windows.Forms.CheckBox BackupRecovery;
        private System.Windows.Forms.CheckBox BackupKernel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label BackupLabel;
        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.Panel FlashPanel;
        private System.Windows.Forms.Panel BackupPanel;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Button ShowConnect;
        private System.Windows.Forms.RadioButton FlashCustom;
        private System.Windows.Forms.TextBox FlashBlock;
        private System.Windows.Forms.CheckBox AllowCustomBlockFlashing;
    }
}

