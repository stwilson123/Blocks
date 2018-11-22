using System;
using System.Threading.Tasks;

namespace Blocks.Framework.Authorization
{
    public class DefaultPermissionDependency : IPermissionDependency
    {
        /// <summary>
        /// A list of permissions to be checked if they are granted.
        /// </summary>
        public string[] Permissions { get; set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Permissions"/> must be granted.
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// Default: false.
        /// </summary>
        public bool RequiresAll { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPermissionDependency"/> class.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        public DefaultPermissionDependency(params string[] permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPermissionDependency"/> class.
        /// </summary>
        /// <param name="requiresAll">
        /// If this is set to true, all of the <see cref="Permissions"/> must be granted.
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// </param>
        /// <param name="permissions">The permissions.</param>
        public DefaultPermissionDependency(bool requiresAll, params string[] permissions)
            : this(permissions)
        {
            RequiresAll = requiresAll;
        }

        /// <inheritdoc/>
        public Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context)
        {
            throw  new NotImplementedException();
//            return context.User != null
//                ? context.PermissionChecker.IsGrantedAsync(context.User, RequiresAll, Permissions)
//                : context.PermissionChecker.IsGrantedAsync(RequiresAll, Permissions);
        }
       
    }
}