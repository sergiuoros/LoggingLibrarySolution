# LoggingLibrary

A lightweight, reusable .NET class library for application event logging to multiple channels (console, file, etc.).

## Features

- Non-blocking (async) logging
- Multiple output channels supported (e.g., Console, File)
- Easily extensible with new logging channels
- Clean architecture, SOLID principles
- No external logging libraries (e.g., Serilog, NLog)

---

## Structure

| Project | Description |
|--------|-------------|
| `LoggingLibrary` | Core class library containing logging logic and interfaces |
| `LoggingLibrary.Tests` | xUnit test project with coverage for service behavior and log channels |
| `LoggingLibrary.SampleApp` | Console app that demonstrates using the library in practice |

---

### Build the solution

	dotnet build

###  Run the tests

	dotnet test

### Run the sample app

	dotnet run --project LoggingLibrary.SampleApp
	
| After running the app, you'll find TEMP-demo_log.txt in **\\LoggingLibrary\\LoggingLibrary.SampleApp\\bin\\Debug\\net8.0\\TEMP-demo_log.txt. |	