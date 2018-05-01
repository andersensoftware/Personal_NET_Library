using System;
using System.IO;

namespace MyClassLibraryDotNet.FolderExtentions
{
    class FolderHandler
    {
        // How to copy folders with files in C#, seems it's not a framework function.
        // https://stackoverflow.com/questions/1974019/folder-copy-in-c-sharp
        public static void Copy(string source, string destination, bool recursive)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(source);
            if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);

            FileInfo[] files = sourceDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destination, file.Name);
                file.CopyTo(tempPath, true);
            }

            if (recursive)
            {
                DirectoryInfo[] subDirectories = sourceDirectory.GetDirectories();
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    string tempPath = Path.Combine(destination, subDirectory.Name);
                    Copy(subDirectory.FullName, tempPath, recursive);
                }
            }
        }
        public static void CheckIfNotExistAndCreate(string[] directoriesToCheck)
        {
            for (int i = 0; i < directoriesToCheck.Length; i++)
            {
                if (!Directory.Exists(directoriesToCheck[i]))
                    Directory.CreateDirectory(directoriesToCheck[i]);
            }
        }

        public static void CheckIfNotExistAndCreate(string directoryToCheck)
        {
            if (!Directory.Exists(directoryToCheck))
                Directory.CreateDirectory(directoryToCheck);
        }

        public static string[] MakeDataFolders(string dataPath, string[] folderNames)
        {

            string[] folderPaths = new string[folderNames.Length];

            for (int i = 0; i < folderNames.Length; i++)
            {
                folderPaths[i] = Path.Combine(dataPath, folderNames[i]);
            }

            FolderHandler.CheckIfNotExistAndCreate(folderPaths);

            return folderPaths;
        }
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            // Call cardiolizer man!!

            // It should listen like a service and call Cardiolizer when needed.
            // I'm wandering if should Cardiolizer call the Ambulatorio file mover or make another watchdog to handle it.

            // By now I'm gonna make just a for loop to search for files on the input folder. REALLY don't like it, but the other solution could take much more time 
            // to me to develop it. I'll do it soon.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Watchs for file creation on a folder.
        /// </summary>
        public static void Watch(DirectoryInfo path, string filter)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path.FullName;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = filter;
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }
    }
}
