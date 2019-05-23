using Blocks.Framework.Ioc;
using Blocks.Framework.Security.Authorization.Permission;

namespace Blocks.Framework.Security
{
    public class SecurityModule : BlocksModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IPermissionManager,PermissionManager>();
        }

        public override void PostInitialize()
        {
            
            IocManager.Resolve<IPermissionManager>().Initialize();
            IocManager.Resolve<IPermissionManager>().InitializeRolePermission("*");
            
        }
    }
}