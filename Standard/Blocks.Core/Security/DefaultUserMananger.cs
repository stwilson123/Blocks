using System.Threading.Tasks;
using Abp.Dependency;
using Blocks.Core.Security.Authorization.Permission;
using Blocks.Framework.NullObject;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Core.Security
{
    public class DefaultUserManager : IUserManager
    {
        IIocManager _iocManager;
        public DefaultUserManager(IIocManager iocManager)
        {
            this._iocManager = iocManager;

        }
        //public Task<bool> IsGrantedAsync(IUserIdentifier user, string permission)
        //{
        //    return Task.FromResult(true);
        //}

        public Task<bool> IsGrantedAsync(IUserIdentifier user, Permission permission)
        {
            if(_iocManager.IsRegistered<IPermissionCheck>())
            {
                return _iocManager.Resolve<IPermissionCheck>().IsGrantedAsync(user,permission);
            }
            return Task.FromResult(true);
        }
    }
}