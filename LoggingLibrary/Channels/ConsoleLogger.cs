using LoggingLibrary.Interfaces;
using LoggingLibrary.Models;

namespace LoggingLibrary.Channels
{
    public class ConsoleLogger : ILoggerChannel
    {
        public Task LogAsync(LogMessage message)
        {
            Console.WriteLine($"[{message.Timestamp}] [{message.Level}] {message.Message}");
            return Task.CompletedTask;
        }
    }
}
