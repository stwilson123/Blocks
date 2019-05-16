using Abp.Dependency;
using Castle.Core.Logging;
using System;

namespace Blocks.Framework.Logging
{
    public static class LogHelper
    {
        private static ILogger logger;
        static LogHelper()
        {
            logger = IocManager.Instance.IsRegistered(typeof(ILoggerFactory))
                ? IocManager.Instance.Resolve<ILoggerFactory>().Create(typeof(LogHelper))
                : NullLogger.Instance;
        }
        public static void Log(LogModel logModel)
        {
            
            switch(logModel.LogSeverity)
            {
                case LogSeverity.Debug: logger.Debug(logModel.Message, logModel.ex); break;
                case LogSeverity.Info: logger.Info(logModel.Message, logModel.ex); break;
                case LogSeverity.Warn: logger.Warn(logModel.Message, logModel.ex); break;
                case LogSeverity.Error: logger.Error(logModel.Message, logModel.ex); break;
                case LogSeverity.Fatal: logger.Fatal(logModel.Message, logModel.ex); break;

            }
        }

        public static void LogException(Exception ex)
        {
            LogException(logger, ex);
        }

        public static void LogException(ILogger logger, Exception ex)
        {
            // var severity = (ex as IHasLogSeverity)?.Severity ?? LogSeverity.Error;

            Log(new LogModel() { LogSeverity = LogSeverity.Error, Message = ex.Message });


           // LogValidationErrors(logger, ex);
        }
    }
}