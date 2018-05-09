using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurnCardMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeBCM();
        }

        private void InitializeBCM()
        {
            // Add available drives
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    if (drive.VolumeLabel != null && !string.IsNullOrWhiteSpace(drive.VolumeLabel)) StorageDevice.Items.Add(drive.Name + " (" + drive.VolumeLabel + ")");
                    else StorageDevice.Items.Add(drive.Name);
                }
            }

            // Set default format FS
            FSType.SelectedIndex = 1;

            // Set default erase method
            EraseMethod.SelectedIndex = 1;
        }

        private string CreateBurnInfo(int erasebootloader, int eraseflash, int reboot, string file)
        {
            // Heading
            string BurnInfo = "; Created by BurnCardMaker V3.0.0 by ricky divjakovski\n; UNIX file format and must contain ONLY readable ASCII characters\n\n";

            // Common functions
            BurnInfo = BurnInfo + "[common]\n";
            BurnInfo = BurnInfo + "erase_bootloader=" + erasebootloader + "\n";
            BurnInfo = BurnInfo + "erase_flash=" + eraseflash + "\n";
            BurnInfo = BurnInfo + "reboot=" + reboot + "\n\n";
            // Package info
            BurnInfo = BurnInfo + "[erase_ex]\n";
            BurnInfo = BurnInfo + "package=" + file + "\n\n";

            // Return burn info
            return BurnInfo;
        }

        private async void CreateBurnCard_Click(object sender, EventArgs e)
        {
            if (StorageDevice.SelectedItem != null && Directory.Exists(StorageDevice.SelectedItem.ToString().Split('\\').First() + "\\"))
            {
                if (File.Exists(SelectedFile.Text))
                {
                    CreateBurnCard.Enabled = false;
                    string driveLetter = StorageDevice.SelectedItem.ToString().Split('\\').First();
                    string drivePath = driveLetter + "\\";
                    string fstype = FSType.SelectedItem.ToString();
                    string fullFile = SelectedFile.Text;
                    string fileName = Path.GetFileName(SelectedFile.Text);

                    // Formatting
                    if (FormatPartition.Checked)
                    {
                        CurrentTask.Text = "Formatting " + driveLetter + "..";
                        TaskProgress.Value = 50;
                        await Task.Delay(200);
                        Thread FormatThread = new Thread(delegate ()
                        {
                            Process process = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.CreateNoWindow = true;
                            startInfo.UseShellExecute = false;
                            startInfo.RedirectStandardOutput = true;
                            startInfo.FileName = "cmd.exe";
                            startInfo.Arguments = "/C \"FORMAT " + driveLetter + " /Y /FS:" + fstype + " /V:SDBurnCard /Q\"";
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                            process.Dispose();
                        });
                        FormatThread.Start();
                        while (FormatThread.IsAlive) Application.DoEvents();
                    }

                    // Burn info ini file
                    int erasebootloader = 0;
                    int eraseflash = 0;
                    int reboot = 0;
                    if (EraseBootloader.Checked) erasebootloader = 1;
                    if (EraseFlash.Checked) eraseflash = EraseMethod.SelectedIndex + 1;
                    if (Reboot.Checked) reboot = 1;
                    CurrentTask.Text = "Generating burn info..";
                    TaskProgress.Value = 50;
                    await Task.Delay(200);
                    string BurnInfo = CreateBurnInfo(erasebootloader, eraseflash, reboot, fileName);
                    if (File.Exists(drivePath + "aml_sdc_burn.ini")) File.Delete(drivePath + "aml_sdc_burn.ini");
                    StreamWriter BurnInfoWriter = new StreamWriter(drivePath + "aml_sdc_burn.ini");
                    BurnInfoWriter.Write(BurnInfo);
                    BurnInfoWriter.Dispose();

                    // Burn info binary file
                    CurrentTask.Text = "Extracting burn UBOOT..";
                    TaskProgress.Value = 25;
                    await Task.Delay(200);
                    if (File.Exists(drivePath + "aml_sdc_burn.UBOOT")) File.Delete(drivePath + "aml_sdc_burn.UBOOT");
                    TaskProgress.Value = 50;
                    BurnInfoExtractor UbootExtractor = new BurnInfoExtractor();
                    TaskProgress.Value = 75;
                    UbootExtractor.Unpack(fullFile, drivePath);

                    // Copy upgrade package
                    CurrentTask.Text = "Copying upgrade package..";
                    TaskProgress.Value = 0;
                    await Task.Delay(200);
                    if (File.Exists(drivePath + fileName)) File.Delete(drivePath + fileName);
                    FileStream reader = new FileStream(fullFile, FileMode.Open);
                    FileStream writer = new FileStream(drivePath + fileName, FileMode.Create);
                    long FileSize = reader.Length + 1;
                    while (reader.Position <= FileSize)
                    {
                        writer.WriteByte((byte)reader.ReadByte());
                        TaskProgress.Value = Convert.ToInt32(Math.Round((double)(100 * reader.Position) / FileSize));
                    }
                    reader.Dispose();
                    writer.Dispose();

                    // Complete
                    CurrentTask.Text = "";
                    TaskProgress.Value = 0;
                    CreateBurnCard.Enabled = true;
                    MessageBox.Show("The task has complete", "Successfully burnt.");
                }
                else MessageBox.Show("Selected upgrade package does not exist", "Please select a valid file");
            }
            else MessageBox.Show("Drive does not exist.", "Please select a valid drive");
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select upgrade package";
            ofd.Filter = "Raw disk image (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                SelectedFile.Text = ofd.FileName;
            }
        }

        private void FormatPartition_CheckedChanged(object sender, EventArgs e)
        {
            if (FormatPartition.Checked) FSType.Enabled = true;
            else FSType.Enabled = false;
        }

        private void EraseFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (EraseFlash.Checked) EraseMethod.Enabled = true;
            else EraseMethod.Enabled = false;
        }
    }

    public class BurnInfoExtractor
    {
        // Splitting parts of the aml upgrade package by hex address
        private void HexSplit(string inputFile, string outputFile, string startAddress, string endAddress)
        {
            FileStream hexReader = new FileStream(inputFile, FileMode.Open);
            FileStream hexWriter = new FileStream(outputFile, FileMode.Create);

            long StartAddress = Convert.ToInt64(startAddress.ToUpper(), 16);
            long EndAddress = Convert.ToInt64(endAddress.ToUpper(), 16);

            long bytecount = StartAddress;
            hexReader.Position = StartAddress;
            while (bytecount <= EndAddress)
            {
                hexWriter.WriteByte((byte)hexReader.ReadByte());
                bytecount++;
            }

            hexReader.Dispose();
            hexWriter.Dispose();
        }

        // Convert hex values to string
        static string HexToString(string HexString)
        {
            string returnString = "";
            HexString = HexString.Replace(" ", "");
            for (int i = 0; i < HexString.Length / 2; i++)
            {
                string hexChar = HexString.Substring(i * 2, 2);
                int hexValue = Convert.ToInt32(hexChar, 16);
                returnString += Char.ConvertFromUtf32(hexValue);
            }
            return returnString;
        }

        // Extracting UBOOT
        public void Unpack(string inputFile, string outputFolder)
        {
            // Split the head so we can r/w from original
            HexSplit(inputFile, outputFolder + "\\head.BIN", "00000000", "000028C0");

            // Line contents(16) and previous line contents(16)
            string LineContent = "";
            string PreviousLineContent = "";

            // File properties read from file
            string FileName = "";
            string FileSize = "";
            string FileExtension = "";
            string StartAddress = "";
            string EndAddress = "";

            // Bytecount and block and bool for when finished
            long CurrentByte = 0;
            byte currentblock;
            int ByteCount = 0;
            bool aml_sdc_burn = false;

            // Read the new header
            FileStream hexReader = new FileStream(outputFolder + "\\head.BIN", FileMode.Open);

            // Read by byte and split at correct address
            while (CurrentByte <= hexReader.Length)
            {
                currentblock = (byte)hexReader.ReadByte();
                if (ByteCount < 16)
                {
                    LineContent = LineContent + currentblock.ToString("X2") + " ";
                    ByteCount++;
                    if (LineContent.StartsWith("61 6D 6C 5F 73 64 63 5F 62 75 72 6E"))
                    {
                        if (LineContent.StartsWith("61 6D 6C 5F 73 64 63 5F 62 75 72 6E") && !aml_sdc_burn) { FileName = HexToString("61 6D 6C 5F 73 64 63 5F 62 75 72 6E"); aml_sdc_burn = true; }
                        if (FileExtension != "" && StartAddress != "" && FileSize != "" && EndAddress != "" && FileName != "")
                        {
                            HexSplit(inputFile, outputFolder + "\\" + FileName + FileExtension, StartAddress, EndAddress);
                            CurrentByte = hexReader.Length + 1;
                        }
                    }
                }
                else
                {
                    if (LineContent.StartsWith("55 42 4F 4F 54"))
                    {
                        if (LineContent.StartsWith("55 42 4F 4F 54")) FileExtension = ".UBOOT";

                        if (PreviousLineContent.Split()[3] == "0") StartAddress = PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];
                        else StartAddress = PreviousLineContent.Split()[3] + PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];

                        if (PreviousLineContent.Split()[11] == "0") FileSize = PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];
                        else FileSize = PreviousLineContent.Split()[11] + PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];

                        EndAddress = ((Convert.ToInt64(StartAddress.ToUpper(), 16) + (Convert.ToInt64(FileSize.ToUpper(), 16))) - 1).ToString("X");
                    }
                    PreviousLineContent = LineContent;
                    LineContent = currentblock.ToString("X2") + " ";

                    ByteCount = 1;
                }
                CurrentByte++;
            }
            hexReader.Dispose();

            // Delete generated header
            File.Delete(outputFolder + "\\head.BIN");
            
        }
    }
}
