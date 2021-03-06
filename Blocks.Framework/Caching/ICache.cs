﻿using System;

namespace Blocks.Framework.Caching {
    public interface ICache<TKey, TResult> {
        TResult Get(TKey key, Func<AcquireContext<TKey>, TResult> acquire);

        bool Put(TKey key, TResult obj);


        bool Remove(TKey key);
        bool Remove();


    }
}
