using System;
using System.Collections.Generic;
using System.IO;

namespace NetExtentions
{
    public static class FileInfoExtentions
    {
        public static string ReadAllText(this FileInfo aSource)
        {
            return String.Join(Environment.NewLine, aSource.ReadAllLines());
        }

        public static IList<string> ReadAllLines(this FileInfo aSource)
        {
            aSource.Refresh();
            //Always start with a fresh list
            var textList = new List<string>();
            using (var fs = new FileStream(aSource.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        textList.Add(sr.ReadLine());
                    }
                }
            }
            return textList.ToArray();
        }

        public static void WriteAllLines(this FileInfo aSource, IList<string> lineList)
        {
            WriteAllText(aSource, String.Join(Environment.NewLine, lineList));
        }

        public static void WriteAllText(this FileInfo aSource, string text)
        {
            aSource.Refresh();
            File.WriteAllText(aSource.FullName, text);
            aSource.Refresh();
        }

        public static void Touch(this FileInfo aFile)
        {
            aFile.Refresh();
            if (!aFile.Exists && aFile.DirectoryName != null)
            {
                Directory.CreateDirectory(aFile.DirectoryName);
                File.Create(aFile.FullName);
            }
            else
            {
                aFile.LastAccessTime = DateTime.UtcNow;
            }
            aFile.Refresh();
        }
    }
}
