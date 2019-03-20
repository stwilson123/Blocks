using Blocks.Framework.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Navigation.Event
{
    public class MenusInitEventData : DomainEventData
    {
        public INavigationItemDefinition[] NavigationItems { get; set; }
    }

     
}
