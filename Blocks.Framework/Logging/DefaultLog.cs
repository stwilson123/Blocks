﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Logging
{
    public class DefaultLog : ILog
    {
        private readonly ILogger logger;
        public DefaultLog(ILogger logger)
        {
            this.logger = logger;
        }

        public void Logger(LogModel logModel)
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