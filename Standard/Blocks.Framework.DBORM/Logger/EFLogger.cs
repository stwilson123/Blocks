using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Blocks.Framework.DBORM.Logger
{
    public class EFLogger : ILogger
    {
        private readonly string categoryName;

        private readonly Stopwatch sw;
        public EFLogger(string categoryName) { 
            this.categoryName = categoryName;

            this.sw = new Stopwatch();
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel > LogLevel.Debug;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var a = RelationalEventId.CommandExecuting.Name;
           

            var logContent = formatter(state, exception);

            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command" && logLevel >= LogLevel.Information)
                  
            {
                Trace.TraceInformation(logContent);
            }

            if (categoryName == "LinqToDB")
            {
                Trace.TraceInformation(logContent);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
