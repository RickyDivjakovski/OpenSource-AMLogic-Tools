namespace BurnCardMaker
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
            this.label1 = new System.Windows.Forms.Label();
            this.StorageDevice = new System.Windows.Forms.ComboBox();
            this.FormatPartition = new System.Windows.Forms.CheckBox();
            this.EraseBootloader = new System.Windows.Forms.CheckBox();
            this.EraseFlash = new System.Windows.Forms.CheckBox();
            this.Reboot = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectedFile = new System.Windows.Forms.TextBox();
            this.SelectFile = new System.Windows.Forms.Button();
            this.CreateBurnCard = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.EraseMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TaskProgress = new System.Windows.Forms.ProgressBar();
            this.CurrentTask = new System.Windows.Forms.Label();
            this.FSType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Disk: ";
            // 
            // StorageDevice
            // 
            this.StorageDevice.FormattingEnabled = true;
            this.StorageDevice.Location = new System.Drawing.Point(53, 10);
            this.StorageDevice.Name = "StorageDevice";
            this.StorageDevice.Size = new System.Drawing.Size(121, 21);
            this.StorageDevice.TabIndex = 1;
            this.StorageDevice.Text = "Select SDcard";
            // 
            // FormatPartition
            // 
            this.FormatPartition.AutoSize = true;
            this.FormatPartition.Checked = true;
            this.FormatPartition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FormatPartition.Location = new System.Drawing.Point(16, 63);
            this.FormatPartition.Name = "FormatPartition";
            this.FormatPartition.Size = new System.Drawing.Size(58, 17);
            this.FormatPartition.TabIndex = 2;
            this.FormatPartition.Text = "Format";
            this.FormatPartition.UseVisualStyleBackColor = true;
            this.FormatPartition.CheckedChanged += new System.EventHandler(this.FormatPartition_CheckedChanged);
            // 
            // EraseBootloader
            // 
            this.EraseBootloader.AutoSize = true;
            this.EraseBootloader.Checked = true;
            this.EraseBootloader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EraseBootloader.Location = new System.Drawing.Point(16, 117);
            this.EraseBootloader.Name = "EraseBootloader";
            this.EraseBootloader.Size = new System.Drawing.Size(106, 17);
            this.EraseBootloader.TabIndex = 3;
            this.EraseBootloader.Text = "Erase bootloader";
            this.EraseBootloader.UseVisualStyleBackColor = true;
            // 
            // EraseFlash
            // 
            this.EraseFlash.AutoSize = true;
            this.EraseFlash.Checked = true;
            this.EraseFlash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EraseFlash.Location = new System.Drawing.Point(16, 140);
            this.EraseFlash.Name = "EraseFlash";
            this.EraseFlash.Size = new System.Drawing.Size(78, 17);
            this.EraseFlash.TabIndex = 4;
            this.EraseFlash.Text = "Erase flash";
            this.EraseFlash.UseVisualStyleBackColor = true;
            this.EraseFlash.CheckedChanged += new System.EventHandler(this.EraseFlash_CheckedChanged);
            // 
            // Reboot
            // 
            this.Reboot.AutoSize = true;
            this.Reboot.Checked = true;
            this.Reboot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Reboot.Location = new System.Drawing.Point(16, 196);
            this.Reboot.Name = "Reboot";
            this.Reboot.Size = new System.Drawing.Size(122, 17);
            this.Reboot.TabIndex = 5;
            this.Reboot.Text = "Reboot on complete";
            this.Reboot.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "SD options:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Erase options:";
            // 
            // SelectedFile
            // 
            this.SelectedFile.Enabled = false;
            this.SelectedFile.Location = new System.Drawing.Point(93, 251);
            this.SelectedFile.Name = "SelectedFile";
            this.SelectedFile.Size = new System.Drawing.Size(306, 20);
            this.SelectedFile.TabIndex = 8;
            this.SelectedFile.Text = "Select image..";
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(12, 250);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(75, 23);
            this.SelectFile.TabIndex = 9;
            this.SelectFile.Text = "Select";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // CreateBurnCard
            // 
            this.CreateBurnCard.Location = new System.Drawing.Point(12, 292);
            this.CreateBurnCard.Name = "CreateBurnCard";
            this.CreateBurnCard.Size = new System.Drawing.Size(75, 23);
            this.CreateBurnCard.TabIndex = 10;
            this.CreateBurnCard.Text = "Create";
            this.CreateBurnCard.UseVisualStyleBackColor = true;
            this.CreateBurnCard.Click += new System.EventHandler(this.CreateBurnCard_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "File options:";
            // 
            // EraseMethod
            // 
            this.EraseMethod.FormattingEnabled = true;
            this.EraseMethod.Items.AddRange(new object[] {
            "Normal",
            "Force",
            "All",
            "Force all"});
            this.EraseMethod.Location = new System.Drawing.Point(93, 138);
            this.EraseMethod.Name = "EraseMethod";
            this.EraseMethod.Size = new System.Drawing.Size(121, 21);
            this.EraseMethod.TabIndex = 12;
            this.EraseMethod.Text = "Select erase method";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Reboot options:";
            // 
            // TaskProgress
            // 
            this.TaskProgress.Location = new System.Drawing.Point(12, 321);
            this.TaskProgress.Name = "TaskProgress";
            this.TaskProgress.Size = new System.Drawing.Size(394, 23);
            this.TaskProgress.TabIndex = 14;
            // 
            // CurrentTask
            // 
            this.CurrentTask.Location = new System.Drawing.Point(93, 292);
            this.CurrentTask.Name = "CurrentTask";
            this.CurrentTask.Size = new System.Drawing.Size(306, 23);
            this.CurrentTask.TabIndex = 15;
            this.CurrentTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FSType
            // 
            this.FSType.FormattingEnabled = true;
            this.FSType.Items.AddRange(new object[] {
            "FAT",
            "FAT32",
            "NTFS",
            "exFAT"});
            this.FSType.Location = new System.Drawing.Point(80, 61);
            this.FSType.Name = "FSType";
            this.FSType.Size = new System.Drawing.Size(121, 21);
            this.FSType.TabIndex = 16;
            this.FSType.Text = "Select FS type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 353);
            this.Controls.Add(this.FSType);
            this.Controls.Add(this.CurrentTask);
            this.Controls.Add(this.TaskProgress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.EraseMethod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CreateBurnCard);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.SelectedFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Reboot);
            this.Controls.Add(this.EraseFlash);
            this.Controls.Add(this.EraseBootloader);
            this.Controls.Add(this.FormatPartition);
            this.Controls.Add(this.StorageDevice);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SD Burn Card Maker V3.0.2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox StorageDevice;
        private System.Windows.Forms.CheckBox FormatPartition;
        private System.Windows.Forms.CheckBox EraseBootloader;
        private System.Windows.Forms.CheckBox EraseFlash;
        private System.Windows.Forms.CheckBox Reboot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SelectedFile;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button CreateBurnCard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox EraseMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar TaskProgress;
        private System.Windows.Forms.Label CurrentTask;
        private System.Windows.Forms.ComboBox FSType;
    }
}

