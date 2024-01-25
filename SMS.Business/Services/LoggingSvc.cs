using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Core;
using NLog;
using NLog.Config;

namespace SMS.Business.Service
{
    public class LoggingSvc : ILogger
    {
        private static Logger logger = LogManager.GetLogger("NLogLogger");

        public static ILogger GetLogger()
        {
            return new LoggingSvc();
        }

        public void Error(string message, Exception exception)
        {
            logger.ErrorException(message, exception);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Log(LogLevel logLevel, string message, Exception exception)
        {
            logger.LogException(logLevel, message, exception);
        }

        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            logger.Log(logLevel, message, args);
        }
    }
}
