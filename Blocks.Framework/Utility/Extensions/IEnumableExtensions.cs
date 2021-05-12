using System;
using System.Collections.Generic;

namespace Blocks.Framework.Utility.Extensions
{
    public static class IEnumableExtensions
    {
        public static IEnumerable<TSource> ConcatWhether<TSource>(this IEnumerable<TSource> iEnumerable,TSource Item)
        {
            return iEnumerable.ConcatWhether(new[] { Item });
        }
        
        public static IEnumerable<TSource> ConcatWhether<TSource>(this IEnumerable<TSource> iEnumerable,IEnumerable<TSource> Items)
        {
            var result = iEnumerable == null ? new List<TSource>() : iEnumerable;
            if (iEnumerable != null)
            {
                foreach (TSource source in result)
                    yield return source;
            }

            if (Items != null)
            {
                foreach (TSource source in Items)
                    yield return source;
            }
      
        }
        public static IList<TSource> AddRange<TSource>(this IList<TSource> iEnumerable, IList<TSource> Items)
        {
            if (Items == null)
                return iEnumerable;

            foreach (var item in Items)
            {
                iEnumerable.Add(item);
            }
            return iEnumerable;
        }


        public static void ForEach<TSource>(this IEnumerable<TSource> iEnumerable, Action<TSource,long> actions)
        {
            if (iEnumerable == null)
                return;


            var i = 0;
            foreach (var item in iEnumerable)
            {
                actions(item, i++);
            }
        }
    }
}