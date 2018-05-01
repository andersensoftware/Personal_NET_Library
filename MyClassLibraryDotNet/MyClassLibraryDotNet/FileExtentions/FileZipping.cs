using MyClassLibraryDotNet.SystemExtentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyClassLibraryDotNet.FileExtentions
{
    class FileZipping
    {
        // How to compress on 7z command line with C#
        // https://stackoverflow.com/questions/16052877/how-to-unzip-all-zip-file-from-folder-using-c-sharp-4-0-and-without-using-any-o

        // 7z command line examples
        // http://www.dotnetperls.com/7-zip-examples

        // If u don't put specific output folder, VS let the file on:
        // C:\[USER]\AppData\Local\VirtualStore\Program Files (x86)\7-Zip\[OUTPUTFILE]
        public static void MakeZip(string inFolder, string outFile)
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string zipCommand = "a -tzip \"{0}\" \"{1}\\*\"";
            string zipCompressorFolder = "7-Zip";
            string zipCompressorFileName = "7z.exe";
            string zipError = "Unable to compress file: {0}";

            FileHandler.CheckFileVersion(ref outFile);

            string zipCompressor = Path.Combine(appPath, zipCompressorFolder);
            string arguments = string.Format(zipCommand, outFile, inFolder);

            System.Diagnostics.Process zipCompressorProcess = InteractionHandler.LaunchInShell(zipCompressor, zipCompressorFileName, arguments);

            while (!zipCompressorProcess.HasExited) { }
            if (zipCompressorProcess.ExitCode == 0)
            {
                // Directory.Delete(inFolder);
            }
            else
            {
                throw new Exception(string.Format(zipError, outFile));
            }
        }
        public static void UnZipOnFolder(FileInfo inFile)
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string zipCommand = "e \"{0}\" -o\"{1}\" * -r";
            string zipCompressorFolder = "7-Zip";
            string zipCompressorFileName = "7z.exe";
            string zipError = "Unable to compress file: {0}";

            string zipCompressor = Path.Combine(appPath, zipCompressorFolder);
            string arguments = string.Format(zipCommand, inFile.FullName, inFile.Directory + "\\" + inFile.Name.Split('.')[0] + "\\");

            Console.WriteLine(arguments);

            try
            {
                System.Diagnostics.Process zipCompressorProcess = InteractionHandler.LaunchInShell(zipCompressor, zipCompressorFileName, arguments);

                while (!zipCompressorProcess.HasExited) { }
                if (zipCompressorProcess.ExitCode == 0)
                {
                    // Directory.Delete(inFolder);
                }
                else
                {
                    throw new Exception(string.Format(zipError, inFile.Name));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
