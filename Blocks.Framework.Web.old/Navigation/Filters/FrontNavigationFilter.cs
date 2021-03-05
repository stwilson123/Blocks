using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Security.Authorization.Permission;
using Castle.Core.Internal;

namespace Blocks.Framework.Web.Navigation.Filters
{
    public class FrontNavigationFilter : INavigationFilter, ISingletonDependency
    {
        public async Task<IEnumerable<INavigationDefinition>> Filter(
            IEnumerable<INavigationDefinition> navigationDefinitions)
        {
            if (navigationDefinitions.IsNullOrEmpty())
                return navigationDefinitions;
            var navDefinitionsResult = new List<INavigationDefinition>();
            foreach (var navigationDefinition in navigationDefinitions)
            {
                navDefinitionsResult.Add(await FillNav(navigationDefinition));
            }

            return navDefinitionsResult;
        }

        private async Task<INavigationDefinition> FillNav(INavigationDefinition navigationDefinition)
        {
            if (navigationDefinition == null)
            {
                throw new BlocksException(StringLocal.Format("There is no menu "));
            }

            var navDefinitionResult = new NavigationDefinition(navigationDefinition);
            await FillNavItems(navigationDefinition.Items, navDefinitionResult.Items);
            return navDefinitionResult;
        }

        private async Task FillNavItems(IList<INavigationItemDefinition> source,
            IList<INavigationItemDefinition> result)
        {
            foreach (var menuItem in source)
            {
                var fillUrlMenuItem = FillNavUrl(menuItem);
                result.Add(fillUrlMenuItem != null ? fillUrlMenuItem : menuItem);

            }
        }

        private WebNavigationItemDefinition FillNavUrl(INavigationItemDefinition navDefinitionItem)
        {
            if (navDefinitionItem == null)
                return null;
            var navItem = navDefinitionItem;

            if (navItem.NavigationType == 1)
            {
                var p = navItem.Name;
                var navigationUrl = Mvc.Route.RouteHelper.GetUrl(navItem.RouteValues);
                var permissons = new List<Permission>();

                if (navItem.RouteValues.Values.Count() < 3)
                    throw new BlocksException(StringLocal.Format("Navigation controller or action {0} can't null",
                        navigationUrl));
                var requiredPermissions = new List<Permission>();
                if(!navItem.HasPermissions.IsNullOrEmpty())
                    requiredPermissions.Add(navItem.HasPermissions.FirstOrDefault());
                return new WebNavigationItemDefinition(navItem.Name,
                    navItem.DisplayName, Mvc.Route.RouteHelper.GetUrl(navItem.RouteValues),
                    navItem.RequiresAuthentication, requiredPermissions.ToArray()
                    , navItem.CustomData, navItem.IsVisible, navItem.HasPermissions, navItem.RouteValues,
                    navItem.NavigationType
                );
            }

            return null;
        }
    }
}