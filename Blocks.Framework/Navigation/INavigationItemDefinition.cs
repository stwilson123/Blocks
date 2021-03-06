﻿using System.Collections.Generic;
using Abp.Application.Features;
using Blocks.Framework.Localization;
using Blocks.Framework.Security.Authorization.Permission;

namespace Blocks.Framework.Navigation
{
    public interface INavigationItemDefinition : INavigation
    {
    
        /// <summary>
        /// Display name of the menu item. Required.
        /// </summary>
        ILocalizableString DisplayName { get; }
 
 
        /// <summary>
        /// This can be set to true if only authenticated users should see this menu item.
        /// </summary>
        bool RequiresAuthentication { get; }

        Permission[]  HasPermissions { get; }

//        /// <summary>
//        /// Returns true if this menu item has no child <see cref="Items"/>.
//        /// </summary>
//        bool IsLeaf { get; }

        /// <summary>
        /// Can be used to store a custom object related to this menu item. Optional.
        /// </summary>
        object CustomData { get; }

        /// <summary>
        /// Can be used to show/hide a menu item.
        /// </summary>
        bool IsVisible { get; set; }

//        /// <summary>
//        /// Sub items of this menu item. Optional.
//        /// </summary>
//        IList<INavigationItemDefinition> Items { get; }
//
//        /// <summary>
//        /// Adds a <see cref="NavigationItemDefinition"/> to <see cref="NavigationItemDefinition.Items"/>.
//        /// </summary>
//        /// <param name="navItem"><see cref="NavigationItemDefinition"/> to be added</param>
//        /// <returns>This <see cref="NavigationItemDefinition"/> object</returns>
//        INavigationItemDefinition AddItem(INavigationItemDefinition navItem);


        string uId { get; }

        /// <summary>
        /// 0 is backend navigation,1 is fontend navigation
        /// </summary>
        int NavigationType { get; set; }
    }
}