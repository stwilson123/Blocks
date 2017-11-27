using Abp.Authorization;
using Blocks.Authorization.Roles;
using Blocks.Authorization.Users;

namespace Blocks.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
