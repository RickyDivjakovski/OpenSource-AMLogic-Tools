using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AMLUnpacker
{
    public class KernelUnpacker
    {
        public void Unpack(string inputIMG, string outputDIR)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;

            if (Directory.Exists(outputDIR)) Directory.Delete(outputDIR, true);
            Directory.CreateDirectory(outputDIR);
            startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\unpackbootimg.exe\" -i \"" + inputIMG + "\" -o \"" + outputDIR + "\"\"";
            processTemp.Start();
            processTemp.WaitForExit();

            Directory.CreateDirectory(outputDIR + "\\ramdisk");
            Directory.SetCurrentDirectory(outputDIR + "\\ramdisk");

            foreach (string formatFile in Directory.GetFiles(outputDIR))
            {
                if (formatFile.Contains("-ramdisk."))
                {
                    if (formatFile.EndsWith(".gz")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\gzip.exe\" -dcv \"" + outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk.gz\" | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\cpio.exe\" -i\"";
                    else if (formatFile.EndsWith(".lzma")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\xz.exe\" -dcv \"" + outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk.lzma\" | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\cpio.exe\" -i\"";
                    else if (formatFile.EndsWith(".xz")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\xz.exe\" -dcv \"" + outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk.xz\" | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\cpio.exe\" -i\"";
                    else if (formatFile.EndsWith(".bz2")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\bzip2.exe\" -dcv \"" + outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk.bz2\" | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\cpio.exe\" -i\"";
                    else if (formatFile.EndsWith(".lz4")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\lz4.exe\" -dv \"" + outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk.lz4\" stdout | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\cpio.exe\" -i\"";
                    else if (formatFile.EndsWith(".lzo")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\lzop.exe\" -dcv \"" + outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk.lzo\" | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\cpio.exe\" -i\"";
                    StreamWriter writer = new StreamWriter(outputDIR + "\\" + Path.GetFileName(inputIMG) + "-ramdisk-compress");
                    writer.Write(formatFile.Split('.').Last() + "\n");
                    writer.Dispose();
                    try
                    {
                        processTemp.Start();
                        processTemp.WaitForExit();
                    }
                    catch { throw; }
                    File.Delete(formatFile);
                }
            }
            processTemp.Dispose();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        public void Repack(string inputDIR, string outputIMG)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;

            string originalFile = "";
            foreach (string file in Directory.GetFiles(inputDIR)) if (Path.GetFileName(file).Contains("-ramdisk")) originalFile = Path.GetFileName(file).Split(new string[] { "-ramdisk" }, StringSplitOptions.None).First();

            Directory.SetCurrentDirectory(inputDIR);
            string compression = readfile(inputDIR + "\\" + originalFile + "-ramdisk-compress");
            if (compression == "gz") startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootfs.exe\" ramdisk | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\minigzip.exe\" -c -9 > " + originalFile + "-ramdisk." + compression + "\"";
            if (compression == "xz") startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootfs.exe\" ramdisk | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\xz.exe\" -1zv -Ccrc32 > " + originalFile + "-ramdisk." + compression + "\"";
            if (compression == "lzma") startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootfs.exe\" ramdisk | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\xz.exe\" --format=lzma -1zv > " + originalFile + "-ramdisk." + compression + "\"";
            if (compression == "bz2") startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootfs.exe\" ramdisk | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\bzip2.exe\" -kv > " + originalFile + "-ramdisk." + compression + "\"";
            if (compression == "lz4") startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootfs.exe\" ramdisk | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\lz4.exe\" -l stdin stdout > " + originalFile + "-ramdisk." + compression + "\"";
            if (compression == "lzo") startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootfs.exe\" ramdisk | \"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\lzop.exe\" -v > " + originalFile + "-ramdisk." + compression + "\"";
            try
            {
                processTemp.Start();
                processTemp.WaitForExit();
            }
            catch { throw; }

            string kernel = originalFile + "-kernel";
            string ramdisk = originalFile + "-ramdisk." + compression;
            string board = "--board \"\"";
            string kernelbase = "";
            string pagesize = "";
            string cmdline = "";
            string kernel_offset = "";
            string ramdisk_offset = "";
            string second_offset = "";
            string secondkernel = "";
            string tagoffset = "";
            string dtb = "";
            if (File.Exists(inputDIR + "\\" + originalFile + "-board")) board = "--board \"" + readfile(inputDIR + "\\" + originalFile + "-board\"");
            if (File.Exists(inputDIR + "\\" + originalFile + "-base")) kernelbase = "--base " + readfile(inputDIR + "\\" + originalFile + "-base");
            if (File.Exists(inputDIR + "\\" + originalFile + "-pagesize")) pagesize = readfile(inputDIR + "\\" + originalFile + "-pagesize");
            if (File.Exists(inputDIR + "\\" + originalFile + "-cmdline")) cmdline = "--cmdline \"" + readfile(inputDIR + "\\" + originalFile + "-cmdline") + "\"";
            if (File.Exists(inputDIR + "\\" + originalFile + "-kernel_offset")) kernel_offset = "--kernel_offset " + readfile(inputDIR + "\\" + originalFile + "-kernel_offset");
            if (File.Exists(inputDIR + "\\" + originalFile + "-ramdisk_offset")) ramdisk_offset = "--ramdisk_offset " + readfile(inputDIR + "\\" + originalFile + "-ramdisk_offset");
            if (File.Exists(inputDIR + "\\" + originalFile + "-second_offset"))
            {
                if (File.Exists(inputDIR + "\\" + originalFile + "-second"))
                {
                    second_offset = readfile(inputDIR + "\\" + originalFile + "-second_offset");
                    secondkernel = "second=--second " + inputDIR + "\\" + originalFile + "-second";
                }
            }
            else
            {
                if (File.Exists(inputDIR + "\\" + originalFile + "-tags_offset")) tagoffset = readfile(inputDIR + "\\" + originalFile + "-tags_offset");
                else if (File.Exists(inputDIR + "\\" + originalFile + "-dt")) dtb = "dtb=--dt " + inputDIR + "\\" + originalFile + "-dt";
            }
            if (File.Exists(inputDIR + "\\" + originalFile + "-mtk")) startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootimg.exe\" --kernel " + kernel + " --ramdisk " + ramdisk + " --pagesize " + pagesize + " " + kernelbase + " " + board + " " + kernel_offset + " " + ramdisk_offset + " --tags_offset " + tagoffset + " " + secondkernel + " " + cmdline + " " + second_offset + " " + dtb + " --mtk 1 -o \"" + outputIMG + "\"\"";
            else startInfo.Arguments = "/C \"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\bin\\mkbootimg.exe\" --kernel " + kernel + " --ramdisk " + ramdisk + " --pagesize " + pagesize + " " + kernelbase + " " + board + " " + kernel_offset + " " + ramdisk_offset + " --tags_offset " + tagoffset + " " + secondkernel + " " + cmdline + " " + second_offset + " " + dtb + " -o \"" + outputIMG + "\"\"";
            try
            {
                processTemp.Start();
                processTemp.WaitForExit();
            }
            catch { throw; }

            processTemp.Dispose();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        string readfile(string file)
        {
            StreamReader fileReader = new StreamReader(file);
            string output = fileReader.ReadToEnd().Split('\n').First().Split('\r').First();
            fileReader.Close();
            fileReader.Dispose();
            return output;
        }
    }
}
