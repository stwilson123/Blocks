using Blocks.Framework.Event;

namespace Blocks.Framework.Security.Authorization.Permission.Event
{
    public class PermissionChangeEventData :  DomainEventData
    {
        public string roleId { get; set; }
    }
}