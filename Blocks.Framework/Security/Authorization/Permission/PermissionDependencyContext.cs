using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public class PermissionDependencyContext : IPermissionDependencyContext
    {
        public IUserIdentifier User { get; }
        public IUserManager UserManager { get; }

        public PermissionDependencyContext(IUserIdentifier user, IUserManager userManager)
        {
            User = user;
            UserManager = userManager;
        }
        
    }
}