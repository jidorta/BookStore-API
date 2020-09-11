using BookStore_API.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogIError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarm(string message)
        {
            logger.Warn(message);
        }
    }
}
