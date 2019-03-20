using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Logging
{
    public interface ILog : ITransientDependency
    {
        void Logger(LogModel logModel);
    }
}
