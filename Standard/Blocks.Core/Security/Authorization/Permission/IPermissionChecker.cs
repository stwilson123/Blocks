using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Core.Security.Authorization.Permission
{
    public interface IPermissionCheck : ITransientDependency
    {
        Task<bool> IsGrantedAsync(IUserIdentifier user, IPermission permission);
         
    }
}
