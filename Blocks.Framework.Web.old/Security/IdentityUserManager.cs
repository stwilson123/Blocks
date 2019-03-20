using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Blocks.Framework.Web.Security
{
    public class IdentityUserManager : Microsoft.AspNet.Identity.UserManager<IdentityUser>, Domain.Service.IDomainService
    {
        public IdentityUserManager(IdentityUserStore store) : base(store)
        {
        }
    }
}
