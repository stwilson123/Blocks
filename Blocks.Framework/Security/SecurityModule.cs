using Blocks.Framework.Ioc;
using Blocks.Framework.Security.Authorization.Permission;
using Castle.MicroKernel.Registration;
using Castle.Winsdor.Aspnet.Web;

namespace Blocks.Framework.Security
{
    public class SecurityModule : BlocksModule
    {
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(Component.For<IPermissionManager, PermissionManager>().LifestyleScoped()
                    .LifestylePerWebRequest());
            //IocManager.Register<IPermissionManager,PermissionManager>();
        }

        public override void PostInitialize()
        {
            
            //IocManager.Resolve<IPermissionManager>().Initialize();
            //IocManager.Resolve<IPermissionManager>().InitializeRolePermission("*");
            
        }
    }
}