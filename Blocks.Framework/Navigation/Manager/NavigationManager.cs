using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Collections;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Event;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation.Event;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Navigation.Provider;
using Blocks.Framework.NullObject;
using ISingletonDependency = Blocks.Framework.Ioc.Dependency.ISingletonDependency;
using NavigationProviderContext = Blocks.Framework.Navigation.Provider.NavigationProviderContext;

namespace Blocks.Framework.Navigation.Manager
{
    internal class NavigationManager : INavigationManager
    {
        public IDictionary<string, INavigationDefinition> Menus { get; private set; }

        private IDictionary<string, INavigationDefinition> sourceMenus;

        public INavigationDefinition MainMenu
        {
            get { return Menus["MainMenu"]; }
        }

        private readonly IIocResolver _iocResolver;
 
        private readonly INavigationConfiguration _configuration;

        public NavigationManager(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;

            Menus = new Dictionary<string, INavigationDefinition>
            {
                {
                    "MainMenu", new NavigationDefinition("MainMenu",new LocalizableString(AbpConsts.LocalizationSourceName,"MainMenu"))
                }
            };
            sourceMenus = new Dictionary<string, INavigationDefinition>();
        }

             public IDomainEventBus EventBus { get; set; }

        public void Initialize()
        {
            var context = new NavigationProviderContext(this);

            //should be register thought LocalizsableModeule
             
            foreach (var provider in _iocResolver.ResolveAll<INavigationProvider>())
            {
                provider.SetNavigation(context);
            }

            sourceMenus = Menus.AutoMapTo<IDictionary<string, INavigationDefinition>>();
            Menus =    Filter(Menus).Result;

            if(_iocResolver.IsRegistered<IDomainEventBus>())
            {
                
                _iocResolver.Resolve<IDomainEventBus>().Trigger(new MenusInitEventData() {
                    NavigationItems = Menus.SelectMany(t => t.Value.Items).ToArray()
                });
            }
                
           

            //TODO Menu Adapter
            {
            }


            //Adapter abp
            var adpaterMenusModel = Menus.Values.FirstOrDefault().Items.Select(menuItem =>
                funTransfter(menuItem));
            foreach (var menus in adpaterMenusModel)
            {
                _iocResolver.Resolve<Abp.Application.Navigation.NavigationManager>().MainMenu.AddItem(menus);
            }
        }

        private async Task<IDictionary<string, INavigationDefinition>> Filter(
            IDictionary<string, INavigationDefinition> navItems)
        {
            if (_iocResolver.IsRegistered<INavigationFilter>())
            {
                var navigationFilters = _iocResolver.ResolveAll<INavigationFilter>();
                var navResult = new Dictionary<string, INavigationDefinition>();
                foreach (var nav in navItems)
                {
                    IEnumerable<INavigationDefinition> result = new List<INavigationDefinition>() {nav.Value};
                    foreach (var filter in navigationFilters)
                    {
                        result = await filter.Filter(result);
                    }
                    navResult.Add(nav.Key, result.FirstOrDefault());
                }

                return navResult;
            }

            return navItems;
        }

        private MenuItemDefinition funTransfter(INavigationItemDefinition menuItem)
        {
            
            var localizableString = (LocalizableString) (menuItem.DisplayName);
            var menuDefinition = new MenuItemDefinition(
                menuItem.Name,
                new Abp.Localization.LocalizableString(localizableString.Name, localizableString.SourceName), null, RouteHelper.GetUrl(menuItem.RouteValues));
//            menuItem.Items?.ForEach(navigationItemDefinition =>
//            {
//                menuDefinition.AddItem(funTransfter(navigationItemDefinition));
//            });


            return menuDefinition;
        }

        class RouteHelper
        {
            public static string GetUrl(IDictionary<string, object> routeValue)
            {
                if (routeValue == null || !routeValue.Any())
                    return "";
                var controllerServiceName = routeValue["area"]?.ToString() + "/" + routeValue["controller"]?.ToString()
                                           + "/" + routeValue["action"]?.ToString();
                return controllerServiceName;
            }
            public static string GetControllerPath(IDictionary<string, object> routeValue)
            {
                var controllerServiceName = routeValue["area"]?.ToString() + "/" + routeValue["controller"]?.ToString();
                return controllerServiceName;
            }
        }

    }
}