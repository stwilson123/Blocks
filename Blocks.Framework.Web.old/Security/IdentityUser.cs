using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Web.Security
{
   public  class IdentityUser : Microsoft.AspNet.Identity.IUser<string>
    {
        public IdentityUser(string id, string userName,IEnumerable<string> roleIds)
        {
            Id = id;
            UserName = userName;
            RoleIds = roleIds;
        }
        public string Id { get; }
        public string UserName { get; set; }
        
        public IEnumerable<string> RoleIds { get; }

    }
}
