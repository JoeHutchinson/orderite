using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging
{
    public class LoggerAdapter<T> : Core.Interfaces.ILogger<T>
    {
        private readonly Microsoft.Extensions.Logging.ILogger<T> _logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void Warn(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
