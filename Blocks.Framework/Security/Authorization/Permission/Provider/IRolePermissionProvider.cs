using System.Collections.Generic;

namespace Blocks.Framework.Security.Authorization.Permission.Provider
{
    public interface IRolePermissionProvider
    {
        IDictionary<string,IList<string>> GetPermissions(string roleId = "*");
    }
}