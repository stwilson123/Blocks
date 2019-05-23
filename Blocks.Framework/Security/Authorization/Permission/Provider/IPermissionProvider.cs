using System.Collections.Generic;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security.Authorization.Permission.Provider
{
    public interface IPermissionProvider : ITransientDependency
    {
        IList<Permission> GetPermissions();
    }
}