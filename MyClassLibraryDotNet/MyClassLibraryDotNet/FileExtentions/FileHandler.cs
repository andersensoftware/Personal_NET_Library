using MyClassLibraryDotNet.SystemExtentions;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace MyClassLibraryDotNet.FileExtentions
{
    class FileHandler
    {
        public static void CheckFileVersion(ref string outFile)
        {
            int outFileVersion = 0;

            while (File.Exists(outFile))
            {
                outFileVersion++;
                outFile = string.Format(outFile, "_" + outFileVersion.ToString());
            }
        }


        private void InsertWaterMarkToDocFile(FileInfo file)
        {
            // 2 - Convert .doc to odt.
            // soffice --headless --convert-to odt --outdir documents/ *.doc <- Useless!!! Generates an XML Only file.
            // soffice --headless --convert-to writer8 --outdir documents/ *.doc <- Use this one, take care about fucking .writer8 extension...
            Console.WriteLine("Calling LibreOffice...");
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            InteractionHandler.LaunchInShell(Settings.Default.UNC, @"C:\Program Files (x86)\LibreOffice 4\program\soffice.exe", "--headless --convert-to writer8 --outdir \"" + documentsFolder + "\" " + file.FullName);
            Console.WriteLine("File: " + file.FullName + " converted.");
            Console.WriteLine("Saved on: " + documentsFolder);

            FileInfo writer8File = new FileInfo(Path.Combine(documentsFolder, file.Name.Split('.')[0] + ".writer8"));
            Console.WriteLine(writer8File.FullName);

            // 3 - Decompress report.
            // We have to wait cause if it begins before file has copied, it throws an ugly error.
            while (!File.Exists(writer8File.FullName)) { Thread.Sleep(200); };
            FileZipping.UnZipOnFolder(writer8File);

            // 4 - Add values and content.


            // 5 - Compress report on odt file.


        }

        public bool IsFileLocked(string file)
        {
            const long ERROR_SHARING_VIOLATION = 0x20;
            const long ERROR_LOCK_VIOLATION = 0x21;

            //check that problem is not in destination file
            if (File.Exists(file) == true)
            {
                FileStream stream = null;
                try
                {
                    stream = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (Exception ex2)
                {
                    //_log.WriteLog(ex2, "Error in checking whether file is locked " + file);
                    int errorCode = Marshal.GetHRForException(ex2) & ((1 << 16) - 1);
                    if ((ex2 is IOException) && (errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION))
                    {
                        return true;
                    }
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }
            return false;
        }      
            /// <summary>
            /// Formats path with "Ambulatorio" crap Report sorting way.
            /// Default path: \\UNC\xx-xxx\xxxxxxxx.doc
            /// </summary>
            /// <param name="fileName">Name of report to format path.</param>
            /// <returns></returns>
            public static string FilenameToPath(string UNC, string fileName)
            {
                string splittedFileName = fileName.Split('.')[0];
                string reportFolder = string.Format("{0}-{1}", splittedFileName.Substring(0, 2), splittedFileName.Substring(2, 3));
                return Path.Combine(UNC, reportFolder, fileName);
            }

            /// <summary>
            /// Formats path with "Ambulatorio" crap Report sorting way.
            /// Default path: \\UNC\xx-xxx\xxxxxxxx.doc
            /// </summary>
            /// <param name="accessionNumber">DICOM Accession number ID. It has to be a number cause its how Ambulatorio stores it.</param>
            /// <param name="desiredExtension">Filename extension, or NULL for ".doc".</param>
            /// <returns></returns>
            public static string AccessionNumberToPath(string UNC, int accessionNumber, string desiredExtension)
            {
                string extension = desiredExtension ?? ".doc";
                string _accessionNumber = accessionNumber.ToString();
                string reportFolder = string.Format("{0}-{1}", _accessionNumber.Substring(0, 2), _accessionNumber.Substring(2, 3));
                return Path.Combine(UNC, reportFolder, _accessionNumber + extension);
            }

        }
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {

        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\172.100.1.201\\Ambulatorio$")]
        public string UNC
        {
            get
            {
                return ((string)(this["UNC"]));
            }
            set
            {
                this["UNC"] = value;
            }
        }

    }
}

