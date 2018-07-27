using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;

namespace Blocks.Framework.Ioc
{
    public static class CastleWinsdorExtensions
    {
        public static IEnumerable<ComponentModel> GetHandlers(this IKernel kernel)
        {
            return kernel.GraphNodes.Select(t => (ComponentModel) t);
        }
    }
}