using System.Linq;
using System.Reflection;
using Abp.TestBase;
using Blocks.Core.Navigation.Services;
using Blocks.Core.Security;
using Blocks.Core.Security.Authorization;
using Blocks.Framework.ApplicationServices.Controller.Factory;
using Blocks.Framework.Ioc;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.User;
using Blocks.Framework.Utility.Extensions;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Controllers.Factory;
using Blocks.Framework.Web.Mvc.Controllers.Manager;
using Blocks.Framework.Web.Navigation.Filters;
using Castle.MicroKernel.Registration;
using Xunit; 
namespace Blocks.Core.Test.Navigation.Services
{
    public class DefaultUserNavigationManagerTest: AbpIntegratedTestBase<TestNavigationModule>
    {
        public DefaultUserNavigationManagerTest()
        {
            LocalIocManager.Register<IUserNavigationManager,DefaultUserNavigationManager>();
            LocalIocManager.Register<IAuthorizationService,RoleAuthorizationService>();
            LocalIocManager.Register<IUserManager,NullUserMananger>();
            
            IDefaultControllerBuilderFactory factory = new MvcControllerBuilderFactory(LocalIocManager);
            var servicePrefix = TestModule.ModuleName.ToCamelCase();
            var servicesType = LocalIocManager.IocContainer.Kernel.GetHandlers().SelectMany(t => t.Services).Where(t => t.Assembly == Assembly.GetExecutingAssembly());
            factory.ForAll<BlocksWebMvcController>(servicePrefix,servicesType).Build();
        }
        [Fact]
        public async  void TestGetMenus()
        {
             var menuItems = await LocalIocManager.Resolve<IUserNavigationManager>().GetMenusAsync(null);
        }
    }
}