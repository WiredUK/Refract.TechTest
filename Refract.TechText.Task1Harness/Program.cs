using System;
using System.IO;
using System.Linq;
using Refract.TechTest.Task1;

namespace Refract.TechText.Task1Harness
{
    public static class Program
    {
        public static void Main()
        {
            var files = Enumerable
                .Range(1, 2)
                .Select(n => GetStreamForFile(Path.Combine(AppContext.BaseDirectory, "Logfiles", $"file{n}.log")))
                .ToArray();

            var logMerger = new LogMerger();

            using var outputStream = new MemoryStream();

            logMerger.MergeLogFiles(outputStream, files);
        }

        private static Stream GetStreamForFile(string path)
        {
            return new FileStream(path, FileMode.Open);
        }
    }
}
