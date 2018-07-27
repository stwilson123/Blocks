using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.TestBase;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc;
using Blocks.Framework.Web.Mvc.Controllers;
using Castle.MicroKernel.Registration;

namespace Blocks.Core.Test
{
    [DependsOn(
        typeof(AbpTestBaseModule),
        typeof(AbpAutoMapperModule)
      
    )]
    public class TestModule : AbpModule
    {
        public const string ModuleName = "TestModule";
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new DependencyConventionalRegistrar(IocManager));
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar(new List<ExtensionDescriptor>() { new ExtensionDescriptor() {
                     Id = Assembly.GetExecutingAssembly().GetName().Name,Name = ModuleName
            } }));

            //            Configuration.Settings.Providers.Add<GlobalSettingProvider>();

        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IConfiguration>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Select((t, baseTypes) => t.GetInterfaces().Where(bType => typeof(IConfiguration) != bType && typeof(IConfiguration).IsAssignableFrom(bType)))
                    .LifestyleTransient()
            );
        }
    }
}