using Blocks.Core.Navigation.Models;
using Blocks.Framework.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.LayoutModule.Extensions.Event
{
    public  class MenusSortEventData : DomainEventData
    {
        public UserNavigation userNavigation { get; set; }
    }
}
