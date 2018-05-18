namespace AMLogicFlashTool
{
    partial class Connect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connect));
            this.ConnectionPanel = new System.Windows.Forms.Panel();
            this.Remember = new System.Windows.Forms.CheckBox();
            this.ConnectAdb = new System.Windows.Forms.Button();
            this.ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ConnectionStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Loader = new System.Windows.Forms.PictureBox();
            this.Status = new System.Windows.Forms.Label();
            this.ConnectionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loader)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectionPanel
            // 
            this.ConnectionPanel.Controls.Add(this.Remember);
            this.ConnectionPanel.Controls.Add(this.ConnectAdb);
            this.ConnectionPanel.Controls.Add(this.ip);
            this.ConnectionPanel.Controls.Add(this.label3);
            this.ConnectionPanel.Controls.Add(this.ConnectionStatus);
            this.ConnectionPanel.Controls.Add(this.label1);
            this.ConnectionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionPanel.Location = new System.Drawing.Point(0, 0);
            this.ConnectionPanel.Name = "ConnectionPanel";
            this.ConnectionPanel.Size = new System.Drawing.Size(397, 191);
            this.ConnectionPanel.TabIndex = 16;
            this.ConnectionPanel.Visible = false;
            // 
            // Remember
            // 
            this.Remember.AutoSize = true;
            this.Remember.ForeColor = System.Drawing.Color.Black;
            this.Remember.Location = new System.Drawing.Point(3, 139);
            this.Remember.Name = "Remember";
            this.Remember.Size = new System.Drawing.Size(88, 17);
            this.Remember.TabIndex = 21;
            this.Remember.Text = "Remember ip";
            this.Remember.UseVisualStyleBackColor = true;
            // 
            // ConnectAdb
            // 
            this.ConnectAdb.Location = new System.Drawing.Point(0, 162);
            this.ConnectAdb.Name = "ConnectAdb";
            this.ConnectAdb.Size = new System.Drawing.Size(298, 23);
            this.ConnectAdb.TabIndex = 20;
            this.ConnectAdb.Text = "Connect";
            this.ConnectAdb.UseVisualStyleBackColor = true;
            this.ConnectAdb.Click += new System.EventHandler(this.ConnectAdb_Click);
            // 
            // ip
            // 
            this.ip.BackColor = System.Drawing.Color.White;
            this.ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip.ForeColor = System.Drawing.Color.Black;
            this.ip.Location = new System.Drawing.Point(83, 115);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(100, 20);
            this.ip.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(0, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "IP Address: ";
            // 
            // ConnectionStatus
            // 
            this.ConnectionStatus.AutoSize = true;
            this.ConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.ConnectionStatus.Location = new System.Drawing.Point(304, 167);
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(78, 13);
            this.ConnectionStatus.TabIndex = 17;
            this.ConnectionStatus.Text = "Not connected";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 107);
            this.label1.TabIndex = 16;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Loader
            // 
            this.Loader.Image = global::AMLogicFlashTool.Properties.Resources.loading;
            this.Loader.Location = new System.Drawing.Point(117, 61);
            this.Loader.Name = "Loader";
            this.Loader.Size = new System.Drawing.Size(50, 50);
            this.Loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Loader.TabIndex = 17;
            this.Loader.TabStop = false;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.Location = new System.Drawing.Point(174, 78);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(129, 16);
            this.Status.TabIndex = 18;
            this.Status.Text = "Killing adb server";
            // 
            // Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 191);
            this.Controls.Add(this.ConnectionPanel);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Loader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Connect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to device";
            this.Shown += new System.EventHandler(this.Connect_Shown);
            this.ConnectionPanel.ResumeLayout(false);
            this.ConnectionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ConnectionPanel;
        private System.Windows.Forms.CheckBox Remember;
        private System.Windows.Forms.Button ConnectAdb;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ConnectionStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Loader;
        private System.Windows.Forms.Label Status;
    }
}