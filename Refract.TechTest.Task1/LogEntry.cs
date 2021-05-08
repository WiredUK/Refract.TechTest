using System;
using System.Globalization;

namespace Refract.TechTest.Task1
{
    public class LogEntry
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public LogEntry(string rawLog)
        {
            RawLog = rawLog;

            var dateSpan = rawLog.AsSpan(0, 23);

            if (!DateTime.TryParseExact(dateSpan, new []{ DateTimeFormat}, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                throw new ArgumentOutOfRangeException(nameof(rawLog), "Log file contained an invalid date");
            }

            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
        public string RawLog { get; }
    }
}