using System.Collections.Generic;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public interface IPermissionManager
    {


        void Initialize();
        
        void InitializeRolePermission(string RoleId);

        IDictionary<string,IList<IPermission>> GetAllPermissions();

    }
}