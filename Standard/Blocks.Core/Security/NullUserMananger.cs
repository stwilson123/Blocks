using System.Threading.Tasks;
using Blocks.Framework.NullObject;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Core.Security
{
    public class NullUserMananger : IUserManager,INullObject
    {
        public Task<bool> IsGrantedAsync(IUserIdentifier user, string permission)
        {
            return Task.FromResult(true);
        }
    }
}