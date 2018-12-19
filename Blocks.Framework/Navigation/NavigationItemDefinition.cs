using System.Collections.Generic;
using Abp;
using Blocks.Framework.Localization;
using Blocks.Framework.Security.Authorization.Permission;

namespace Blocks.Framework.Navigation
{
   

    /// <summary>
    /// Represents an item in a <see cref="NavigationItemDefinition"/>.
    /// </summary>
    public class NavigationItemDefinition : INavigationItemDefinition
    {
        /// <summary>
        /// Unique name of the menu item in the application. 
        /// Can be used to find this menu item later.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Display name of the menu item. Required.
        /// </summary>
        public ILocalizableString DisplayName { get; internal set; }


        
        public IDictionary<string,object> RouteValues { get; internal set; }

//        /// <summary>
//        /// A permission dependency. Only users that can satisfy this permission dependency can see this menu item.
//        /// Optional.
//        /// </summary>
//        public IPermissionDependency PermissionDependency { get; internal  set; }

//        /// <summary>
//        /// A feature dependency.
//        /// Optional.
//        /// </summary>
//        public IFeatureDependency FeatureDependency { get; internal set; }

        /// <summary>
        /// This can be set to true if only authenticated users should see this menu item.
        /// </summary>
        public bool RequiresAuthentication { get; internal set; }


        public Permission[] HasPermissions { get; set; }
//        /// <summary>
//        /// Returns true if this menu item has no child <see cref="Items"/>.
//        /// </summary>
//        public bool IsLeaf => Items.IsNullOrEmpty();

//        /// <summary>
//        /// Target of the menu item. Can be "_blank", "_self", "_parent", "_top" or a frame name.
//        /// </summary>
//        public string Target { get; set; }

        /// <summary>
        /// Can be used to store a custom object related to this menu item. Optional.
        /// </summary>
        public object CustomData { get; internal set; }
 

        /// <summary>
        /// Can be used to show/hide a menu item.
        /// </summary>
        public bool IsVisible { get; set; }
     //   public string ExtensionName { get; set; }

        //        /// <summary>
        //        /// Sub items of this menu item. Optional.
        //        /// </summary>
        //        public virtual IList<INavigationItemDefinition> Items { get; }

        internal NavigationItemDefinition(bool isVisible = true)
        {
            IsVisible = isVisible;
        }
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="icon"></param>
        /// <param name="url"></param>
        /// <param name="requiresAuthentication"></param>
        /// <param name="PermissionNames">This parameter is obsolete. Use <paramref name="permissionDependency"/> instead!</param>
        /// <param name="order"></param>
        /// <param name="customData"></param>
        /// <param name="featureDependency"></param>
        /// <param name="target"></param>
        /// <param name="isEnabled"></param>
        /// <param name="isVisible"></param>
        /// <param name="permissionDependency"></param>
        public NavigationItemDefinition(
            string name,
            ILocalizableString displayName,
//            string url = null,
            bool requiresAuthentication = false,
            Permission[] permissionNames = null,
            object customData = null,
            //     IFeatureDependency featureDependency = null,
        //    bool isEnabled = true,
            bool isVisible = true ,
             IPermissionDependency permissionDependency = null
        )
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(displayName, nameof(displayName));

            Name = name;
            DisplayName = displayName;
            RequiresAuthentication = requiresAuthentication;
            HasPermissions = permissionNames;
            //   RequiredPermissionName = HasPermissions;
            CustomData = customData;
            // FeatureDependency = featureDependency;
         //   IsEnabled = isEnabled;
            IsVisible = isVisible;
            //  PermissionDependency = permissionDependency;
//            Items = new List<INavigationItemDefinition>();
            
        }

//        /// <summary>
//        /// Adds a <see cref="NavigationItemDefinition"/> to <see cref="Items"/>.
//        /// </summary>
//        /// <param name="navItem"><see cref="NavigationItemDefinition"/> to be added</param>
//        /// <returns>This <see cref="NavigationItemDefinition"/> object</returns>
//        public INavigationItemDefinition AddItem(INavigationItemDefinition navItem)
//        {
//            Items.Add(navItem);
//            return this;
//        }
    }
}