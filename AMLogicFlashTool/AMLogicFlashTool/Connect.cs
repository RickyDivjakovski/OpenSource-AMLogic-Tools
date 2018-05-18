using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoolADB;

namespace AMLogicFlashTool
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        ADBClient Client = new ADBClient();
        public bool Connected = false;

        private void Connect_Shown(object sender, EventArgs e)
        {
            Client.KillServer();
            ConnectionPanel.Visible = true;
            if (AMLogicFlashTool.Properties.Settings.Default.ip != null && !string.IsNullOrWhiteSpace(AMLogicFlashTool.Properties.Settings.Default.ip))
            {
                Remember.Checked = true;
                ip.Text = AMLogicFlashTool.Properties.Settings.Default.ip;
            }
        }

        private void ConnectAdb_Click(object sender, EventArgs e)
        {
            Status.Text = "Connecting";
            ConnectionPanel.Visible = false;

            ConnectAdb.Enabled = false;
            ip.Enabled = false;
            Remember.Enabled = false;
            if (Remember.Checked && ip.Text != null && !string.IsNullOrWhiteSpace(ip.Text))
            {
                AMLogicFlashTool.Properties.Settings.Default.ip = ip.Text;
                AMLogicFlashTool.Properties.Settings.Default.Save();
            }
            ConnectDevice();
        }

        private void ConnectDevice()
        {
            Client.Connect(ip.Text);
            ip.Enabled = false;
            ConnectAdb.Enabled = false;
            Remember.Enabled = false;
            if (ip.Text != null && !string.IsNullOrWhiteSpace(ip.Text) && Client.Devices()[0].Split(':').First() == ip.Text)
            {
                ConnectionStatus.Text = "Connected";
                ConnectionStatus.ForeColor = Color.LimeGreen;
                Thread.Sleep(1000);
                Connected = true;
                this.Close();
            }
            else
            {
                ConnectAdb.Enabled = true;
                ip.Enabled = true;
                Remember.Enabled = true;
                Connected = false;
            }
            ConnectionPanel.Visible = true;
        }
    }
}
