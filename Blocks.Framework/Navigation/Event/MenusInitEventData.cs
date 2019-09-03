using Blocks.Framework.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Navigation.Event
{
    public class MenusInitEventData : DomainEventData
    {
        public IDictionary<string, INavigationItemDefinition[]>  NavigationItems { get; set; }
    }

     
}
