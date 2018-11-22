using System.Collections.Generic;
using Blocks.Core.Navigation.Models;

namespace Blocks.LayoutModule.ViewModels
{
    public class Menus : UserNavigation
    {
        public Menus(string name, IList<UserNavigationItem> items) : base(name, items)
        {
            
        }
        
      
        public string ActiveMenuItemName { get; set; }
    }
}