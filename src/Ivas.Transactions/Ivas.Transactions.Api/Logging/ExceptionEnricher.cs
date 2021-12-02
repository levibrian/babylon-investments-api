using Serilog.Core;
using Serilog.Events;

namespace Ivas.Transactions.Api.Logging
{
    public class ExceptionEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception == null)
                return;

            var logEventProperty = 
                propertyFactory
                    .CreateProperty("EscapedException", 
                        logEvent
                            .Exception
                            .ToString()
                            .Replace("\r\n", "\\r\\n"));
            
            logEvent.AddPropertyIfAbsent(logEventProperty);
        }
    }
}