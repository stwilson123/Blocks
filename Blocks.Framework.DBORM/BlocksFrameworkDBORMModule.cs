using System;
using System.Data.Entity;
using System.Reflection;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Orm;
using Abp.Reflection;
using Blocks.Framework.Caching;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.Environment;
using Blocks.Framework.Localization;
using Blocks.Framework.Modules;
using Castle.MicroKernel.Registration;
using AbpDbContext = Abp.EntityFramework.AbpDbContext;

namespace Blocks.Framework.DBORM
{
    [DependsOn(typeof(BlocksFrameworkModule))]
    public class BlocksFrameworkDBORMModule: AbpModule
    {
         
        private readonly ITypeFinder _typeFinder;

        public BlocksFrameworkDBORMModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
         
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component.For(typeof(DBContext.IDbContextProvider<>))
                    .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                    .LifestyleTransient()
            );
            IocManager.IocContainer.Register(Component.For(typeof(BlocksDbContext<>)));
                
             
            RegisterGenericRepositoriesAndMatchDbContexes();
        }
        
        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsPublic &&
                           !typeInfo.IsAbstract &&
                           typeInfo.IsClass &&
                           typeof(BaseBlocksDbContext).IsAssignableFrom(type);
                });

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from AbpDbContext.");
                return;
            }

            using (IScopedIocResolver scope = IocManager.CreateScope())
            {
                foreach (var dbContextType in dbContextTypes)
                {
                    Logger.Debug("Registering DbContext: " + dbContextType.AssemblyQualifiedName);

                    scope.Resolve<IEfGenericRepositoryRegistrar>().RegisterForDbContext(dbContextType, IocManager, Blocks.Framework.DBORM.Repository.EfAutoRepositoryTypes.Default);

                    IocManager.IocContainer.Register(
                        Component.For<ISecondaryOrmRegistrar>()
                            .Named(Guid.NewGuid().ToString("N"))
                            .Instance(new EfCoreBasedSecondaryOrmRegistrar(dbContextType, scope.Resolve<IDbContextEntityFinder>()))
                            .LifestyleTransient()
                    );
                }

                scope.Resolve<IDbContextTypeMatcher>().Populate(dbContextTypes);
            }
        }
    }
}