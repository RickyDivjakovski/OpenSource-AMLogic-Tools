using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AMLUnpacker
{
    public partial class Unpacker
    {
        // Splitting function
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

        // Convert hex to decimal
        private long ConvertToDecimal(string hexDecimal)
        {
            return (Convert.ToInt64(hexDecimal.ToUpper(), 16));
        }

        // Properly split head
        public void SplitHead(string inputFile, string outputFile)
        {
            FileStream byteReader = new FileStream(inputFile, FileMode.Open);
            FileStream byteWriter = new FileStream(outputFile, FileMode.Create);
            string LineContent = "";
            int CharCount = 0;
            bool copyByte = true;

            while (copyByte)
            {
                byte bit = ((byte)byteReader.ReadByte());
                LineContent = LineContent + (bit).ToString("X2") + " ";
                byteWriter.WriteByte(bit);
                CharCount++;
                if (CharCount == 16)
                {
                    if (LineContent.StartsWith("40 41 4D 4C F0 BF"))
                    {
                        copyByte = false;
                    }
                    LineContent = "";
                    CharCount = 0;
                }
            }
            byteReader.Dispose();
            byteWriter.Dispose();
        }

        // SHA1 Calculation
        static string SHA1(string file)
        {
            Stream SHA1Stream = new FileStream(file, FileMode.Open);
            string sha1 = BitConverter.ToString(System.Security.Cryptography.SHA1.Create().ComputeHash(SHA1Stream)).Replace("-", "");
            SHA1Stream.Dispose();
            return sha1;
        }

        // Hex strint to string
        static string HexToString(string HexString)
        {
            string returnString = "";
            HexString = HexString.Replace(" ", "");
            for (int i = 0; i < HexString.Length / 2; i++)
            {
                string hexChar = HexString.Substring(i * 2, 2);
                int hexValue = Convert.ToInt32(hexChar, 16);
                Regex r = new Regex("[^A-Z0-9.$ ]$");
                if (!Char.ConvertFromUtf32(hexValue).Any(Path.GetInvalidFileNameChars().Contains)) returnString += Char.ConvertFromUtf32(hexValue);
            }
            return returnString;
        }

        // Verification
        public bool Verify(string verifyFile, string partitionFile)
        {
            bool returnVal = true;
            StreamReader reader = new StreamReader(verifyFile);
            if (reader.ReadToEnd().Split().Last() != SHA1(partitionFile).ToLower()) returnVal = false;
            reader.Dispose();
            return returnVal;
        }

        // Unpack logo
        public void UnpackLogo(string inputFile, string outputFolder)
        {
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            HexSplit(inputFile, outputFolder + "\\head.BIN", "00000000", "000004D0");

            string LineContent = "";

            string FileName = "";
            string FileExtension = ".png";
            string StartAddress = "";
            string EndAddress = "";

            int CurrentByte = 0;
            int CharCount = 0;

            FileStream hexReader = new FileStream(outputFolder + "\\head.BIN", FileMode.Open);

            while (CurrentByte <= hexReader.Length)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;

                if (CharCount == 16)
                {
                    if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 6C 6F 67 6F")) FileName = "upgrade_logo";
                    else if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 65 72 72 6F 72")) FileName = "upgrade_error";
                    else if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 75 6E 66 6F 63 75 73")) FileName = "upgrade_unfocus";
                    else if (LineContent.StartsWith("62 6F 6F 74 75 70")) FileName = "bootup";
                    else if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 62 61 72")) FileName = "upgrade_bar";
                    else if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 73 75 63 63 65 73 73")) FileName = "upgrade_success";
                    else if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 75 70 67 72 61 64 69")) FileName = "upgrade_upgrading";
                    else if (LineContent.StartsWith("75 70 67 72 61 64 65 5F 66 61 69 6C")) FileName = "upgrade_fail";
                    else if (LineContent.StartsWith("56 19 05 27"))
                    {
                        StartAddress = LineContent.Split()[15] + LineContent.Split()[14] + LineContent.Split()[13] + LineContent.Split()[12];
                        string FileSize = LineContent.Split()[11] + LineContent.Split()[10] + LineContent.Split()[9] + LineContent.Split()[8];
                        EndAddress = ((Convert.ToInt64(StartAddress.ToUpper(), 16) + (Convert.ToInt64(FileSize.ToUpper(), 16))) - 1).ToString("X");
                    }

                    if (StartAddress != "" && EndAddress != "" && FileName != "")
                    {
                        HexSplit(inputFile, outputFolder + "\\" + FileName + FileExtension, StartAddress, EndAddress);
                        StartAddress = "";
                        EndAddress = "";
                        FileName = "";
                    }
                    LineContent = "";
                    CharCount = 0;
                }
            }
            hexReader.Dispose();
            File.Delete(outputFolder + "\\head.BIN");
        }

        // Generate partition info
        public void GeneratePartitionInfo(string inputFile, string outputFolder)
        {
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            HexSplit(inputFile, outputFolder + "\\head.BIN", "00000000", "000028C0");

            string LineContent = "";
            string PreviousLineContent = "";
            string FileName = "";
            string FileSize = "";
            string FileExtension = "";
            string StartAddress = "";
            string EndAddress = "";
            string shaSum = "";
            string TotalFile = "";

            int CurrentByte = 0;
            int CharCount = 0;
            bool SkipNull = false;

            FileStream hexReader = new FileStream(outputFolder + "\\head.BIN", FileMode.Open);

            while (CurrentByte <= hexReader.Length)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;

                if (CharCount == 16)
                {
                    if (SkipNull)
                    {
                        if (!string.IsNullOrWhiteSpace(HexToString(LineContent).Split().First()))
                        {
                            FileName = HexToString(LineContent).Split().First();
                            SkipNull = false;
                        }
                    }
                    else
                    {
                        if (LineContent.StartsWith("55 53 42")) FileExtension = ".USB";
                        else if (LineContent.StartsWith("50 41 52 54 49 54 49 4F 4E")) FileExtension = ".PARTITION";
                        else if (LineContent.StartsWith("56 45 52 49 46 59")) FileExtension = ".VERIFY";
                        else if (LineContent.StartsWith("64 74 62")) FileExtension = ".dtb";
                        else if (LineContent.StartsWith("55 42 4F 4F 54") && string.IsNullOrWhiteSpace(FileName)) FileExtension = ".UBOOT";
                        else if (LineContent.StartsWith("69 6E 69")) FileExtension = ".ini";
                        else if (LineContent.StartsWith("63 6F 6E 66")) FileExtension = ".conf";

                        if (FileExtension != "")
                        {
                            if (PreviousLineContent.Split()[3] == "0") StartAddress = PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];
                            else StartAddress = PreviousLineContent.Split()[3] + PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];

                            if (PreviousLineContent.Split()[11] == "0") FileSize = PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];
                            else FileSize = PreviousLineContent.Split()[11] + PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];

                            EndAddress = ((Convert.ToInt64(StartAddress.ToUpper(), 16) + (Convert.ToInt64(FileSize.ToUpper(), 16))) - 1).ToString("X");
                            SkipNull = true;
                        }
                    }

                    PreviousLineContent = LineContent;
                    LineContent = "";
                    CharCount = 0;
                }

                if (FileName != "" && FileExtension != "" && StartAddress != null && EndAddress != null && FileSize != null)
                {
                    if (FileExtension == ".VERIFY")
                    {
                        HexSplit(inputFile, Path.GetTempPath() + "\\" + FileName + ".VERIFY", StartAddress, EndAddress);
                        StreamReader reader = new StreamReader(Path.GetTempPath() + "\\" + FileName + ".VERIFY");
                        shaSum = reader.ReadToEnd().Split().Last();
                        reader.Dispose();
                        File.Delete(Path.GetTempPath() + "\\" + FileName + ".VERIFY");
                        TotalFile = TotalFile + "SHA1: " + shaSum + "\n";
                    }
                    else
                    {
                        TotalFile = TotalFile + "\n" + FileName + FileExtension + "\nStart address: " + StartAddress + "\nEnd address: " + EndAddress + "\nFile size: " + ConvertToDecimal(FileSize).ToString() + "\n";
                        FileExtension = "";
                        StartAddress = "";
                        FileSize = "";
                        EndAddress = "";
                        FileName = "";
                    }
                }
            }

            hexReader.Dispose();
            File.Delete(outputFolder + "\\head.BIN");
            File.WriteAllText(outputFolder + "\\partition_info.txt", TotalFile);
        }

        // Unpack ugrade package
        public void UnpackUpgradePackage(string inputFile, string outputFolder)
        {
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            SplitHead(inputFile, outputFolder + "\\head.BIN");

            string LineContent = "";
            string PreviousLineContent = "";
            string FileName = "";
            string FileSize = "";
            string FileExtension = "";
            string StartAddress = "";
            string EndAddress = "";

            int CurrentByte = 0;
            int CharCount = 0;
            bool SkipNull = false;

            FileStream hexReader = new FileStream(outputFolder + "\\head.BIN", FileMode.Open);

            while (CurrentByte < hexReader.Length)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;
                if (CharCount == 16)
                {
                    if (SkipNull)
                    {
                        if (!string.IsNullOrWhiteSpace(HexToString(LineContent).Split().First()))
                        {
                            FileName = HexToString(LineContent).Split().First();
                            SkipNull = false;
                        }
                    }
                    else
                    {
                        if (LineContent.StartsWith("55 53 42")) FileExtension = ".USB";
                        else if (LineContent.StartsWith("50 41 52 54 49 54 49 4F 4E")) FileExtension = ".PARTITION";
                        else if (LineContent.StartsWith("56 45 52 49 46 59")) FileExtension = ".VERIFY";
                        else if (LineContent.StartsWith("64 74 62")) FileExtension = ".dtb";
                        else if (LineContent.StartsWith("55 42 4F 4F 54") && string.IsNullOrWhiteSpace(FileName)) FileExtension = ".UBOOT";
                        else if (LineContent.StartsWith("69 6E 69")) FileExtension = ".ini";
                        else if (LineContent.StartsWith("63 6F 6E 66")) FileExtension = ".conf";

                        if (FileExtension != "")
                        {
                            if (PreviousLineContent.Split()[3] == "0") StartAddress = PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];
                            else StartAddress = PreviousLineContent.Split()[3] + PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];

                            if (PreviousLineContent.Split()[11] == "0") FileSize = PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];
                            else FileSize = PreviousLineContent.Split()[11] + PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];

                            EndAddress = ((Convert.ToInt64(StartAddress.ToUpper(), 16) + (Convert.ToInt64(FileSize.ToUpper(), 16))) - 1).ToString("X");
                            SkipNull = true;
                        }
                    }

                    PreviousLineContent = LineContent;
                    LineContent = "";
                    CharCount = 0;
                }

                if (FileName != "" && FileExtension != "" && StartAddress != null && EndAddress != null && FileSize != null)
                {
                    HexSplit(inputFile, outputFolder + "\\" + FileName + FileExtension, StartAddress, EndAddress);
                    FileExtension = "";
                    StartAddress = "";
                    FileSize = "";
                    EndAddress = "";
                    FileName = "";
                }
            }

            hexReader.Dispose();
            File.Delete(outputFolder + "\\head.BIN");
        }

        // Return all partitions that can be extracted
        public string GetUpgradeContent(string inputFile)
        {
            HexSplit(inputFile, Path.GetTempPath() + "\\head.BIN", "00000000", "000028C0");

            string LineContent = "";
            string PreviousLineContent = "";
            string FileName = "";
            string FileExtension = "";
            string partitions = "";

            int CurrentByte = 0;
            int CharCount = 0;
            bool SkipNull = false;

            FileStream hexReader = new FileStream(Path.GetTempPath() + "\\head.BIN", FileMode.Open);

            while (CurrentByte <= hexReader.Length)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;

                if (CharCount == 16)
                {
                    if (SkipNull)
                    {
                        if (!string.IsNullOrWhiteSpace(HexToString(LineContent).Split().First()))
                        {
                            FileName = HexToString(LineContent).Split().First();
                            SkipNull = false;
                        }
                    }
                    else
                    {
                        if (LineContent.StartsWith("55 53 42")) FileExtension = ".USB";
                        else if (LineContent.StartsWith("50 41 52 54 49 54 49 4F 4E")) FileExtension = ".PARTITION";
                        else if (LineContent.StartsWith("56 45 52 49 46 59")) FileExtension = ".VERIFY";
                        else if (LineContent.StartsWith("64 74 62")) FileExtension = ".dtb";
                        else if (LineContent.StartsWith("55 42 4F 4F 54") && string.IsNullOrWhiteSpace(FileName)) FileExtension = ".UBOOT";
                        else if (LineContent.StartsWith("69 6E 69")) FileExtension = ".ini";
                        else if (LineContent.StartsWith("63 6F 6E 66")) FileExtension = ".conf";

                        if (FileExtension != "")
                        {
                            SkipNull = true;
                        }
                    }

                    PreviousLineContent = LineContent;
                    LineContent = "";
                    CharCount = 0;
                }

                if (FileName != "" && FileExtension != "")
                {
                    partitions = partitions + FileName + FileExtension + "\n";
                    FileExtension = "";
                    FileName = "";
                }
            }
            hexReader.Dispose();
            File.Delete(Path.GetTempPath() + "\\head.BIN");
            return partitions;
        }

        // Upgrade info type enum
        public enum UpgradeInfoType
        {
            FileSize
        }

        // Query info on upgrade package
        public string UpgradeInfo(string inputFile, UpgradeInfoType upgradeInfoType)
        {
            HexSplit(inputFile, Path.GetTempPath() + "\\head.BIN", "00000000", "000028C0");

            string LineContent = "";
            string returnInfo = "";

            int CurrentByte = 0;
            int CharCount = 0;
            int line = 1;
            bool finished = false;

            FileStream hexReader = new FileStream(Path.GetTempPath() + "\\head.BIN", FileMode.Open);

            while (CurrentByte <= hexReader.Length && !finished)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;

                if (CharCount == 16)
                {
                    if (line == 1 && upgradeInfoType == UpgradeInfoType.FileSize)
                    {
                        returnInfo = ConvertToDecimal(LineContent.Split()[15] + LineContent.Split()[14] + LineContent.Split()[13] + LineContent.Split()[12]).ToString();
                        finished = true;
                    }
                    line++;
                }
            }
            hexReader.Dispose();
            File.Delete(Path.GetTempPath() + "\\head.BIN");
            return returnInfo;
        }

        // Partition info type enum
        public enum PartitionInfoType
        {
            Filename, FileExtension, FileSize, StartAddress, EndAddress, PackageSize
        }

        // Query info on available partitions
        public string PartitionInfo(string inputFile, string partition, PartitionInfoType partitionInfoType)
        {
            HexSplit(inputFile, Path.GetTempPath() + "\\head.BIN", "00000000", "000028C0");

            string LineContent = "";
            string PreviousLineContent = "";
            string FileName = "";
            string FileSize = "";
            string FileExtension = "";
            string StartAddress = "";
            string EndAddress = "";
            string returnInfo = "";

            int CurrentByte = 0;
            int CharCount = 0;
            bool SkipNull = false;

            FileStream hexReader = new FileStream(Path.GetTempPath() + "\\head.BIN", FileMode.Open);

            while (CurrentByte <= hexReader.Length)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;

                if (CharCount == 16)
                {
                    if (SkipNull)
                    {
                        if (!string.IsNullOrWhiteSpace(HexToString(LineContent).Split().First()))
                        {
                            FileName = HexToString(LineContent).Split().First();
                            SkipNull = false;
                        }
                    }
                    else
                    {
                        if (LineContent.StartsWith("55 53 42")) FileExtension = ".USB";
                        else if (LineContent.StartsWith("50 41 52 54 49 54 49 4F 4E")) FileExtension = ".PARTITION";
                        else if (LineContent.StartsWith("56 45 52 49 46 59")) FileExtension = ".VERIFY";
                        else if (LineContent.StartsWith("64 74 62")) FileExtension = ".dtb";
                        else if (LineContent.StartsWith("55 42 4F 4F 54") && string.IsNullOrWhiteSpace(FileName)) FileExtension = ".UBOOT";
                        else if (LineContent.StartsWith("69 6E 69")) FileExtension = ".ini";
                        else if (LineContent.StartsWith("63 6F 6E 66")) FileExtension = ".conf";

                        if (FileExtension != "")
                        {
                            if (PreviousLineContent.Split()[3] == "0") StartAddress = PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];
                            else StartAddress = PreviousLineContent.Split()[3] + PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];

                            if (PreviousLineContent.Split()[11] == "0") FileSize = PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];
                            else FileSize = PreviousLineContent.Split()[11] + PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];

                            EndAddress = ((Convert.ToInt64(StartAddress.ToUpper(), 16) + (Convert.ToInt64(FileSize.ToUpper(), 16))) - 1).ToString("X");
                            SkipNull = true;
                        }
                    }

                    PreviousLineContent = LineContent;
                    LineContent = "";
                    CharCount = 0;
                }

                if (FileName != "" && FileExtension != "" && StartAddress != null && EndAddress != null && FileSize != null && FileName.ToLower() + FileExtension.ToLower() == partition.ToLower())
                {
                    if (partitionInfoType == PartitionInfoType.Filename) returnInfo = FileName;
                    if (partitionInfoType == PartitionInfoType.FileExtension) returnInfo = FileExtension;
                    if (partitionInfoType == PartitionInfoType.FileSize) returnInfo = ConvertToDecimal(FileSize).ToString();
                    if (partitionInfoType == PartitionInfoType.StartAddress) returnInfo = StartAddress;
                    if (partitionInfoType == PartitionInfoType.EndAddress) returnInfo = EndAddress;
                    FileExtension = "";
                    StartAddress = "";
                    FileSize = "";
                    EndAddress = "";
                    FileName = "";
                }
            }
            hexReader.Dispose();
            File.Delete(Path.GetTempPath() + "\\head.BIN");
            return returnInfo;
        }

        // Unpack specific partition
        public void UnpackPartition(string inputFile, string partitionName, string outputFolder)
        {
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
            SplitHead(inputFile, outputFolder + "\\head.BIN");

            string LineContent = "";
            string PreviousLineContent = "";
            string FileName = "";
            string FileSize = "";
            string FileExtension = "";
            string StartAddress = "";
            string EndAddress = "";
            string TotalFile = "";

            int CurrentByte = 0;
            int CharCount = 0;
            bool SkipNull = false;

            FileStream hexReader = new FileStream(outputFolder + "\\head.BIN", FileMode.Open);

            while (CurrentByte <= hexReader.Length)
            {
                LineContent = LineContent + ((byte)hexReader.ReadByte()).ToString("X2") + " ";
                CharCount++;
                CurrentByte++;

                if (CharCount == 16)
                {
                    if (SkipNull)
                    {
                        if (!string.IsNullOrWhiteSpace(HexToString(LineContent).Split().First()))
                        {
                            FileName = HexToString(LineContent).Split().First();
                            SkipNull = false;
                        }
                    }
                    else
                    {
                        if (LineContent.StartsWith("55 53 42")) FileExtension = ".USB";
                        else if (LineContent.StartsWith("50 41 52 54 49 54 49 4F 4E")) FileExtension = ".PARTITION";
                        else if (LineContent.StartsWith("56 45 52 49 46 59")) FileExtension = ".VERIFY";
                        else if (LineContent.StartsWith("64 74 62")) FileExtension = ".dtb";
                        else if (LineContent.StartsWith("55 42 4F 4F 54") && string.IsNullOrWhiteSpace(FileName)) FileExtension = ".UBOOT";
                        else if (LineContent.StartsWith("69 6E 69")) FileExtension = ".ini";
                        else if (LineContent.StartsWith("63 6F 6E 66")) FileExtension = ".conf";

                        if (FileExtension != "")
                        {
                            if (PreviousLineContent.Split()[3] == "0") StartAddress = PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];
                            else StartAddress = PreviousLineContent.Split()[3] + PreviousLineContent.Split()[2] + PreviousLineContent.Split()[1] + PreviousLineContent.Split()[0];

                            if (PreviousLineContent.Split()[11] == "0") FileSize = PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];
                            else FileSize = PreviousLineContent.Split()[11] + PreviousLineContent.Split()[10] + PreviousLineContent.Split()[9] + PreviousLineContent.Split()[8];

                            EndAddress = ((Convert.ToInt64(StartAddress.ToUpper(), 16) + (Convert.ToInt64(FileSize.ToUpper(), 16))) - 1).ToString("X");
                            SkipNull = true;
                        }
                    }

                    PreviousLineContent = LineContent;
                    LineContent = "";
                    CharCount = 0;
                }

                if (FileName != "" && FileExtension != "" && StartAddress != null && EndAddress != null && FileSize != null && FileName.ToLower() + FileExtension.ToLower() == partitionName.ToLower())
                {
                    HexSplit(inputFile, outputFolder + "\\" + FileName + FileExtension, StartAddress, EndAddress);
                    FileExtension = "";
                    StartAddress = "";
                    FileSize = "";
                    EndAddress = "";
                    FileName = "";
                }
            }

            hexReader.Dispose();
            File.Delete(outputFolder + "\\head.BIN");
        }
    }
}
