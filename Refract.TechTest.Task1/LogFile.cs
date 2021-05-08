using System;
using System.IO;

namespace Refract.TechTest.Task1
{
    public class LogFile : IDisposable
    {
        private readonly StreamReader _file;

        public LogFile(Stream logFile)
        {
            _file = new StreamReader(logFile);
            ReadNextLine();
        }

        public LogEntry CurrentLine { get; set; }

        public void ReadNextLine()
        {
            var line = _file.ReadLine();

            CurrentLine = line == null 
                ? null 
                : new LogEntry(line);
        }

        public void Dispose()
        {
            _file?.Dispose();
        }
    }
}