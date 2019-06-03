using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using das.Extensions.Logger;
using OKUD2XML.Extensions;

namespace OKUD2XML.Core
{
    public static class App
    {
        public static string Name = Assembly.GetExecutingAssembly().GetName().Name;
        public static string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(); 

        public static class Paths
        {
            public static string ROOT = AppDomain.CurrentDomain.BaseDirectory;
            public static string SOURCE = Directory.CreateDirectory(Path.Combine(ROOT, "SOURCE")).FullName;
            public static string RESULT = Directory.CreateDirectory(Path.Combine(ROOT, "RESULT")).FullName;

            public static string ResultFile(string fileName)
            {
                return Path.Combine(RESULT, fileName);
            }

            public static string[] GetFilesFromSource(string searchPattern)
            {
                return GetFiles(SOURCE, searchPattern);
            }

            public static string[] GetFiles(string path, string searchPattern)
            {
                string[] searchPatterns = searchPattern.Split('|');
                List<string> files = new List<string>();

                foreach (string sp in searchPatterns)
                    files.AddRange(Directory.GetFiles(path, sp).Where(f => !Path.GetFileName(f).StartsWith("~")));

                files.Sort();
                return files.ToArray();
            }
        }

        public static ILogger Log = LoggerFactory.CreateFactory(LoggerProvider.Empty.UseDefaultWriters()).CreateLogger();
    }
}
