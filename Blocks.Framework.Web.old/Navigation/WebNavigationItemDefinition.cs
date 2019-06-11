using System.Collections.Generic;
using Abp.Application.Features;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Types;

namespace Blocks.Framework.Web.Navigation
{
    public class WebNavigationItemDefinition : INavigationItemDefinition
    {
        public string Name { get; }
        public ILocalizableString DisplayName { get; }
        public IDictionary<string, object> RouteValues { get; }
//        public IPermissionDependency PermissionDependency { get; }
        public IFeatureDependency FeatureDependency { get; }
        public bool RequiresAuthentication { get; }
        public Permission[]  HasPermissions { get; }
        public bool IsLeaf { get; }
        public object CustomData { get; }
        public bool IsVisible { get; set; }
        public string uId => this.GetUniqueId();


        /// <summary>
        /// The URL to navigate when this menu item is selected. Optional.
        /// </summary>
        public string Url { get;  }

        public Permission[] RequirePermissions { get; }
        public string ExtensionName { get ; set; }

        internal WebNavigationItemDefinition() : base()
        {
            
        }
        
        public WebNavigationItemDefinition(string name, ILocalizableString displayName,string url, bool requiresAuthentication = false, Permission[] requiredPermissionName = null, object customData = null,  bool isVisible = true,Permission[] hasPermissions = null, IDictionary<string, object> routeValues = null)  
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(displayName, nameof(displayName));

            Name = name;
            DisplayName = displayName;
            RequiresAuthentication = requiresAuthentication;
            RequirePermissions = requiredPermissionName;
            //   RequiredPermissionName = HasPermissions;
            CustomData = customData;
            // FeatureDependency = featureDependency;
            //   IsEnabled = isEnabled;
            IsVisible = isVisible;
//            PermissionDependency = permissionDependency;
//            Items = new List<INavigationItemDefinition>();
            Url = url;
            HasPermissions = hasPermissions;
            RouteValues = routeValues;
        }

       
//        public INavigationItemDefinition AddItem(INavigationItemDefinition navItem)
//        {
//            Items.Add(navItem);
//            return this;
//        }
    }
}