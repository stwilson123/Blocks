using System;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Caching
{
    public interface IAsyncTokenProvider : ISingletonDependency {
        IVolatileToken GetToken(Action<Action<IVolatileToken>> task);
    }
}