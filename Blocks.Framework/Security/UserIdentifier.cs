using System;
using System.Collections;
using System.Collections.Generic;

namespace Blocks.Framework.Security
{
    /// <summary>
    /// Used to identify a user.
    /// </summary>
    [Serializable]
    public class UserIdentifier : IUserIdentifier
    {
        public string TenantId { get; }
        public string UserId { get; }

        public string UserAccount { get; }

        public IEnumerable<string> RoleIds { get; }

        public UserIdentifier(string userId, string tenantId,string userAccount,IEnumerable<string> roleIds )
        {
            UserId = userId;
            TenantId = tenantId;
            UserAccount = userAccount;
            RoleIds = roleIds;
        }

  
        public static UserIdentifier CreateNull()
        {
            return new UserIdentifier("TestId", "", "TestName",new List<string>());
        }
    }
}