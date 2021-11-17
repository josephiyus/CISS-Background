using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CISS_Background.id.co.cdp.util
{
    static class FileUtil
    {
        public static void generateFile(string destFile, string values)
        {
            File.WriteAllBytes(destFile, System.Text.Encoding.UTF8.GetBytes(values));
        }

        public static bool isFolder(string folder)
        {
            bool result = true;
            if (!Directory.Exists(folder)) 
            {
                Directory.CreateDirectory(folder);
                result = File.Exists(folder);
            }
            return result;
        }

        public static void appendTextFile(string fileName, string value)
        {
            File.AppendAllText(fileName, value + System.Environment.NewLine);
        }
    }
}
