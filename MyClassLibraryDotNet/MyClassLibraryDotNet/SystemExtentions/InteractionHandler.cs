using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibraryDotNet.SystemExtentions
{
    public class InteractionHandler
    {
        public static System.Diagnostics.Process LaunchInShell(string workingDirectory, string fileName, string arguments)
        {
            System.Diagnostics.ProcessStartInfo processInfo;
            processInfo = new System.Diagnostics.ProcessStartInfo();
            processInfo.WorkingDirectory = workingDirectory;
            processInfo.FileName = fileName;
            processInfo.Arguments = arguments;
            processInfo.UseShellExecute = true;

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(processInfo);

            /* Code	Meaning
             * 0	No error
             * 1	Warning (Non fatal error(s)). For example, one or more files were locked by some other application, so they were not compressed.
             * 2	Fatal error
             * 7	Command line error
             * 8	Not enough memory for operation
             * 255	User stopped the process
             */
            return process;
        }

        public static void EnvironmentPaths()
        {
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string drive = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%");
            string homePath = Environment.ExpandEnvironmentVariables("%HOMEPATH%");
            string programFilesPath = Environment.ExpandEnvironmentVariables("%PROGRAMFILES%");
        }
    }
}
