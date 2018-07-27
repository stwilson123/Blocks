﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blocks.Framework.ApplicationServices.Controller;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Web.Mvc.Controllers.Manager;
using Blocks.Framework.Web.Mvc.Route;
using Castle.Core.Internal;

namespace Blocks.Framework.Web.Navigation.Filters
{
    public class MvcNavigationFilter : INavigationFilter,ISingletonDependency
    {
        private MvcControllerManager _defaultControllerManager;
        public MvcNavigationFilter(MvcControllerManager defaultControllerManager)
        {
            _defaultControllerManager = defaultControllerManager;
        }
        public async Task<IEnumerable<INavigationDefinition>> Filter(IEnumerable<INavigationDefinition> navigationDefinitions)
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
                throw new BlocksException(StringLocal.Format("There is no menu " ));
            }
            var navDefinitionResult = new NavigationDefinition(navigationDefinition);
            await FillNavItems(navigationDefinition.Items, navDefinitionResult.Items);
            return navDefinitionResult;
        }
        private async Task FillNavItems(IList<INavigationItemDefinition> source,IList<INavigationItemDefinition> result)
        {
            foreach (var menuItem in source)
            {
                var fillUrlMenuItem = FillNavUrl(menuItem);
//                if (!menuItem.IsLeaf)
//                    await FillNavItems(menuItem.Items, fillUrlMenuItem.Items);

                result.Add(fillUrlMenuItem);
            }
        }
        private WebNavigationItemDefinition FillNavUrl(INavigationItemDefinition navDefinitionItem)
        {
            if (navDefinitionItem == null)
                return null;
            var navItem = navDefinitionItem;
            var controllerPath = RouteHelper.GetControllerPath(navItem.RouteValues);
            var controllerActionKv = _defaultControllerManager.FindOrNull(controllerPath)?.Actions.FirstOrDefault(a => a.Key == navItem.RouteValues["action"]?.ToString());
            if (controllerActionKv?.Key == null)
            {
                throw new BlocksException(StringLocal.Format("Navigation or action {0} can't found",controllerPath));
            }
            var controllerAction = controllerActionKv.Value.Value;
            var requirePermission = controllerAction.GetAuthorize()?.Select(p => Permission.Create(p,navItem.Name,new LocalizableString(navItem.DisplayName.SourceName,p))).ToArray();
            return new WebNavigationItemDefinition(navItem.Name,
                navItem.DisplayName, RouteHelper.GetUrl(navItem.RouteValues), navItem.RequiresAuthentication, requirePermission
                , navItem.CustomData, navItem.IsVisible, navItem.HasPermissions,navItem.RouteValues
            );
        }
    }
}