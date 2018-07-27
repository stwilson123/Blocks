using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Navigation.Filters
{
    public class DefaultNavigationFilter : INavigationFilter 
    {
        private readonly IExtensionManager extensionManager;

        public DefaultNavigationFilter(IExtensionManager extensionManager)
        {
            this.extensionManager = extensionManager;
        }
        public async Task<IEnumerable<INavigationDefinition>> Filter(IEnumerable<INavigationDefinition> navigationDefinitions)
        {
            foreach (var navDef in navigationDefinitions)
            {
                var areaName = extensionManager.AvailableExtensions()
                    .FirstOrDefault(e => e.Id == navDef.GetType().Assembly.FullName).Name;
                foreach (var navItem in navDef.Items)
                {
                    if (!navItem.RouteValues.ContainsKey(RouteConst.area))
                        navItem.RouteValues[RouteConst.area] = areaName;
                }
            }
            
            return navigationDefinitions;
        }
    }
}
