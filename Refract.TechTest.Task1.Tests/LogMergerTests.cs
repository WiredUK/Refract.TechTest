using System;
using System.IO;
using Xunit;

namespace Refract.TechTest.Task1.Tests
{
    public class LogMergerTests
    {
        [Fact]
        public void Can_merge_files_files_with_alternating_logs()
        {
            // Arrange
            var merger = new LogMerger();

            using var output = new MemoryStream();

            var log1 = GetStreamFromLogs(
                "2010-01-01 00:00:01.000 Item1", 
                "2010-01-01 00:00:03.000 Item3");

            var log2 = GetStreamFromLogs(
                "2010-01-01 00:00:02.000 Item2", 
                "2010-01-01 00:00:04.000 Item4");

            // Act
            merger.MergeLogFiles(output, log1, log2);

            var reader = new StreamReader(output);
            output.Position = 0;

            // Assert
            for (var n = 1; n <= 4; n++)
            {
                var logEntry = reader.ReadLine();
                Assert.Equal($"2010-01-01 00:00:0{n}.000 Item{n}", logEntry);
            }
        }

        [Fact]
        public void Invalid_date_format_will_throw_exception()
        {
            // Arrange
            var merger = new LogMerger();
            using var output = new MemoryStream();
            using var log = GetStreamFromLogs("abcde");

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => merger.MergeLogFiles(output, log));
        }

        // TODO:
        // Add more tests for:
        //  * No files
        //  * Lots of files
        //  * Empty files
        //  * 

        private static Stream GetStreamFromLogs(params string[] logs)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            foreach (var log in logs)
            {
                writer.WriteLine(log);
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
