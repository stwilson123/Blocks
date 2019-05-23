
using System;
using System.Linq;
using Abp.Runtime.Security;

namespace Blocks.Framework.Security
{
    public class DefaultUserContext : IUserContext
    {
        private IPrincipalAccessor _principalAccessor;

        public DefaultUserContext(IPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        public IUserIdentifier GetCurrentUser()
        {
            var userIdClaim = _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                return null;
            }
            
            var userNameClaim = _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.UserName);
            return new UserIdentifier(userIdClaim.Value,null, userNameClaim.Value);
        }
    }
}