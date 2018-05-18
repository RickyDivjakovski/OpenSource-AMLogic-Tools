using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoolADB;
using AMLUnpacker;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace AMLogicFlashTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FlashPanel.Enabled = false;
            PullFromDevice.Enabled = false;
            BackupPanel.Enabled = false;
        }

        bool IsConnected = false;

        private void ShowConnect_Click(object sender, EventArgs e)
        {
            ShowConnectBox();
        }

        private void ShowConnectBox()
        {
            Connect connector = new Connect();
            connector.ShowDialog();
            IsConnected = connector.Connected;
            if (IsConnected)
            {
                ConnectedLabel.Text = "Connected";
                ConnectedLabel.ForeColor = Color.LimeGreen;
                FlashPanel.Enabled = true;
                PullFromDevice.Enabled = true;
                BackupPanel.Enabled = true;
                ShowConnect.Enabled = false;
            }
            connector.Dispose();
        }

        ADBClient Client = new ADBClient();
        Unpacker Unpacker = new Unpacker();
        KernelUnpacker KernelUnpacker = new KernelUnpacker();

        private void BackgroundShell(string executable, string command)
        {
            Thread newThread = new Thread(delegate ()
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo(executable, command);
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
                proc.Dispose();
            });
            newThread.Start();
            while (newThread.IsAlive) Application.DoEvents();
        }

        // Flashing
        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "img files (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                ImgPath.Text = ofd.FileName;
            }
        }

        private void Flash_Click(object sender, EventArgs e)
        {
            if (FlashBoot.Checked != true && FlashRecovery.Checked != true) MessageBox.Show("Select partition to flash", "Error");
            else
            {
                if (!File.Exists(ImgPath.Text)) MessageBox.Show("Cannot find the file you have selected", "Error");
                else
                {
                    if (!ImgPath.Text.EndsWith(".img")) MessageBox.Show("The file you have selected is not correct format.\nPlease select a .img file", "Error");
                    else
                    {
                        string filename = Path.GetFileName(ImgPath.Text);
                        FlashOptions.Enabled = false;
                        RebootOptions.Enabled = false;
                        FlashOutput.Text = "Flashing started.\nDo not turn off or disconnect device";
                        FlashOutput.Text = FlashOutput.Text + "\n\nPlease wait..\nSending " + filename + " to device..";
                        Client.Execute("mkdir /sdcard/amltmp", true);
                        Client.Push(ImgPath.Text, "/sdcard/amltmp/");
                        Client.Execute("ls /sdcard/amltmp", true);
                        if (!Client.Output.Contains(filename)) MessageBox.Show("Failed to send file", "Error");
                        else
                        {
                            FlashOutput.Text = FlashOutput.Text + "\nFile sent";
                            if (FlashBoot.Checked == true)
                            {
                                FlashOutput.Text = FlashOutput.Text + "\n\nFlashing boot..";
                                Client.Execute("dd if=/sdcard/amltmp/" + filename + " of=/dev/block/boot", true);
                                FlashOutput.Text = FlashOutput.Text + "\n" + Client.Output;
                            }
                            else if (FlashRecovery.Checked == true)
                            {
                                FlashOutput.Text = FlashOutput.Text + "\n\nFlashing recovery..";
                                Client.Execute("dd if=/sdcard/amltmp/" + filename + " of=/dev/block/recovery", true);
                                FlashOutput.Text = FlashOutput.Text + "\n" + Client.Output;
                            }
                            else if (FlashCustom.Checked == true)
                            {
                                FlashOutput.Text = FlashOutput.Text + "\n\nFlashing custom block..";
                                Client.Execute("dd if=/sdcard/amltmp/" + filename + " of=" + FlashBlock, true);
                                FlashOutput.Text = FlashOutput.Text + "\n" + Client.Output;
                            }
                            FlashOutput.Text = FlashOutput.Text + "\n\nSUCCESS!";
                        }
                        Client.Execute("rm -rf /sdcard/amltmp", true);
                    }
                }
            }
            FlashOptions.Enabled = true;
            RebootOptions.Enabled = true;
        }

        private void RebootSystem_Click(object sender, EventArgs e)
        {
            Client.Reboot(ADBClient.BootState.System);
        }

        private void RebootRecovery_Click(object sender, EventArgs e)
        {
            Client.Reboot(ADBClient.BootState.Recovery);
        }

        // Porting

        private void AllowChanging_CheckedChanged(object sender, EventArgs e)
        {
            if (AllowChanging.Checked == true)
            {
                BlockPath.Enabled = true;
            }
            else
            {
                BlockPath.Text = "/dev/block/recovery";
                BlockPath.Enabled = false;
            }
        }

        private void PullFromDevice_CheckedChanged(object sender, EventArgs e)
        {
            PullRecovery.Enabled = true;
            LocalRecovery.Enabled = false;
            ExtractPackage.Enabled = false;
        }

        private void PullFromLocal_CheckedChanged(object sender, EventArgs e)
        {
            PullRecovery.Enabled = false;
            LocalRecovery.Enabled = true;
            ExtractPackage.Enabled = false;
        }

        private void ExtractFromPackage_CheckedChanged(object sender, EventArgs e)
        {
            PullRecovery.Enabled = false;
            LocalRecovery.Enabled = false;
            ExtractPackage.Enabled = true;
        }

        private void SelectRecoveryLocal_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "recovery img files (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                RecoveryPath.Text = ofd.FileName;
            }
        }

        private void SelectRecoveryUpgrade_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "upgrade img files (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UpgradePath.Text = ofd.FileName;
            }
        }

        private void UpdateRecoveryPort(int progress)
        {
            if (PortProgressBar.Visible == false) PortProgressBar.Visible = true;
            if (PortProgress.Visible == false) PortProgress.Visible = true;

            PortProgressBar.Value = progress;
            PortProgress.Text = progress + "%";

            if (progress == 100)
            {
                PortProgressBar.Visible = false;
                PortProgress.Visible = false;
            }
        }

        private void PortRecovery_Click(object sender, EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.Filter = "Ported recovery.img (*.img) | *.img";
            DialogResult res = saver.ShowDialog();
            if (res == DialogResult.OK)
            {
                PortRecovery.Enabled = false;
                LocalRecovery.Enabled = false;
                PullRecovery.Enabled = false;
                PullFromDevice.Enabled = false;
                PullFromLocal.Enabled = false;
                string recovery = RecoveryPath.Text;
                bool errorFlag = false;
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp")) Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp", true);
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp");
                if (PullFromDevice.Checked)
                {
                    UpdateRecoveryPort(10);
                    PortLabel.Text = "Copying recovery block to /sdcard..";
                    Client.Execute("dd if=" + BlockPath.Text + " of=/sdcard/tmp.img", true);
                    if (File.Exists(Application.StartupPath + "\\tmp.img")) File.Delete(Application.StartupPath + "\\tmp.img");
                    PortLabel.Text = "Pulling recovery from device..";
                    Client.Pull("/sdcard/tmp.img", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\recovery.img");
                    recovery = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\recovery.img";
                }
                else if (ExtractFromPackage.Checked)
                {
                    Unpacker.UnpackPartition(UpgradePath.Text, "recovery.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp");
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\recovery.PARTITION"))
                    {
                        File.Move(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\recovery.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\recovery.img");
                        recovery = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\recovery.img";
                    }
                    else
                    {
                        errorFlag = true;
                    }
                }
                else if (PullFromLocal.Checked)
                {
                    if (!File.Exists(recovery))
                    {
                        MessageBox.Show("Cannot find specified file - \n\"" + recovery + "\"", "Error");
                        errorFlag = true;
                    }
                }
                if (errorFlag == false)
                {
                    UpdateRecoveryPort(20);
                    PortLabel.Text = "Unpacking stock recovery..";
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\unpacked");
                    Thread KernelUnpackThread = new Thread(delegate ()
                    {
                        KernelUnpacker.Unpack(recovery, AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\unpacked");
                    });
                    KernelUnpackThread.Start();
                    while (KernelUnpackThread.IsAlive) Application.DoEvents();

                    if (File.Exists(Application.StartupPath + "\\tmp\\unpacked\\ramdisk\\default.prop"))
                    {
                        UpdateRecoveryPort(50);
                        Directory.Delete(Application.StartupPath + "\\tmp\\unpacked\\ramdisk", true);
                        PortLabel.Text = "Patching kernel data..";
                        Thread PortThread = new Thread(delegate ()
                        {
                            ProcessStartInfo procStartInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " x \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\Twrp-rd.zip" + "\" -o\"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\unpacked\\ramdisk" + "\" -y");
                            procStartInfo.UseShellExecute = false;
                            procStartInfo.CreateNoWindow = true;
                            procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            Process proc = new Process();
                            proc.StartInfo = procStartInfo;
                            proc.Start();
                            proc.WaitForExit();
                            proc.Dispose();
                        });
                        PortThread.Start();
                        while (PortThread.IsAlive) Application.DoEvents();

                        UpdateRecoveryPort(60);
                        PortLabel.Text = "Rebuilding recovery..";
                        Thread KernelRepackThread = new Thread(delegate ()
                        {
                            KernelUnpacker.Repack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\unpacked", saver.FileName);
                        });
                        KernelRepackThread.Start();
                        while (KernelRepackThread.IsAlive) Application.DoEvents();

                        if (File.Exists(saver.FileName))
                        {
                            UpdateRecoveryPort(90);
                            PortLabel.Text = "Cleaning up working directory..";
                            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp")) Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp", true);

                            PortLabel.Text = "SUCCESS!";
                            UpdateRecoveryPort(100);

                            if (FlashAfterPort.Checked)
                            {
                                FlashRecovery.Checked = true;
                                ImgPath.Text = saver.FileName;
                                tabControl1.SelectedIndex = 0;
                                Flash.PerformClick();
                            }
                        }
                        else
                        {
                            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp", true);
                            MessageBox.Show("Failed to repack recovery", "ERROR");
                        }
                    }
                    else
                    {
                        Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp", true);
                        MessageBox.Show("Failed to unpack recovery", "ERROR");
                    }
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp")) Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp", true);
                }
                PortRecovery.Enabled = false;
                LocalRecovery.Enabled = false;
                PullRecovery.Enabled = false;
                PullFromDevice.Enabled = false;
                PullFromLocal.Enabled = false;
            }
        }


        // Backups
        private void BackupFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult res = fbd.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (BackupRecovery.Checked)
                {
                    BackupLabel.Text = "Generating kernel..";
                    Client.Execute("dd if=" + BlockPath.Text + " of=/sdcard/tmp.img", true);
                    BackupLabel.Text = "Pulling kernel..";
                    Client.Pull("/sdcard/tmp.img", fbd.SelectedPath + "\\recovery.img");
                    Client.Execute("rm /sdcard/tmp.img", false);
                    if (!File.Exists(fbd.SelectedPath + "\\recovery.img")) MessageBox.Show("Failed to backup recovery", "Error");
                }
                if (BackupKernel.Checked)
                {
                    BackupLabel.Text = "Generating recovery..";
                    Client.Execute("dd if=" + BlockPath.Text + " of=/sdcard/tmp.img", true);
                    BackupLabel.Text = "Pulling recovery..";
                    Client.Pull("/sdcard/tmp.img", fbd.SelectedPath + "\\boot.img");
                    Client.Execute("rm /sdcard/tmp.img", false);
                    if (!File.Exists(fbd.SelectedPath + "\\boot.img")) MessageBox.Show("Failed to backup kernel", "Error");
                }
                BackupLabel.Text = "";
                MessageBox.Show("Completed backup operation", "Done");
            }
        }

        private void BackupZip_Click(object sender, EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.Filter = "Flashable zip (*.zip) | *.zip";
            DialogResult res = saver.ShowDialog();
            if (res == DialogResult.OK)
            {
                BackupLabel.Text = "Copying common binary..";
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\presigned.zip", saver.FileName);
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup");
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\META-INF\\com\\google\\android");
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\update-binary", AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\META-INF\\com\\google\\android\\update-binary");

                string UpdaterScript = UpdaterScript =
            "ui_print(\"******************************************\");\n" +
            "ui_print(\"      Installing restore package\");\n" +
            "ui_print(\"******************************************\");\n" +
            "ui_print(\"    Generated by AMLogic Flash Tool\");\n\n";

                if (BackupKernel.Checked)
                {
                    BackupLabel.Text = "Generating kernel..";
                    Client.Execute("dd if=/dev/block/boot of=/sdcard/tmp.img", true);
                    BackupLabel.Text = "Pulling kernel..";
                    Client.Pull("/sdcard/tmp.img", AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\boot.img");
                    Client.Execute("rm /sdcard/tmp.img", false);
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\boot.img")) MessageBox.Show("Failed to backup kernel", "Error");
                    else
                    {
                        UpdaterScript = UpdaterScript + "ui_print(\"Unpacking Kernel\");\npackage_extract_file(\"boot.img\", \"/dev/block/boot\");\n\n";
                        BackupLabel.Text = "Compressing kernel..";
                        BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx9 -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\boot.img" + "\"");
                    }
                }
                if (BackupRecovery.Checked)
                {
                    BackupLabel.Text = "Generating recovery..";
                    Client.Execute("dd if=/dev/block/recovery of=/sdcard/tmp.img", true);
                    BackupLabel.Text = "Pulling recovery..";
                    Client.Pull("/sdcard/tmp.img", AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\recovery.img");
                    Client.Execute("rm /sdcard/tmp.img", false);
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\recovery.img")) MessageBox.Show("Failed to backup recovery", "Error");
                    else
                    {
                        UpdaterScript = UpdaterScript + "ui_print(\"Unpacking Kernel\");\npackage_extract_file(\"recovery.img\", \"/dev/block/recovery\");\n\n";
                        BackupLabel.Text = "Compressing recovery..";
                        BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx9 -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\recovery.img" + "\"");
                    }
                }

                BackupLabel.Text = "Generating updater-script..";
                UpdaterScript = UpdaterScript + "ui_print(\"******************************************\");\n" +
            "ui_print(\"        Successfully Installed\");\n" +
            "ui_print(\"******************************************\");\n" +
            "set_progress(1.0);";

                StreamWriter WriteUpdaterScript = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\META-INF\\com\\google\\android\\updater-script");
                WriteUpdaterScript.Write(UpdaterScript);
                WriteUpdaterScript.Dispose();

                BackupLabel.Text = "Compressing META-INF..";
                BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx9 -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup\\META-INF" + "\"");

                BackupLabel.Text = "Cleaning up..";
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmpBackup", true);
                BackupLabel.Text = "";
                MessageBox.Show("Completed backup operation", "Done");
            }
        }

        // ADVANCED
        private void AllowCustomBlockFlashing_CheckedChanged(object sender, EventArgs e)
        {
            if (AllowCustomBlockFlashing.Checked)
            {
                FlashCustom.Enabled = true;
                FlashBlock.Enabled = true;
            }
            else
            {
                FlashCustom.Enabled = false;
                FlashBlock.Enabled = false;
            }
        }
    }
}
