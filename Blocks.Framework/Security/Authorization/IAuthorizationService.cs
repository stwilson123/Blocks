using System.Threading.Tasks;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Framework.Security.Authorization
{
    public interface IAuthorizationService : ITransientDependency
    {
        Task<bool> TryCheckAccess(Permission.Permission permission, IUserIdentifier user);
        
//        void CheckAccess(Permission.Permission permission, IUserIdentifier user);
    }
}