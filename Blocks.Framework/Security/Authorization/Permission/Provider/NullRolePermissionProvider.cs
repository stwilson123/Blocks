using System.Collections.Generic;

namespace Blocks.Framework.Security.Authorization.Permission.Provider
{
    public class NullRolePermissionProvider : IRolePermissionProvider
    {
        public IDictionary<string, IList<string>> GetPermissions(string roleId = "*")
        {
            return  new Dictionary<string, IList<string>>();
        }
    }
}