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
        
        public UserIdentifier(string userId, string tenantId)
        {
            UserId = userId;
            TenantId = tenantId;
        }

  
    }
}