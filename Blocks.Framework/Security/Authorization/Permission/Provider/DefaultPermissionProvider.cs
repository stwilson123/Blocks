using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Manager;

namespace Blocks.Framework.Security.Authorization.Permission.Provider
{
    public class DefaultPermissionProvider  : IPermissionProvider
    {
        private readonly INavigationManager _navigationManager;

        public DefaultPermissionProvider(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        public IList<Permission> GetPermissions()
        {
            return _navigationManager.Menus.Select(m => m.Value).SelectMany(n => n.Items?.SelectMany((i => i.HasPermissions != null ? i.HasPermissions : new Permission[0]))).ToList();
        }
    }
}