using System.Threading.Tasks;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Framework.Security.Authorization
{
    public static class AuthorizationServiceExtension
    {
        public static async Task<bool> TryCheckAccess(this IAuthorizationService authorizationService ,Permission.Permission[] permissions, bool RequiresAuthentication,IUserIdentifier user)
        {
            if (permissions == null)
                return true;
            if (RequiresAuthentication )
            {
                foreach (var permission in permissions)
                {
                    if (!(await authorizationService.TryCheckAccess(permission, user)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (var permission in permissions)
                {
                    if (await authorizationService.TryCheckAccess(permission,user))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}