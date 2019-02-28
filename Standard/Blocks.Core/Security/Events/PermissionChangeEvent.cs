using Blocks.Framework.Event;

namespace Blocks.Core.Security.Events
{
    public class PermissionChangeEventData : DomainEventData
    {
        public string UserId { get; set; }
        
        public string ResourceKey { get; set; }
    }
}