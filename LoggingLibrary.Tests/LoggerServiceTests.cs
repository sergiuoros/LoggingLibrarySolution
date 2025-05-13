using LoggingLibrary.Interfaces;
using LoggingLibrary.Models;
using LoggingLibrary.Services;
using Moq;

namespace LoggingLibrary.Tests
{
    public class LoggerServiceTests
    {
        [Fact]
        public async Task LogAsync_CallsAllChannelsWithCorrectMessage()
        {
            // Arrange
            var mockChannel1 = new Mock<ILoggerChannel>();
            var mockChannel2 = new Mock<ILoggerChannel>();

            var logger = new LoggerService(
            [
                mockChannel1.Object,
                mockChannel2.Object
            ]);

            // Act
            await logger.LogAsync("Hello test", LogLevel.Warning);

            // Assert
            mockChannel1.Verify(c => c.LogAsync(It.Is<LogMessage>(
                msg => msg.Message == "Hello test" && msg.Level == LogLevel.Warning)), Times.Once);

            mockChannel2.Verify(c => c.LogAsync(It.Is<LogMessage>(
                msg => msg.Message == "Hello test" && msg.Level == LogLevel.Warning)), Times.Once);
        }

        [Fact]
        public async Task LogAsync_HandlesExceptionsWithoutThrowing()
        {
            // Arrange
            var faultyChannel = new Mock<ILoggerChannel>();
            faultyChannel.Setup(c => c.LogAsync(It.IsAny<LogMessage>()))
                         .ThrowsAsync(new InvalidOperationException("Oops"));

            var workingChannel = new Mock<ILoggerChannel>();

            var logger = new LoggerService(
            [
                faultyChannel.Object,
                workingChannel.Object
            ]);

            // Act
            var exception = await Record.ExceptionAsync(() =>
                logger.LogAsync("Test message", LogLevel.Info));

            // Assert
            Assert.Null(exception); 
            workingChannel.Verify(c => c.LogAsync(It.IsAny<LogMessage>()), Times.Once);
        }

        [Fact]
        public async Task LogAsync_UsesErrorHandlerWhenExceptionOccurs()
        {
            // Arrange
            var faultyChannel = new Mock<ILoggerChannel>();
            faultyChannel.Setup(c => c.LogAsync(It.IsAny<LogMessage>()))
                         .ThrowsAsync(new Exception("Something went wrong"));

            var errorHandler = new Mock<ILoggerErrorHandler>();

            var logger = new LoggerService([faultyChannel.Object], errorHandler.Object);

            // Act
            await logger.LogAsync("Error test", LogLevel.Critical);

            // Assert
            errorHandler.Verify(e => e.Handle(
                It.IsAny<Exception>(),
                It.Is<LogMessage>(m => m.Message == "Error test" && m.Level == LogLevel.Critical),
                It.Is<ILoggerChannel>(c => c == faultyChannel.Object)
            ), Times.Once);
        }
    }
}
