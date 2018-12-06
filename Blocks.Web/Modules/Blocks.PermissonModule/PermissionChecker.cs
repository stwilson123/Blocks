using Blocks.Core.Security.Authorization.Permission;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.BussnessDomainModule.MasterData
{
    public class PermissionChecker : IPermissionCheck
    {
        public Task<bool> IsGrantedAsync(IUserIdentifier user, IPermission permission)
        {
           
            return Task.FromResult(true);
        }
    }
}
