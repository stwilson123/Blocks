using System;

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

        public UserIdentifier(string userId, string tenantId,string userAccount)
        {
            UserId = userId;
            TenantId = tenantId;
            UserAccount = userAccount;
        }

  
        public static UserIdentifier CreateNull()
        {
            return new UserIdentifier("TestId", "", "TestName");
        }
    }
}