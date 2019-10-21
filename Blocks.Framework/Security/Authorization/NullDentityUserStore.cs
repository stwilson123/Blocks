using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Security.Authorization
{
    public class NullDentityUserStore : IDentityUserStore
    {
        public UserIdentifier GetUser(string UserAccount)
        {
            return UserIdentifier.CreateNull();
        }

        public void CheckUserStatus(IUserIdentifier userIdentifier)
        {
            
        }
    }
}
