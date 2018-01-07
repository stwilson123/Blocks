using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blocks.Framework.Untility.Extensions
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

    }
}