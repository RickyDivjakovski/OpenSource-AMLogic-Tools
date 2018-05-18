namespace AMLUpgradeInfo
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
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.UpgradeFile = new System.Windows.Forms.TextBox();
            this.SelectPackage = new System.Windows.Forms.Button();
            this.FileComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FilesPacked = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FileInfo = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.Label();
            this.FileLocation = new System.Windows.Forms.Label();
            this.FileSize = new System.Windows.Forms.Label();
            this.WrittenSize = new System.Windows.Forms.Label();
            this.NumberFiles = new System.Windows.Forms.Label();
            this.NumberPartitions = new System.Windows.Forms.Label();
            this.InfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoPanel
            // 
            this.InfoPanel.Controls.Add(this.NumberPartitions);
            this.InfoPanel.Controls.Add(this.NumberFiles);
            this.InfoPanel.Controls.Add(this.WrittenSize);
            this.InfoPanel.Controls.Add(this.FileSize);
            this.InfoPanel.Controls.Add(this.FileLocation);
            this.InfoPanel.Controls.Add(this.FileName);
            this.InfoPanel.Controls.Add(this.label7);
            this.InfoPanel.Controls.Add(this.label8);
            this.InfoPanel.Controls.Add(this.FileInfo);
            this.InfoPanel.Controls.Add(this.label6);
            this.InfoPanel.Controls.Add(this.FilesPacked);
            this.InfoPanel.Controls.Add(this.label5);
            this.InfoPanel.Controls.Add(this.label4);
            this.InfoPanel.Controls.Add(this.label3);
            this.InfoPanel.Controls.Add(this.label2);
            this.InfoPanel.Controls.Add(this.label1);
            this.InfoPanel.Controls.Add(this.FileComboBox);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InfoPanel.Enabled = false;
            this.InfoPanel.Location = new System.Drawing.Point(0, 30);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(800, 491);
            this.InfoPanel.TabIndex = 0;
            // 
            // UpgradeFile
            // 
            this.UpgradeFile.Enabled = false;
            this.UpgradeFile.Location = new System.Drawing.Point(86, 4);
            this.UpgradeFile.Name = "UpgradeFile";
            this.UpgradeFile.Size = new System.Drawing.Size(702, 20);
            this.UpgradeFile.TabIndex = 1;
            this.UpgradeFile.Text = "Select AMLogic upgrade package";
            // 
            // SelectPackage
            // 
            this.SelectPackage.Location = new System.Drawing.Point(5, 2);
            this.SelectPackage.Name = "SelectPackage";
            this.SelectPackage.Size = new System.Drawing.Size(75, 23);
            this.SelectPackage.TabIndex = 2;
            this.SelectPackage.Text = "Select";
            this.SelectPackage.UseVisualStyleBackColor = true;
            this.SelectPackage.Click += new System.EventHandler(this.SelectPackage_Click);
            // 
            // FileComboBox
            // 
            this.FileComboBox.FormattingEnabled = true;
            this.FileComboBox.Location = new System.Drawing.Point(667, 8);
            this.FileComboBox.Name = "FileComboBox";
            this.FileComboBox.Size = new System.Drawing.Size(121, 21);
            this.FileComboBox.TabIndex = 0;
            this.FileComboBox.SelectedIndexChanged += new System.EventHandler(this.FileComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(13, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of partitions packed : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number of files packed : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(13, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "File size : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Written size : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(13, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "List of files packed";
            // 
            // FilesPacked
            // 
            this.FilesPacked.AutoSize = true;
            this.FilesPacked.Location = new System.Drawing.Point(13, 178);
            this.FilesPacked.Name = "FilesPacked";
            this.FilesPacked.Size = new System.Drawing.Size(143, 13);
            this.FilesPacked.TabIndex = 6;
            this.FilesPacked.Text = "-null                                       ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(509, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Select packed file to show info";
            // 
            // FileInfo
            // 
            this.FileInfo.Location = new System.Drawing.Point(370, 31);
            this.FileInfo.Name = "FileInfo";
            this.FileInfo.Size = new System.Drawing.Size(418, 448);
            this.FileInfo.TabIndex = 8;
            this.FileInfo.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(11, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "File location : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(12, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "File name : ";
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileName.Location = new System.Drawing.Point(77, 8);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(0, 13);
            this.FileName.TabIndex = 11;
            // 
            // FileLocation
            // 
            this.FileLocation.AutoSize = true;
            this.FileLocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileLocation.Location = new System.Drawing.Point(89, 29);
            this.FileLocation.Name = "FileLocation";
            this.FileLocation.Size = new System.Drawing.Size(0, 13);
            this.FileLocation.TabIndex = 12;
            // 
            // FileSize
            // 
            this.FileSize.AutoSize = true;
            this.FileSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileSize.Location = new System.Drawing.Point(72, 50);
            this.FileSize.Name = "FileSize";
            this.FileSize.Size = new System.Drawing.Size(0, 13);
            this.FileSize.TabIndex = 13;
            // 
            // WrittenSize
            // 
            this.WrittenSize.AutoSize = true;
            this.WrittenSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.WrittenSize.Location = new System.Drawing.Point(89, 71);
            this.WrittenSize.Name = "WrittenSize";
            this.WrittenSize.Size = new System.Drawing.Size(0, 13);
            this.WrittenSize.TabIndex = 14;
            // 
            // NumberFiles
            // 
            this.NumberFiles.AutoSize = true;
            this.NumberFiles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NumberFiles.Location = new System.Drawing.Point(144, 91);
            this.NumberFiles.Name = "NumberFiles";
            this.NumberFiles.Size = new System.Drawing.Size(0, 13);
            this.NumberFiles.TabIndex = 15;
            // 
            // NumberPartitions
            // 
            this.NumberPartitions.AutoSize = true;
            this.NumberPartitions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NumberPartitions.Location = new System.Drawing.Point(168, 113);
            this.NumberPartitions.Name = "NumberPartitions";
            this.NumberPartitions.Size = new System.Drawing.Size(0, 13);
            this.NumberPartitions.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 521);
            this.Controls.Add(this.SelectPackage);
            this.Controls.Add(this.UpgradeFile);
            this.Controls.Add(this.InfoPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AML Upgrade Info tool";
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Label NumberPartitions;
        private System.Windows.Forms.Label NumberFiles;
        private System.Windows.Forms.Label WrittenSize;
        private System.Windows.Forms.Label FileSize;
        private System.Windows.Forms.Label FileLocation;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox FileInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label FilesPacked;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FileComboBox;
        private System.Windows.Forms.TextBox UpgradeFile;
        private System.Windows.Forms.Button SelectPackage;
    }
}

