using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.TestBase;
using Blocks;
using Blocks.Framework.DBORM;
using Castle.MicroKernel.Registration;
using Blocks.Framework.Configurations;
using System.Linq;

namespace EntityFramework.Test
{
    [DependsOn(
        typeof(AbpTestBaseModule),
        typeof(AbpAutoMapperModule),
        typeof(BlocksDataModule),
        typeof(BlocksFrameworkDBORMModule)
    )]
    public class TestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Settings.Providers.Add<GlobalSettingProvider>();

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