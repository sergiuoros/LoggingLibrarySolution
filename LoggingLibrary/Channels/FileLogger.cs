using LoggingLibrary.Interfaces;
using LoggingLibrary.Models;

namespace LoggingLibrary.Channels
{
    public class FileLogger : ILoggerChannel
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public async Task LogAsync(LogMessage message)
        {
            var logLine = $"[{message.Timestamp}] [{message.Level}] {message.Message}{Environment.NewLine}";
            await File.AppendAllTextAsync(_filePath, logLine);
        }
    }
}
