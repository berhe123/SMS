using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace SMS.Core
{
    public interface ILogger
    {
        void Error(string message, Exception exception);
        void Error(string message, params object[] args);
        void Log(LogLevel logLevel, string message, Exception exception);
        void Log(LogLevel logLevel, string message, params object[] args);
    }
}
