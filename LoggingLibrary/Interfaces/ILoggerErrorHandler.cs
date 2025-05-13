using LoggingLibrary.Models;

namespace LoggingLibrary.Interfaces
{
    public interface ILoggerErrorHandler
    {
        void Handle(Exception ex, LogMessage message, ILoggerChannel channel);
    }
}
