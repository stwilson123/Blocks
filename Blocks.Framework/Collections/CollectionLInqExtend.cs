 
using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.Exceptions.Helper;

namespace Blocks.Framework.Collections
{
    public static class CollectionLInqExtend
    {
        public static void ForEach<T>(this IEnumerable<T> collection , Action<T> action) 
        {
            if (action == null || collection == null)
                ExceptionHelper.ThrowArgumentNullException(ExceptionArgument.match);
            foreach (var item in collection)
            {
                action(item);
            }
            
           
        }
    }
}