using Blocks.Framework.Event;

namespace Blocks.Framework.Security.Authorization.Permission.Event
{
    public class PermissionChangeEventHandler : IDomainEventHandler<PermissionChangeEventData>
    {
        private readonly IPermissionManager _permissionManager;

        public PermissionChangeEventHandler(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }
        public void HandleEvent(PermissionChangeEventData eventData)
        {
            _permissionManager.InitializeRolePermission(eventData.roleId);
        }
    }
}