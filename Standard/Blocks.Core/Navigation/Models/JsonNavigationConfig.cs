using System.Collections.Generic;

namespace Blocks.Core.Navigation.Models
{
    public class JsonNavigationConfig
    {
        public IList<NavigationItem> Items { get; set; }

        public JsonNavigationConfig()
        {
            Items = new List<NavigationItem>();
        }
    }

    public class NavigationItem
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Action { get; set; }

        public string ControllerName { get; set; }

        public string AreaName { get; set; }

        public int? NavigationType { get; set; }

        public string[] Permission { get; set; }

        public bool? Visible { get; set; }
    }
}