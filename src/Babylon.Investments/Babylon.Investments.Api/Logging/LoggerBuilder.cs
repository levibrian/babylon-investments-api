using Serilog;
using Serilog.Events;

namespace Babylon.Investments.Api.Logging
{
    public static class LoggerBuilder
    {
        public static ILogger Configure()
        {
            return new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "[{Level:u3}] {Message:lj}-{Properties:j}{NewLine}")
                .MinimumLevel.Is(LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.With<ExceptionEnricher>()
                .CreateLogger();
        }
    }
}