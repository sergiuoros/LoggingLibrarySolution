namespace LoggingLibrary.Models
{
    public class LogMessage
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
        public LogLevel Level { get; set; } = LogLevel.Info;
    }
}
