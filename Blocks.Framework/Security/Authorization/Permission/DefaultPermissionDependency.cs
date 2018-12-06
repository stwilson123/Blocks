using System.Linq;
using System.Threading.Tasks;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public class DefaultPermissionDependency : IPermissionDependency
    {
        /// <summary>
        /// A list of permissions to be checked if they are granted.
        /// </summary>
        public Permission[] Permissions { get; set; }


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
        public DefaultPermissionDependency(params Permission[] permissions)
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
        public DefaultPermissionDependency(bool requiresAll, params Permission[] permissions)
            : this(permissions)
        {
            RequiresAll = requiresAll;
            Permissions = permissions == null ? new Permission[0] : permissions;

        }

        /// <inheritdoc/>
        public Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context)
        {
            if (RequiresAll)
            {
               
                var permissionTask = Permissions?.Select(p =>
                context.UserManager.IsGrantedAsync(context.User, p)).ToArray();
                Task.WaitAll(permissionTask);
                return permissionTask.Any(p => p.Result == false) ? Task.FromResult(false)  : Task.FromResult(true);
            }

            foreach (var permission in Permissions?.ToArray())
            {
                return context.UserManager.IsGrantedAsync(context.User, permission);
            }
            return  Task.FromResult(true);
 
        }
       
    }
}