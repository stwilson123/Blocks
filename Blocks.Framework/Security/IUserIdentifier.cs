namespace Blocks.Framework.Security
{
    public interface IUserIdentifier
    {
        // <summary>
        /// Tenant Id. Can be null for host users.
        /// </summary>
        string TenantId { get; }

        /// <summary>
        /// Id of the user.
        /// </summary>
        string UserId { get; }
    }
}