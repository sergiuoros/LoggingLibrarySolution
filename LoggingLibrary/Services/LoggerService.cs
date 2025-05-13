using LoggingLibrary.Interfaces;
using LoggingLibrary.Models;

namespace LoggingLibrary.Services
{
    public class LoggerService
    {
        private readonly List<ILoggerChannel> _channels = new();
        private readonly ILoggerErrorHandler _errorHandler;

        public LoggerService(IEnumerable<ILoggerChannel> channels, ILoggerErrorHandler errorHandler = null)
        {
            _channels.AddRange(channels);
            _errorHandler = errorHandler;
        }

        public async Task LogAsync(string message, LogLevel level = LogLevel.Info)
        {
            var logMessage = new LogMessage
            {
                Message = message,
                Level = level
            };

            var tasks = _channels.Select(async channel =>
            {
                try
                {
                    await channel.LogAsync(logMessage);
                }
                catch (Exception ex)
                {
                    _errorHandler?.Handle(ex, logMessage, channel);
                }
            });

            await Task.WhenAll(tasks);
        }
    }

}
