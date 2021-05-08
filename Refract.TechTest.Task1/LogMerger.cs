using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Refract.TechTest.Task1
{
    public class LogMerger
    {
        /// <summary>
        /// The naive solution here would be to load all files, parse the lines into C# objects with date and log text, chuck them into a big list, sort it and the write to a file
        /// That would work fine but potentially have a huge memory cost so would be a bad approach.
        /// Instead we can rely on <see cref="System.IO.StreamReader.ReadLine"/> to only give us the files line by line.
        /// 
        /// Assumptions:
        ///     * The log files all have messages in chronological order
        ///     * If one log file has an invalid date, we will throw an exception and stop exporting. No instructions on how to handle that, some options:
        ///         o Delete the output file since it cannot be trusted
        ///         o Ignore the failed line
        ///         o Consider that entire file corrupt and no longer continue processing it
        ///         o Output everything that was valid up to that point
        ///         o Just throw an exception - this is what I have chosen
        /// </summary>
        /// <param name="outputStream">The stream to write the logs to</param>
        /// <param name="sourceLogFiles">An array of log files to read</param>
        public void MergeLogFiles(Stream outputStream, params Stream[] sourceLogFiles)
        {
            // Load all files to begin processing
            var files = sourceLogFiles
                .Select(sourceLogFile => new LogFile(sourceLogFile))
                .ToList();

            var outputFile = new StreamWriter(outputStream);
            
            LogEntry earliestLog;

            do
            {
                earliestLog = GetEarliestEntry(files);

                if (earliestLog != null)
                {
                    outputFile.WriteLine(earliestLog.RawLog);
                }

            } while (earliestLog != null);
            
            outputFile.Flush();
        }

        /// <summary>
        /// Returns the earliest entry in all the files. Will then advance that file to the next line.
        /// </summary>
        /// <param name="files">The log files</param>
        /// <returns>The earliest entry from all of the logs</returns>
        private static LogEntry GetEarliestEntry(IEnumerable<LogFile> files)
        {
            LogFile earliestLog = null;

            foreach (var file in files.Where(file => file.CurrentLine != null))
            {
                if (earliestLog == null || file.CurrentLine.DateTime < earliestLog.CurrentLine.DateTime)
                {
                    earliestLog = file;
                }
            }

            if (earliestLog == null)
            {
                // No more entries, return a null so the process can terminate
                return null;
            }

            var log = earliestLog.CurrentLine;
            earliestLog.ReadNextLine();

            return log;
        }
    }
}
