using Blocks.Core.Navigation.Models;
using Blocks.Framework.Event;
 

namespace Blocks.LayoutModule.ExtensionsModule.Event
{
    public class MenusSortEventData : DomainEventData
    {
        public UserNavigation userNavigation { get; set; }
    }
}
