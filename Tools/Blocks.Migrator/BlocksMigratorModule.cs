using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Blocks.EntityFramework;

namespace Blocks.Migrator
{
    [DependsOn(typeof(BlocksDataModule))]
    public class BlocksMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<BlocksDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}