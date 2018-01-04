using System.Reflection;
using Abp.Modules;
using Abp.TestBase;
using Abp.AutoMapper;
using Blocks;

namespace EntityFramework.Test
{
    [DependsOn(
        typeof(Blocks.Framework.DBORM.BlocksFrameworkDBORMModule),
        typeof(AbpTestBaseModule),
        typeof(BlocksDataModule),
        typeof(AbpAutoMapperModule))]
    public class TestModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}
