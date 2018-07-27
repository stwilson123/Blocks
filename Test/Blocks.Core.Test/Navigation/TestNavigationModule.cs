using System.Linq;
using System.Reflection;
using Abp.Modules;
using Blocks.Framework.ApplicationServices.Controller.Factory;
using Blocks.Framework.Ioc;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Utility.Extensions;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Controllers.Factory;
using Blocks.Framework.Web.Mvc.Controllers.Manager;
using Blocks.Framework.Web.Navigation.Filters;

namespace Blocks.Core.Test.Navigation
{
    [DependsOn(typeof(TestModule))]
    [DependsOn(typeof(NavigationModule))]
    public class TestNavigationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.Register<INavigationFilter,MvcNavigationFilter>();
            IocManager.Register<MvcControllerManager>();
            IDefaultControllerBuilderFactory factory = new MvcControllerBuilderFactory(IocManager);
            var servicePrefix = TestModule.ModuleName.ToCamelCase();
            var servicesType = IocManager.IocContainer.Kernel.GetHandlers().SelectMany(t => t.Services).Where(t => t.Assembly == Assembly.GetExecutingAssembly());
            factory.ForAll<BlocksWebMvcController>(servicePrefix,servicesType).Build();
        }
    }
}