using System;
using System.Linq;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation.Builder;

namespace Blocks.Framework.Navigation
{
    public static class NavigationExtensions
    {
        public static INavigationDefinition AddBuilder(this INavigationDefinition navItem, Action<NavigationItemBuilder> builderAction)
        {
            var navigationItemBuilder = new NavigationItemBuilder();
            builderAction(navigationItemBuilder);
            var navigationItem =  navigationItemBuilder.Build();
            if (navItem.Items.Any(i => i.Name == navigationItem.Name))
                throw new BlocksException(StringLocal.Format("System find navigatiomItems has same Name \"{0}\"",navigationItem.Name));
            navItem.AddItem(navigationItem);
            return navItem;
        }
        
        public static string GetUniqueId(this INavigationItemDefinition navItem)
        {
            return navItem.Name + "_" + navItem.RouteValues?[RouteConst.area];
        }
        
        
    }
}