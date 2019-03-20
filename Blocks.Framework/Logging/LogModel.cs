using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Logging
{
    public class LogModel
    {
        public string  Message { get; set; }

        public LogSeverity LogSeverity { get; set; } = LogSeverity.Info;
    }
}
