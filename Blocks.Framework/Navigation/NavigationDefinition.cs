using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.Localization;
using Blocks.Framework.Types;

namespace Blocks.Framework.Navigation
{
   

    public class NavigationDefinition : INavigationDefinition
    {
        /// <summary>
        /// Unique name of the navigation in the application. Required.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the menu. Required.
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// Can be used to store a custom object related to this menu. Optional.
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Navigation items (first level).
        /// </summary>
        public IList<INavigationItemDefinition> Items { get; set; }

        /// <summary>
        /// Creates a new <see cref="NavigationDefinition"/> object.
        /// </summary>
        /// <param name="name">Unique name of the navigation</param>
        /// <param name="displayName">Display name of the navigation</param>
        /// <param name="customData">Can be used to store a custom object related to this navigation.</param>
        public NavigationDefinition(string name, ILocalizableString displayName, object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
             
                throw new ArgumentNullException("name", "navigation name can not be empty or null.");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("displayName", "Display name of the navigation can not be null.");
            }

            Name = name;
            DisplayName = displayName;
            CustomData = customData;

            Items = new List<INavigationItemDefinition>();
        }

        /// <summary>
        /// Creates a new <see cref="NavigationDefinition"/> object.
        /// </summary>
        /// <param name="name">Unique name of the navigation</param>
        /// <param name="displayName">Display name of the navigation</param>
        /// <param name="customData">Can be used to store a custom object related to this navigation.</param>
        public NavigationDefinition(INavigationDefinition navigationDefinition) : this(navigationDefinition.Name,navigationDefinition.DisplayName,navigationDefinition.CustomData)
        {
            Check.NotNull(navigationDefinition, "navigationDefinition");
           
        }
        /// <summary>
        /// Adds a <see cref="NavigationDefinition"/> to <see cref="Items"/>.
        /// </summary>
        /// <param name="navItem"><see cref="NavigationDefinition"/> to be added</param>
        /// <returns>This <see cref="NavigationDefinition"/> object</returns>
        public INavigationDefinition AddItem(INavigationItemDefinition navItem)
        {
            Items.Add(navItem);
            return this;
        }
    }
}