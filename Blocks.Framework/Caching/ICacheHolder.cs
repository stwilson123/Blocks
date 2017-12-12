using System;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Caching {
    public interface ICacheHolder : ISingletonDependency {
        ICache<TKey, TResult> GetCache<TKey, TResult>(Type component);
    }
}
