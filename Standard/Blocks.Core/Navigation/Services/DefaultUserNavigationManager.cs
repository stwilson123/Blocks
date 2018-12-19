using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Localization;
using Blocks.Core.Navigation.Models;
using Blocks.Framework.Collections.Extensions;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Security.Authorization.User;
using Blocks.Framework.Web.Navigation;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using INavigationManager = Blocks.Framework.Navigation.Manager.INavigationManager;
using ITransientDependency = Blocks.Framework.Ioc.Dependency.ITransientDependency;
using Blocks.Framework.Utility.Extensions;
namespace Blocks.Core.Navigation.Services
{
    public class DefaultUserNavigationManager: IUserNavigationManager, ITransientDependency
    {
        private readonly INavigationManager _navigationManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILocalizationContext _localizationContext;

        public DefaultUserNavigationManager(INavigationManager navigationManager,IAuthorizationService authorizationService, ILocalizationContext localizationContext)
        {
            _navigationManager = navigationManager;
            _authorizationService = authorizationService;
            _localizationContext = localizationContext;
        }

        public async Task<UserNavigation> GetMenuAsync(string menuName, IUserIdentifier user)
        {
            var navDefinition = _navigationManager.Menus.GetOrDefault(menuName);
            if (navDefinition == null)
            {
                throw new BlocksException(StringLocal.Format("There is no menu with given name: " + menuName));
            }
            var userMenu = new UserNavigation(navDefinition.Name,new List<UserNavigationItem>());
            await FilterUserNavigation(user,navDefinition.Items, userMenu.Items);
            return userMenu;
        }

        private async Task  FilterUserNavigation(IUserIdentifier user, IList<INavigationItemDefinition> navDefinitionItems, IList<UserNavigationItem> userMenuItems)
        {
            foreach (var navigationItemDefinition in navDefinitionItems)
            {
                var webNavItem = navigationItemDefinition as WebNavigationItemDefinition;
                if (webNavItem == null)
                    throw new BlocksCoreException(StringLocal.Format("WebNavItem not found"));
                if( !await _authorizationService.TryCheckAccess(webNavItem.RequirePermissions,webNavItem.RequiresAuthentication,user))
                    continue;
                userMenuItems.Add( CreateUserNavItem(user, webNavItem));
               
            }

        }

        private  UserNavigationItem CreateUserNavItem(IUserIdentifier user, WebNavigationItemDefinition webNavItem)
        {
            var userNavItem = new UserNavigationItem(webNavItem);
            //var hasPermissions = new List<Permission>();
            //foreach (var hasPermission in userNavItem.HasPermissions)
            //{
            //    if (await _authorizationService.TryCheckAccess(webNavItem.HasPermissions,
            //        userNavItem.RequiresAuthentication, user))
            //        hasPermissions.Add(hasPermission);
            //}

            //userNavItem.HasPermissions = hasPermissions.ToArray();
            return userNavItem;
        }


        public async Task<IReadOnlyList<UserNavigation>> GetMenusAsync(IUserIdentifier user)
        {
            var userMenus = new List<UserNavigation>();

            foreach (var menu in _navigationManager.Menus.Values)
            {
                userMenus.Add(await GetMenuAsync(menu.Name, user));
            }

            return userMenus;
        }
        
        
    }
}