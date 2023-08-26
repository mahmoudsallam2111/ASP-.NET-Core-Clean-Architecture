using HR.LeaveManagement.Application.Contracts.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Services.Logging
{
    public class LoggingAdapter<T> : IAppLogging<T>
    {
        private readonly ILogger<T> _logger;

        public LoggingAdapter(ILoggerFactory loggingFactory)
        {
            _logger = loggingFactory.CreateLogger<T>();
        }
        public void LoggingInformation(string message, params object[] args)
        {
             _logger.LogInformation(message, args); 
        }

        public void LoggingWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args); 
        }
    }
}
