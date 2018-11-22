using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public interface IPermissionDependencyContext
    {
        
        /// <summary>
        /// The user which requires permission. Can be null if no user.
        /// </summary>
        IUserIdentifier User { get; }

        IUserManager UserManager { get; }



    }
}