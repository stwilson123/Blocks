using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Blocks.EntityFramework;

namespace Blocks
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(BlocksCoreModule))]
    public class BlocksDataModule : AbpModule
    {
        public override void PreInitialize()
        {   
            
            
            Database.SetInitializer(new CreateDatabaseIfNotExists<BlocksDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
