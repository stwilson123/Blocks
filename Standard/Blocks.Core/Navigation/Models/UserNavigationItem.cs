using System.Collections.Generic;
using Abp.Localization;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Types;
using Blocks.Framework.Web.Navigation;

namespace Blocks.Core.Navigation.Models
{
    public class UserNavigationItem : INavigationItemDefinition
    {
        private WebNavigationItemDefinition _navItem;

        public string Icon { get; set; } 
        public long Order { get; set; }

        public string Name { get; private set; }
     
        public Framework.Localization.ILocalizableString DisplayName
        {
            get; private set; 
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

        public string uId => this.GetUniqueId();

        public UserNavigationItem()
        {
            
        }
        public UserNavigationItem(WebNavigationItemDefinition navItem)
        {

            Init(navItem);
        }
        public UserNavigationItem(string Name, Framework.Localization.ILocalizableString DisplayName)
        {
            Init(new WebNavigationItemDefinition(Name, DisplayName, ""));
        }
        private void Init(WebNavigationItemDefinition navItem)
        {
            _navItem = navItem;
            Name = _navItem.Name;
            DisplayName = _navItem.DisplayName;
            HasPermissions = navItem.HasPermissions != null ? navItem.HasPermissions : new Permission[] { };
            Items = new List<UserNavigationItem>();
            IsVisible = navItem.IsVisible;
            Order = 0;
        }

      
        
        public IList<UserNavigationItem> Items { get; set; }
        public string ExtensionName { get ; set; }
    }
}