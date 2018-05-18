using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AMLUnpacker;

namespace AMLUpgradeInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Unpacker unpacker = new Unpacker();

        private void SelectPackage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select upgrade package";
            ofd.Filter = "Amlogic upgrade package (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UpgradeFile.Text = ofd.FileName;
                FileName.Text = Path.GetFileName(ofd.FileName);
                FileLocation.Text = Path.GetDirectoryName(ofd.FileName);
                FileSize.Text = new FileInfo(ofd.FileName).Length.ToString();
                WrittenSize.Text = unpacker.UpgradeInfo(ofd.FileName, Unpacker.UpgradeInfoType.FileSize);
                FilesPacked.Text = unpacker.GetUpgradeContent(ofd.FileName);
                int partitions = 0;
                int files = 0;
                foreach (string s in FilesPacked.Text.Split('\n'))
                {
                    if (Path.GetExtension(s) == ".PARTITION") partitions++;
                    else files++;
                    if (s != null && !string.IsNullOrWhiteSpace(s))
                    {
                        FileComboBox.Items.Add(s);
                    }
                }
                NumberFiles.Text = files.ToString();
                NumberPartitions.Text = partitions.ToString();

                InfoPanel.Enabled = true;
            }
        }

        private void FileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo.Text = "Start address:" + unpacker.PartitionInfo(UpgradeFile.Text, FileComboBox.Text, Unpacker.PartitionInfoType.StartAddress) + "\n" +
                "End address:" + unpacker.PartitionInfo(UpgradeFile.Text, FileComboBox.Text, Unpacker.PartitionInfoType.EndAddress) + "\n" +
                "File size:" + unpacker.PartitionInfo(UpgradeFile.Text, FileComboBox.Text, Unpacker.PartitionInfoType.FileSize) + "\n" +
                "File name:" + unpacker.PartitionInfo(UpgradeFile.Text, FileComboBox.Text, Unpacker.PartitionInfoType.Filename) + "\n" +
                "File extension:" + unpacker.PartitionInfo(UpgradeFile.Text, FileComboBox.Text, Unpacker.PartitionInfoType.FileExtension);
        }
    }
}
