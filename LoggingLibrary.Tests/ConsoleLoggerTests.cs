using LoggingLibrary.Channels;
using LoggingLibrary.Models;

namespace LoggingLibrary.Tests
{
    public class ConsoleLoggerTests
    {
        [Fact]
        public async Task LogAsync_WritesMessageToConsoleOutput()
        {
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var logger = new ConsoleLogger();
            var message = new LogMessage
            {
                Message = "Test console output",
                Level = LogLevel.Error,
                Timestamp = DateTime.UtcNow
            };

            // Act
            await logger.LogAsync(message);

            // Assert
            var output = consoleOutput.ToString();
            Assert.Contains("Test console output", output);
            Assert.Contains("Error", output);
        }
    }
}
