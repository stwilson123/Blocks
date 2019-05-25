using System.Collections.Generic;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security.Authorization.Permission.Provider
{
    public interface IRolePermissionProvider : ITransientDependency
    {
        IDictionary<string,IList<string>> GetPermissions(string roleId = "*");
    }
}