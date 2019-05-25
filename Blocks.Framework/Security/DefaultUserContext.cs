
using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.Logging;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Claim;
using Castle.Core.Logging;

namespace Blocks.Framework.Security
{
    public class DefaultUserContext : IUserContext
    {
        private IPrincipalAccessor _principalAccessor;
        private readonly IDentityUserStore _dentityUserStore;

        private IEnumerable<string> roleIds;
        private object locker = new object();
        public readonly ILogger Log;
        public DefaultUserContext(IPrincipalAccessor principalAccessor,IDentityUserStore dentityUserStore, ILogger log)
        {
            _principalAccessor = principalAccessor;
            _dentityUserStore = dentityUserStore;
            Log = log;
        }

        public IUserIdentifier GetCurrentUser()
        {
            var userIdClaim = _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BlocksClaimTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                return null;
            }
            
            var userNameClaim = _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BlocksClaimTypes.UserName);


            lock (locker)
            {
                if (roleIds == null)
                {
                    roleIds = _dentityUserStore.GetUser(userNameClaim.Value)?.RoleIds;
                    Log.Debug($"User {userIdClaim.Value} find roleIds {string.Join(",",roleIds)}");
                }
            }
 
            return new UserIdentifier(userIdClaim.Value,null, userNameClaim.Value,roleIds);
        }
    }
}