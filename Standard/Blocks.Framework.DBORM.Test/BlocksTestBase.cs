using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.TestBase;
using Blocks.Framework.DBORM.DBContext;
using Castle.MicroKernel.Registration;
//using Effort;
//using EntityFramework.DynamicFilters;

namespace EntityFramework.Test
{
    public abstract class BlocksTestBase : AbpIntegratedTestBase<TestModule>
    {
        private DbConnection _hostDb;
        private Dictionary<int, DbConnection> _tenantDbs; //only used for db per tenant architecture

        protected BlocksTestBase()
        {
            //Seed initial data for host
            AbpSession.TenantId = null;
//            UsingDbContext(context =>
//            {
////                new InitialHostDbBuilder(context).Create();
////                new DefaultTenantCreator(context).Create();
//            });

//            //Seed initial data for default tenant
//            AbpSession.TenantId = 1;
//            UsingDbContext(context =>
//            {
////                new TenantRoleAndUserBuilder(context, 1).Create();
//            });

//            LoginAsDefaultTenantAdmin();
        }

        protected override void PreInitialize()
        {
            base.PreInitialize();

            /* You can switch database architecture here: */
           // UseSingleDatabase();
            //UseDatabasePerTenant();
        }

        /* Uses single database for host and all tenants.
         */
        //private void UseSingleDatabase()
        //{
        //    _hostDb = DbConnectionFactory.CreateTransient();

        //    LocalIocManager.IocContainer.Register(
        //        Component.For<DbConnection>()
        //            .UsingFactoryMethod(() => _hostDb)
        //            .LifestyleSingleton()
        //        );
        //}

        /* Uses single database for host and Default tenant,
         * but dedicated databases for all other tenants.
         */
        //private void UseDatabasePerTenant()
        //{
        //    _hostDb = DbConnectionFactory.CreateTransient();
        //    _tenantDbs = new Dictionary<int, DbConnection>();

        //    LocalIocManager.IocContainer.Register(
        //        Component.For<DbConnection>()
        //            .UsingFactoryMethod((kernel) =>
        //            {
        //                lock (_tenantDbs)
        //                {
        //                    var currentUow = kernel.Resolve<ICurrentUnitOfWorkProvider>().Current;
        //                    var abpSession = kernel.Resolve<IAbpSession>();

        //                    var tenantId = currentUow != null ? currentUow.GetTenantId() : abpSession.TenantId;

        //                    if (tenantId == null || tenantId == 1) //host and default tenant are stored in host db
        //                    {
        //                        return _hostDb;
        //                    }

        //                    if (!_tenantDbs.ContainsKey(tenantId.Value))
        //                    {
        //                        _tenantDbs[tenantId.Value] = DbConnectionFactory.CreateTransient();
        //                    }

        //                    return _tenantDbs[tenantId.Value];
        //                }
        //            }, true)
        //            .LifestyleTransient()
        //        );
        //}

        #region UsingDbContext

        protected IDisposable UsingTenantId(int? tenantId)
        {
            var previousTenantId = AbpSession.TenantId;
            AbpSession.TenantId = tenantId;
            return new DisposeAction(() => AbpSession.TenantId = previousTenantId);
        }

        //protected void UsingDbContext(Action<BaseBlocksDbContext> action)
        //{
        //    UsingDbContext(AbpSession.TenantId, action);
        //}

        //protected Task UsingDbContextAsync(Func<BaseBlocksDbContext, Task> action)
        //{
        //    return UsingDbContextAsync(AbpSession.TenantId, action);
        //}

        //protected T UsingDbContext<T>(Func<BaseBlocksDbContext, T> func)
        //{
        //    return UsingDbContext(AbpSession.TenantId, func);
        //}

        //protected Task<T> UsingDbContextAsync<T>(Func<BaseBlocksDbContext, Task<T>> func)
        //{
        //    return UsingDbContextAsync(AbpSession.TenantId, func);
        //}

        //protected void UsingDbContext(int? tenantId, Action<BaseBlocksDbContext> action)
        //{
        //    using (UsingTenantId(tenantId))
        //    {
        //        using (var context = LocalIocManager.Resolve<BaseBlocksDbContext>())
        //        {
        //         //   context.DisableAllFilters();
        //            action(context);
        //            context.SaveChanges();
        //        }
        //    }
        //}

        //protected async Task UsingDbContextAsync(int? tenantId, Func<BaseBlocksDbContext, Task> action)
        //{
        //    using (UsingTenantId(tenantId))
        //    {
        //        using (var context = LocalIocManager.Resolve<BaseBlocksDbContext>())
        //        {
        //            //context.DisableAllFilters();
        //            await action(context);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //}

        protected T UsingDbContext<T>(int? tenantId, Func<BaseBlocksDbContext, T> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<BaseBlocksDbContext>())
                {
                    //context.DisableAllFilters();
                    result = func(context);
                    context.SaveChanges();
                }
            }

            return result;
        }

        //protected async Task<T> UsingDbContextAsync<T>(int? tenantId, Func<BaseBlocksDbContext, Task<T>> func)
        //{
        //    T result;

        //    using (UsingTenantId(tenantId))
        //    {
        //        using (var context = LocalIocManager.Resolve<BaseBlocksDbContext>())
        //        {
        //            //.DisableAllFilters();
        //            result = await func(context);
        //            await context.SaveChangesAsync();
        //        }
        //    }

        //    return result;
        //}

        #endregion
 
    }
}