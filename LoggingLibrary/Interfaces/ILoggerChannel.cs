using LoggingLibrary.Models;

namespace LoggingLibrary.Interfaces
{
    public interface ILoggerChannel
    {
        Task LogAsync(LogMessage message);
    }
}
