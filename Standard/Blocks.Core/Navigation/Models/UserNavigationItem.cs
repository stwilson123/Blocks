using System.Collections.Generic;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Types;
using Blocks.Framework.Web.Navigation;

namespace Blocks.Core.Navigation.Models
{
    public class UserNavigationItem : INavigationItemDefinition
    {
        private readonly WebNavigationItemDefinition _navItem;
        public string Icon { get; set; } 
        public long Order { get; set; }

        public string Name
        {
            get { return _navItem.Name; }
        }
        public ILocalizableString DisplayName
        {
            get { return _navItem.DisplayName; }
        }
        public IDictionary<string, object> RouteValues
        {
            get { return _navItem.RouteValues; }
        }
//        public IPermissionDependency PermissionDependency { get; }
       // public IFeatureDependency FeatureDependency { get; }
        public bool RequiresAuthentication
        {
            get { return _navItem.RequiresAuthentication; }
        }


        public Permission[] HasPermissions { set; get; }
     
        public bool IsLeaf
        {
            get { return _navItem.IsLeaf; }
        }
        public object CustomData {
            get { return _navItem.CustomData; }
        }
        public bool IsVisible { get; set; }
    
        
        /// <summary>
        /// The URL to navigate when this menu item is selected. Optional.
        /// </summary>
        public string Url { get { return _navItem.Url; }  }

        public Permission[] RequirePermissions { get { return _navItem.RequirePermissions; }  }
        public UserNavigationItem(WebNavigationItemDefinition navItem)  
        {
            _navItem = navItem;
            HasPermissions = navItem.HasPermissions != null ? navItem.HasPermissions : new Permission[]{};
            Items = new List<UserNavigationItem>();
            Order = 0;
        }
        
        public IList<UserNavigationItem> Items { get; }
    }
}