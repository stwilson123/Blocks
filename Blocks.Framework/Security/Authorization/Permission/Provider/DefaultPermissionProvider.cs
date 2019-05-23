using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.Navigation;

namespace Blocks.Framework.Security.Authorization.Permission.Provider
{
    public class DefaultPermissionProvider  : IPermissionProvider
    {
        private readonly IEnumerable<INavigationDefinition> _navigationDefinitions;

        public DefaultPermissionProvider(IEnumerable<INavigationDefinition> navigationDefinitions)
        {
            _navigationDefinitions = navigationDefinitions;
        }
        public IList<Permission> GetPermissions()
        {
            return _navigationDefinitions.SelectMany(n => n.Items.SelectMany((i => i.HasPermissions))).ToList();
        }
    }
}