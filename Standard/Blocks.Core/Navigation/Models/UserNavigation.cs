using System.Collections.Generic;

namespace Blocks.Core.Navigation.Models
{
    public class UserNavigation
    {
        public UserNavigation(string name, IList<UserNavigationItem> items)
        {
            Items = items;
            Name = name;
        }

        /// <summary>
        /// Sub items of this menu item. Optional.
        /// </summary>
        public virtual IList<UserNavigationItem> Items { get; internal set; }
        
        /// <summary>
        /// Unique name of the navigation in the application. Required.
        /// </summary>
        public string Name { get; private set; }
    }
}