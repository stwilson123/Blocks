using System.Threading.Tasks;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Core.Security.Authorization
{
    public class RoleAuthorizationService : IAuthorizationService
    {
        private IUserManager _UserManager;

        public RoleAuthorizationService(IUserManager userManager)
        {
            _UserManager = userManager;
        }

        public Task<bool> TryCheckAccess(Blocks.Framework.Security.Authorization.Permission.Permission permission, IUserIdentifier user)
        {
             
            return  permission.PermissionDependency.IsSatisfiedAsync(new PermissionDependencyContext(user,_UserManager));
        }

//        public void CheckAccess(Permission permission, IUserIdentifier user)
//        {
//            if(TryCheckAccess(permission,user))
//                throw new BlocksAuthorizeException(StringLocal.Format("Current user don't have {0} permission.", permission.DisplayName.ToString()));
//        }
    }
}