using System.IO;
using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Blocks.Framework.ApplicationServices.Controller.Factory;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.FileSystems;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.FileSystems.WebSite;
using Blocks.Framework.Ioc;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Utility.Extensions;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Mvc.Controllers.Factory;
using Blocks.Framework.Web.Mvc.Controllers.Manager;
using Blocks.Framework.Web.Navigation.Filters;

namespace Blocks.Framework.Web.Test.Navigation
{
    [DependsOn(typeof(TestModule))]
    [DependsOn(typeof(NavigationModule))]
    public class TestNavigationModule : AbpModule
    {
        public override void PreInitialize()
        {
            var _currentRootPath = Directory.GetCurrentDirectory();
            var currentHostingEnvironment = WebHostingEnvironment.CreateHostingEnvironment(new WebHostingEnvironment()
            {
                ContentRootPath = _currentRootPath
            });
            var _defaultVirtualPathProvider = new DefaultVirtualPathProvider(currentHostingEnvironment);

            IocManager.Register<IWebSiteFolder, WebSiteFolder>(
                (Kernel, ComponentModel, CreationContext) =>
                {
                    return new WebSiteFolder(null, _defaultVirtualPathProvider);
                }, DependencyLifeStyle.Singleton);

            IocManager.Register<ExtensionDescriptor, ExtensionDescriptor>(
                (Kernel, ComponentModel, CreationContext) =>
                {
                    return new ExtensionDescriptor() {Name = "TestNavigationModule"};
                }, DependencyLifeStyle.Singleton);
            IocManager.Register<Localizer, Localizer>(
                (Kernel, ComponentModel, CreationContext) =>
                {
                    return (text, param) => new LocalizableString("TestNavigationModule", text, param);
                }, DependencyLifeStyle.Singleton);
        }

        public override void Initialize()
        {
            IocManager.Register<INavigationFilter, MvcNavigationFilter>();
            IocManager.Register<INavigationFilter,FrontNavigationFilter>();
            IocManager.Register<MvcControllerManager>();
            IDefaultControllerBuilderFactory factory = new MvcControllerBuilderFactory(IocManager);
            var servicePrefix = TestModule.ModuleName.ToCamelCase();
            var servicesType = IocManager.IocContainer.Kernel.GetHandlers().SelectMany(t => t.Services)
                .Where(t => t.Assembly == Assembly.GetExecutingAssembly());
            factory.ForAll<BlocksWebMvcController>(servicePrefix, servicesType).Build();
        }
    }
}