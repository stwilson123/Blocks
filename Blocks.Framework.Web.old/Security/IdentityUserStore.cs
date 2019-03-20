using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Web.Security
{
    public class IdentityUserStore : Microsoft.AspNet.Identity.IUserStore<IdentityUser>, Ioc.Dependency.ITransientDependency
    {
        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
