using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Logging
{
    public interface IAppLogging<T>
    {
        void LoggingInformation(string message , params object[] args);
        void LoggingWarning(string message , params object[] args);
    }
}
