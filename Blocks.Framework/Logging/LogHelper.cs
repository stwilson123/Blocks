using Abp.Dependency;
using Castle.Core.Logging;

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
                case LogSeverity.Debug: logger.Debug(logModel.Message); break;
                case LogSeverity.Info: logger.Info(logModel.Message); break;
                case LogSeverity.Warn: logger.Warn(logModel.Message); break;
                case LogSeverity.Error: logger.Error(logModel.Message); break;
                case LogSeverity.Fatal: logger.Fatal(logModel.Message); break;

            }
        }
    }
}