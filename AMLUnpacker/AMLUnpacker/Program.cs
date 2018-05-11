using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMLUnpacker
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Handled = false;
            Unpacker AmlogicUnpacker = new Unpacker();
            Console.WriteLine("\nAMLUnpacker\nBy Ricky Divjakovski");

            if (args.Length > 1)
            {
                if (args[1].ToLower() == "unpack" && args[2].ToLower() == "-p" && args.Length == 4)
                {
                    if (File.Exists(args[2]))
                    {
                        Console.WriteLine("Unpacking upgrade..");
                        AmlogicUnpacker.UnpackUpgradePackage(args[2], args[3]);
                        Console.WriteLine("Upgrade unpacked.");
                        Handled = true;
                    }
                    else Console.WriteLine("Error: Cannot find file " + args[2]);
                }
                else if (args[1].ToLower() == "unpack" && args[2].ToLower() == "-l" && args.Length == 4)
                {
                    if (File.Exists(args[2]))
                    {
                        Console.WriteLine("Unpacking logo..");
                        AmlogicUnpacker.UnpackLogo(args[2], args[3]);
                        Console.WriteLine("Logo unpacked.");
                        Handled = true;
                    }
                    else Console.WriteLine("Error: Cannot find file " + args[2]);
                }
                else if (args[0] == "verify" && args.Length == 3)
                {
                    if (File.Exists(args[1]))
                    {
                        if (File.Exists(args[2]))
                        {
                            Console.WriteLine("Verifying..");
                            if (AmlogicUnpacker.Verify(args[1], args[2])) Console.WriteLine("SHA1 sum matched, file verified");
                            else Console.WriteLine("SHA1 sum mismatched, file is corrupt");
                            Handled = true;
                        }
                        else Console.WriteLine("Error: Cannot find file " + args[2]);
                    }
                    else Console.WriteLine("Error: Cannot find file " + args[1]);
                }
            }

            if (!Handled)
            {
                Console.WriteLine("AMLUnpacker usage : AMLUnpacker [OPTION] -[WILDCARD]\n" +
                    "\nOPTIONS\n" +
                    "UNPACK  |  Unpacks a file\n" +
                    "VERIFY  |  Compares extracted sha1 to file\n" +
                    "           -AMLUnpacker verify boot.VERIFY boot.IMG\n" +
                    "\nWILDCARDS\n" +
                    "-P  |  Specifies the input file is an upgrade package\n" +
                    "       -AMLUnpacker unpack -p aml_upgrade_package.img outputdir\n" +
                    "-L  |  Specifies the input file is a logo\n" +
                    "       -AMLUnpacker unpack -l logo.img outputdir\n");
            }
        }
    }
}
