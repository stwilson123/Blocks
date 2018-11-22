using System.Threading.Tasks;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security.Authorization.User
{
    public interface IUserManager : ITransientDependency
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        Task<bool> IsGrantedAsync(IUserIdentifier user, string permission);
        
    }
}