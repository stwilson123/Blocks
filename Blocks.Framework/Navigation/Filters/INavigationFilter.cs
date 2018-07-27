using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blocks.Framework.Navigation.Filters
{
    public interface INavigationFilter
    {
        Task<IEnumerable<INavigationDefinition>> Filter(IEnumerable<INavigationDefinition> navigationDefinitions);
    }
}