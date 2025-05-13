using LoggingLibrary.Channels;
using LoggingLibrary.Models;

namespace LoggingLibrary.Tests
{
    public class FileLoggerTests
    {
        [Fact]
        public async Task LogAsync_WritesLogMessageToFile()
        {
            // Arrange
            var filePath = "TEMP-test_log.txt";
            if (File.Exists(filePath)) File.Delete(filePath);

            var logger = new FileLogger(filePath);
            var message = new LogMessage
            {
                Message = "Test file log message",
                Level = LogLevel.Info,
                Timestamp = DateTime.UtcNow
            };

            // Act
            await logger.LogAsync(message);

            // Assert
            Assert.True(File.Exists(filePath));
            var content = await File.ReadAllTextAsync(filePath);
            Assert.Contains("Test file log message", content);

            // Cleanup
            File.Delete(filePath);
        }
    }
}
