using System.Threading.Tasks;
using Blocks.Framework.NullObject;

namespace Blocks.Framework.Security.Authorization.User
{
    public class NullableUserManager : IUserManager,INullObject
    {
        public Task<bool> IsGrantedAsync(IUserIdentifier user, string permission)
        {
            return Task.FromResult(true);
        }
    }
}