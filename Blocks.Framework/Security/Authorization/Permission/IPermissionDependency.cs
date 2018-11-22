using System.Threading.Tasks;

namespace Blocks.Framework.Security.Authorization.Permission
{
    /// <summary>
    /// Defines interface to check a dependency.
    /// </summary>
    public interface IPermissionDependency
    {
        /// <summary>
        /// Checks if permission dependency is satisfied.
        /// </summary>
        /// <param name="context">Context.</param>
        Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context);
    }

   
}