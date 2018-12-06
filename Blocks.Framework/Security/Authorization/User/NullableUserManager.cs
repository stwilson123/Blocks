using System.Threading.Tasks;
using Blocks.Framework.NullObject;
using Blocks.Framework.Security.Authorization.Permission;

namespace Blocks.Framework.Security.Authorization.User
{

    public class NullableUserManager  //TODO NullObject//: IUserManager,INullObject
    {
        //public Task<bool> IsGrantedAsync(IUserIdentifier user, string permission)
        //{
        //    return Task.FromResult(true);
        //}

        public Task<bool> IsGrantedAsync(IUserIdentifier user, Permission.Permission permission)
        {
            return Task.FromResult(true);
        }
    }
}