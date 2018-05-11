using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;

namespace CustomizationTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\build.prop")) { MainTab.Enabled = true; LoadInfo(); AppsDirectory.SelectedIndex = 0; }
            else MainTab.Enabled = false;
        }

        // Bin
        string bin = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\";

        // Background shell
        private async void BackgroundShell(string executable, string command)
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

        // ================================================>> UNPACKING
        AMLImageUnpacker unpacker = new AMLImageUnpacker();
        KernelUtility kernelUnpacker = new KernelUtility();
        RomPacker packer = new RomPacker();

        private void UnpackImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "amlogic package (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UnpackingLoader.Visible = true;
                MainTab.Enabled = false;
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp")) Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp", true);
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp");

                // Level 1
                StatusLabel.Text = "Unpacking level1(Splitting package)..";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1");
                Thread level1Thread = new Thread(delegate ()
                {
                    unpacker.UnpackUpgradePackage(ofd.FileName, AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1");
                });
                level1Thread.Start();
                while (level1Thread.IsAlive) Application.DoEvents();

                StatusLabel.Text = "Verifying split data..";
                foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1"))
                {
                    if (file.EndsWith(".VERIFY"))
                    {
                        StatusLabel.Text = "Verifying " + Path.GetFileNameWithoutExtension(file) + " partition..";
                        Thread VerifyThread = new Thread(delegate ()
                        {
                            if (unpacker.Verify(file, Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + ".PARTITION") != true) MessageBox.Show("SHA1 sum mismatch for " + Path.GetFileNameWithoutExtension(file) + ".\nFile may be corrupt.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                        VerifyThread.Start();
                        while (VerifyThread.IsAlive) Application.DoEvents();
                    }
                }

                // Level 2
                StatusLabel.Text = "Unpacking level2(Unpacking system)..";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2");
                Thread level2SystemThread = new Thread(delegate ()
                {
                    ProcessStartInfo procStartInfo = new ProcessStartInfo(bin + "ImgExtractor.exe", "\"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\system.PARTITION" + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system" + "\"");
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process proc = new Process();
                    proc.StartInfo = procStartInfo;
                    proc.Start();
                    proc.WaitForExit();
                    proc.Dispose();
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system_statfile.txt");
                });
                level2SystemThread.Start();
                while (level2SystemThread.IsAlive) Application.DoEvents();

                StatusLabel.Text = "Unpacking level2(Unpacking kernel)..";
                Thread level2KernelThread = new Thread(delegate ()
                {
                    kernelUnpacker.Unpack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\boot.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot");
                });
                level2KernelThread.Start();
                while (level2KernelThread.IsAlive) Application.DoEvents();

                StatusLabel.Text = "Unpacking level2(Unpacking recovery)..";
                Thread level2RecoveryThread = new Thread(delegate ()
                {
                    kernelUnpacker.Unpack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery");
                });
                level2RecoveryThread.Start();
                while (level2RecoveryThread.IsAlive) Application.DoEvents();

                // Level 3
                StatusLabel.Text = "Unpacking level3(Unpacking bootanimation)..";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3");
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation");
                ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\media\\bootanimation.zip", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation");

                StatusLabel.Text = "Unpacking level3(Unpacking bootlogo)..";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootlogo");
                unpacker.UnpackLogo(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\logo.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootlogo");

                StatusLabel.Text = "Unpacking level3(Unpacking wallpaper)..";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper");
                string wallpaperPath = "";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher.apk"))
                    wallpaperPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher.apk";
                else if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher\\SunvellWin8Launcher.apk"))
                    wallpaperPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher\\SunvellWin8Launcher.apk";
                if (wallpaperPath != null && !string.IsNullOrWhiteSpace(wallpaperPath))
                {
                    ZipArchive wallpaper = ZipFile.OpenRead(wallpaperPath);
                    foreach (ZipArchiveEntry entry in wallpaper.Entries) if (entry.FullName.Equals(@"res/drawable/bg.png")) entry.ExtractToFile(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\bg.png");
                }

                MainTab.Enabled = true;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\build.prop")) { MainTab.Enabled = true; LoadInfo(); AppsDirectory.SelectedIndex = 0; MessageBox.Show("Successfully unpacked.", "Unpack complete"); }
                else MainTab.Enabled = false;
                StatusLabel.Text = "Done.";
                UnpackingLoader.Visible = false;
            }
        }


        // ================================================>> PRODUCT INFO
        private void LoadInfo()
        {
            UnpackingLoader.Visible = true;
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\build.prop");
            foreach (string s in reader.ReadToEnd().Split('\n'))
            {
                if (s.StartsWith("ro.build.version.release")) Version.Text = s.Split('=').Last();
                if (s.StartsWith("ro.product.model")) Model.Text = s.Split('=').Last();
                if (s.StartsWith("ro.product.brand")) Vendor.Text = s.Split('=').Last();
                if (s.StartsWith("ro.product.device")) Product.Text = s.Split('=').Last();
                if (s.StartsWith("ro.build.version.security_patch")) SecurityPatch.Text = s.Split('=').Last();
                if (s.StartsWith("ro.build.display.id")) Build.Text = s.Split('=').Last().Substring(s.Split('=').Last().Length - 4);
            }
            reader.Dispose();

            SystemSize.Text = (RomSize(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system") / 1024 / 1024).ToString();
            SystemFiles.Text = FilesAmount(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system").ToString();
            SystemApps.Text = AppsAmount(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system").ToString();
            UnpackingLoader.Visible = false;
        }

        public static long RomSize(string searchDirectory)
        {
            var files = Directory.EnumerateFiles(searchDirectory);
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();
            var directories = Directory.EnumerateDirectories(searchDirectory);
            var subDirSize = (from directory in directories select RomSize(directory)).Sum();
            return currentSize + subDirSize;
        }

        public static int AppsAmount(string searchDirectory) { return Directory.GetFiles(searchDirectory, "*.apk", SearchOption.AllDirectories).Count(); }
        public static int FilesAmount(string searchDirectory) { return Directory.GetFiles(searchDirectory, "*", SearchOption.AllDirectories).Count(); }


        // ================================================>> USER INTERFACE
        bool playingBootanimation = false;

        private async void BootanimationButton_Click(object sender, EventArgs e)
        {
            BootAnimationPanel.BringToFront();
            BootAnimationPanel.Dock = DockStyle.Fill;
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation"))
            {
                UnpackingLoader.Visible = true;
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation");
                ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\media\\bootanimation.zip", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation");
                UnpackingLoader.Visible = false;
            }
            if (!playingBootanimation)
            {
                playingBootanimation = true;
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation\\desc.txt");
                string fpsdata = reader.ReadLine();
                reader.Dispose();
                BootanimationFPS.Text = fpsdata.Split()[0] + "x" + fpsdata.Split()[1] + "@" + fpsdata.Split()[2] + "fps";
                foreach (string s in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation"))
                {
                    foreach (string f in Directory.GetFiles(s))
                    {
                        if (BootanimationPicturebox.Image != null) BootanimationPicturebox.Image.Dispose();
                        BootanimationPicturebox.Image = Image.FromFile(f);
                        await Task.Delay(20);
                    }
                }
                playingBootanimation = false;
            }
        }

        private async void PlayBootanimation_Click(object sender, EventArgs e)
        {
            if (!playingBootanimation)
            {
                playingBootanimation = true;
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation\\desc.txt");
                string fpsdata = reader.ReadLine();
                reader.Dispose();
                BootanimationFPS.Text = fpsdata.Split()[0] + "x" + fpsdata.Split()[0] + "@" + fpsdata.Split()[0] + "fps";
                foreach (string s in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation"))
                {
                    foreach (string f in Directory.GetFiles(s))
                    {
                        if (BootanimationPicturebox.Image != null) BootanimationPicturebox.Image.Dispose();
                        BootanimationPicturebox.Image = Image.FromFile(f);
                        await Task.Delay(20);
                    }
                }
                playingBootanimation = false;
            }
        }

        private void BootanimationChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "bootanimation files (*.zip) | *.zip";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UnpackingLoader.Visible = true;
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\media\\bootanimation.zip");
                File.Copy(ofd.FileName, AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\media\\bootanimation.zip");
                if (BootanimationPicturebox.Image != null) BootanimationPicturebox.Image.Dispose();
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootanimation", true);
                BootanimationButton.PerformClick();
                UnpackingLoader.Visible = false;
            }
        }

        private void BootLogoButton_Click(object sender, EventArgs e)
        {
            BootlogoPanel.BringToFront();
            BootlogoPanel.Dock = DockStyle.Fill;
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootlogo"))
            {
                UnpackingLoader.Visible = true;
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootlogo");
                unpacker.UnpackLogo(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\logo.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootlogo");
                UnpackingLoader.Visible = false;
            }

            if (BootlogoPicturebox.Image != null) BootlogoPicturebox.Image.Dispose();
            BootlogoPicturebox.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\bootlogo\\bootup.png");
        }

        private void WallpaperButton_Click(object sender, EventArgs e)
        {
            WallpaperPanel.BringToFront();
            WallpaperPanel.Dock = DockStyle.Fill;
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper"))
            {
                UnpackingLoader.Visible = true;
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper");

                string wallpaperPath = "";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher.apk"))
                    wallpaperPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher.apk";
                else if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher\\SunvellWin8Launcher.apk"))
                    wallpaperPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher\\SunvellWin8Launcher.apk";

                if (wallpaperPath != null && !string.IsNullOrWhiteSpace(wallpaperPath))
                {
                    ZipArchive wallpaper = ZipFile.OpenRead(wallpaperPath);
                    foreach (ZipArchiveEntry entry in wallpaper.Entries) if (entry.FullName.Equals(@"res/drawable/bg.png")) entry.ExtractToFile(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\bg.png");
                }
                UnpackingLoader.Visible = false;
            }

            if (WallpaperPicturebox.Image != null) WallpaperPicturebox.Image.Dispose();
            WallpaperPicturebox.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\bg.png");
        }

        private void WallpaperChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UnpackingLoader.Visible = true;
                Image Wallpaper = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\bg.png");
                Image NewWallpaper = new Bitmap(Wallpaper.Width, Wallpaper.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(NewWallpaper);
                g.DrawImage(Image.FromFile(ofd.FileName), new Rectangle(0, 0, Wallpaper.Width, Wallpaper.Height));
                Wallpaper.Dispose();
                if (WallpaperPicturebox.Image != null) WallpaperPicturebox.Image.Dispose();
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\bg.png");
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\res");
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\res\\drawable");
                NewWallpaper.Save(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\res\\drawable\\bg.png");
                NewWallpaper.Dispose();
                string wallpaperPath = "";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher.apk"))
                    wallpaperPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher.apk";
                else if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher\\SunvellWin8Launcher.apk"))
                    wallpaperPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\app\\SunvellWin8Launcher\\SunvellWin8Launcher.apk";
                BackgroundShell(bin + "7za.exe", " u -mx9 -tzip -y \"" + wallpaperPath + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper\\res" + "\"");
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level3\\wallpaper", true);
                WallpaperButton.PerformClick();
                UnpackingLoader.Visible = false;
            }
        }

        // ================================================>> APPS
        private void AppsDirectory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAppsView();
        }

        private void UpdateAppsView()
        {
            AppsViewer.Items.Clear();
            if (AppsDirectory.SelectedItem != null && !string.IsNullOrWhiteSpace(AppsDirectory.SelectedItem.ToString()) && Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2" + AppsDirectory.SelectedItem.ToString().Replace("/", "\\")))
            {
                foreach (string subfolder in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2" + AppsDirectory.SelectedItem.ToString().Replace("/", "\\")))
                {
                    foreach (string app in Directory.GetFiles(subfolder)) if (app.EndsWith(".apk"))
                        {
                            ListViewItem lvi = new ListViewItem(Path.GetFileName(subfolder), 0);
                            AppsViewer.Items.Add(lvi);
                        }
                }
                foreach (string app in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2" + AppsDirectory.SelectedItem.ToString().Replace("/", "\\")))
                {
                    if (app.EndsWith(".apk"))
                    {
                        ListViewItem lvi = new ListViewItem(Path.GetFileNameWithoutExtension(app), 0);
                        AppsViewer.Items.Add(lvi);
                    }
                }
            }
        }

        private void RemoveApps_Click(object sender, EventArgs e)
        {
            if (AppsViewer.SelectedItems != null && AppsDirectory.SelectedItem != null && !string.IsNullOrWhiteSpace(AppsDirectory.SelectedItem.ToString()))
            {
                string appfile = AppsViewer.SelectedItems[0].Text;
                foreach (string subfolder in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2" + AppsDirectory.SelectedItem.ToString().Replace("/", "\\"))) if (subfolder.EndsWith(appfile)) Directory.Delete(subfolder, true);
                foreach (string app in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2" + AppsDirectory.SelectedItem.ToString().Replace("/", "\\"))) if (app.EndsWith(appfile + ".apk")) File.Delete(app);
                UpdateAppsView();
            }
        }

        private void AddApps_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "android apps (*.apk) | *.apk";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                File.Copy(ofd.FileName, AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2" + AppsDirectory.SelectedItem.ToString().Replace("/", "\\") + "\\" + Path.GetFileName(ofd.FileName));
                UpdateAppsView();
            }
        }

        // ================================================>> KERNEL/RECOVERY
        // Kernel
        private void OpenKernel_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot\\ramdisk");
        }

        private void KernelInitRc_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot\\ramdisk\\init.rc");
        }

        private void KernelInitAmlogicRc_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot\\ramdisk\\init.amlogic.rc");
        }

        private void KernelInitAmlogicBoardRc_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot\\ramdisk\\init.amlogic.board.rc");
        }

        private void KernelDefaultProp_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot\\ramdisk\\default.prop");
        }

        private void ReplaceKernel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "boot.img files (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UnpackingLoader.Visible = true;
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot", true);
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\boot.PARTITION");
                File.Copy(ofd.FileName, AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\boot.PARTITION");
                Thread level2KernelThread = new Thread(delegate ()
                {
                    kernelUnpacker.Unpack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\boot.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot");
                });
                level2KernelThread.Start();
                while (level2KernelThread.IsAlive) Application.DoEvents();
                UnpackingLoader.Visible = false;
                MessageBox.Show("Successfully replaced kernel.", "Success");
            }
        }

        // Recovery
        private void OpenRecovery_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery\\ramdisk");
        }

        private void RecoveryInitRc_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery\\ramdisk\\init.rc");
        }

        private void RecoveryInitAmlogicRc_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery\\ramdisk\\init.amlogic.rc");
        }

        private void DefaultProp_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery\\ramdisk\\default.prop");
        }

        private void ReplaceRecovery_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "recovery.img files (*.img) | *.img";
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                UnpackingLoader.Visible = true;
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery", true);
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION");
                File.Copy(ofd.FileName, AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION");
                Thread level2KernelThread = new Thread(delegate ()
                {
                    kernelUnpacker.Unpack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery");
                });
                level2KernelThread.Start();
                while (level2KernelThread.IsAlive) Application.DoEvents();
                UnpackingLoader.Visible = false;
                MessageBox.Show("Successfully replaced recovery.", "Success");
            }
        }

        private void DirectoryCopy(string source, string destination)
        {
            foreach (string s in Directory.GetDirectories(source))
            {
                if (!Directory.Exists(destination + "\\" + s.Replace(source, ""))) Directory.CreateDirectory(destination + "\\" + s.Replace(source, ""));
                DirectoryCopy(s, destination + "\\" + s.Replace(source, ""));
            }
            foreach (string f in Directory.GetFiles(source))
            {
                if (File.Exists(destination + "\\" + f.Replace(source, ""))) File.Delete(destination + "\\" + f.Replace(source, ""));
                else File.Copy(f, destination + "\\" + f.Replace(source, ""));
            }
        }

        private void ConvertRecovery_Click(object sender, EventArgs e)
        {
            UnpackingLoader.Visible = true;
            ConvertRecovery.Enabled = false;
            StatusLabel.Text = "Unpacking TWRP resources and compiling recovery..";
            Thread PortThread = new Thread(delegate ()
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo(bin + "7za.exe", " x \"" + bin + "Twrp-rd.zip" + "\" -o\"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery\\ramdisk" + "\" -y");
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
                proc.Dispose();
                kernelUnpacker.Repack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION.port");
            });
            PortThread.Start();
            while (PortThread.IsAlive) Application.DoEvents();
           
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION.port"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION");
                File.Move(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION.port", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION");
                MessageBox.Show("Successfully ported TWRP", "Success");
            }
            else
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery", true);
                Thread level2RecoveryThread = new Thread(delegate ()
                {
                    kernelUnpacker.Unpack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level1\\recovery.PARTITION", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery");
                });
                level2RecoveryThread.Start();
                while (level2RecoveryThread.IsAlive) Application.DoEvents();
                MessageBox.Show("Failed to port TWRP to the kernel", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            StatusLabel.Text = "Done.";
            ConvertRecovery.Enabled = true;
            UnpackingLoader.Visible = false;
        }

        // ================================================>> ADVANCED

        private void OpenWorking_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp");
        }

        private void OpenSystem_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system");
        }

        private void OpenBuildProp_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\build.prop");
        }

        private void OpenKeymap_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\usr\\keylayout");
        }

        private void OpenGenericKl_Click(object sender, EventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\usr\\keylayout\\Generic.kl");
        }

        private void AddInitdHook_Click(object sender, EventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\etc\\init.d\\00Preinstall") && new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\bin\\preinstall.sh").ReadToEnd().Contains("/system/etc/init.d"))
            {
                MessageBox.Show("Feature already implemented", "Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\bin\\preinstall.sh");
                writer.Write("#!/system/bin/sh\n\nmount -o remount,rw /system  \n\nchmod 777 /system/etc/init.d\n \nfor script in $(ls /system/etc/init.d)\ndo\n	chmod 775 /system/etc/init.d/$script\n	sh /system/etc/init.d/$script\ndone\n");
                writer.Dispose();
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\etc\\init.d");
                StreamWriter initwriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system\\etc\\init.d\\00Preinstall");
                initwriter.Write("#!/system/bin/sh\n\nFLAG=/data/local/PreinstallMark\nif [ ! -f $FLAG ]; then\n    for app in $(ls /system/preinstall)\n    do\n        chmod 644 /system/preinstall/$app\n        /system/bin/pm install /system/preinstall/$app\n    done\n    touch $FLAG\n    rm -r /system/preinstall\nfi\n\nexit\n");
                initwriter.Dispose();
                MessageBox.Show("Init.d has been enabled(through system hook)", "Feature added");
            }
        }

        // ================================================>> PACKING
        private void CompressionLevel_Scroll(object sender, EventArgs e)
        {
            if (CompressionLevel.Value <= 3) CompressionStatus.Text = CompressionLevel.Value.ToString() + " - larger package size, faster processing time";
            if (CompressionLevel.Value > 3 && CompressionLevel.Value <= 6) CompressionStatus.Text = CompressionLevel.Value.ToString() + " - medium package size, medium processing time";
            if (CompressionLevel.Value >= 7) CompressionStatus.Text = CompressionLevel.Value.ToString() + " - smaller package size, slower processing time";
        }

        private void Compile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.Filter = "flashable zip (*.zip) | *.zip";
            DialogResult res = saver.ShowDialog();
            if (res == DialogResult.OK)
            {
                UnpackingLoader.Visible = true;
                Compile.Enabled = false;
                StatusLabel.Text = "Compiling kernel and recovery..";
                Thread KernelThread = new Thread(delegate ()
                {
                    if (WriteKernel.Checked) kernelUnpacker.Repack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img");
                    if (WriteRecovery.Checked) kernelUnpacker.Repack(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img");
                });
                KernelThread.Start();
                while (KernelThread.IsAlive) Application.DoEvents();
                if (WriteKernel.Checked && !File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img")) MessageBox.Show("Failed to repack the kernel\nPacking will continue without including the kernel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (WriteRecovery.Checked && !File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img")) MessageBox.Show("Failed to repack the recovery\nPacking will continue without including the recovery.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img")) WriteKernel.Checked = false;
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img")) WriteRecovery.Checked = false;

                StatusLabel.Text = "Building installation files..";
                packer.PackToZip(WriteSystem.Checked,WriteKernel.Checked, WriteRecovery.Checked, WipeData.Checked);
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\update-binary..", AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\META-INF\\com\\google\\android\\update-binary");

                StatusLabel.Text = "Creating pre-signed package..";
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\presigned.zip", saver.FileName);
                StatusLabel.Text = "Packing installation files..";
                BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx" +  CompressionLevel.Value + " -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\META-INF" + "\"");
                StatusLabel.Text = "Packing kernel..";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img") && WriteKernel.Checked) BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx" +  CompressionLevel.Value + " -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img" + "\"");
                StatusLabel.Text = "Packing recovery..";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img") && WriteRecovery.Checked) BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx" +  CompressionLevel.Value + " -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img" + "\"");
                StatusLabel.Text = "Packing system..";
                if (WriteSystem.Checked) BackgroundShell(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\7za.exe", " u -mx" +  CompressionLevel.Value + " -tzip -y \"" + saver.FileName + "\" \"" + AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\system" + "\"");

                StatusLabel.Text = "Cleaning up..";
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\META-INF", true);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img")) File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\boot.img");
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img")) File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\tmp\\level2\\recovery.img");

                MessageBox.Show("Packing completed", "Complete");
                StatusLabel.Text = "Done.";
                Compile.Enabled = true;
                UnpackingLoader.Visible = false;
            }
        }

        // ================================================>> ABOUT AND HELP

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("An open source build of AMLogics CustomizationTool built from scratch.\nProject by Ricky Divjakovski.", "About Customization Tool");
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://forum.xda-developers.com/android-stick--console-computers/amlogic/opensource-amlogic-tools-t3786991");
        }
    }
}
