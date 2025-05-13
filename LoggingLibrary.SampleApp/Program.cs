using LoggingLibrary.Channels;
using LoggingLibrary.Interfaces;
using LoggingLibrary.Models;
using LoggingLibrary.Services;

namespace LoggingLibrary.SampleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting logging SampleApp...\n");

            var loggers = new List<ILoggerChannel>
            {
                new ConsoleLogger(),
                new FileLogger("TEMP-demo_log.txt")
            };

            var loggerService = new LoggerService(loggers);

            await loggerService.LogAsync("Application started", LogLevel.Info);
            await loggerService.LogAsync("This is a warning", LogLevel.Warning);
            await loggerService.LogAsync("Something went wrong!", LogLevel.Critical);

            Console.WriteLine("\nLogging complete. Check console and **\\LoggingLibrary\\LoggingLibrary.SampleApp\\bin\\Debug\\net8.0\\TEMP-demo_log.txt.");
        }
    }
}
