﻿using Abp.Dependency;
using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.DBContext
{
    public class DefaultDbContextResolver : IDbContextResolver, Ioc.Dependency.ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly IDbContextTypeMatcher _dbContextTypeMatcher;

        public DefaultDbContextResolver(IIocResolver iocResolver, IDbContextTypeMatcher dbContextTypeMatcher)
        {
            _iocResolver = iocResolver;
            _dbContextTypeMatcher = dbContextTypeMatcher;
        }

        public TDbContext Resolve<TDbContext>(string connectionString)
            where TDbContext : DbContext
        {
            var dbContextType = GetConcreteType<TDbContext>();
            return (TDbContext)_iocResolver.Resolve(dbContextType, new
            {
                nameOrConnectionString = connectionString
            });
        }

        public TDbContext Resolve<TDbContext>(DbConnection existingConnection, bool contextOwnsConnection)
            where TDbContext : DbContext
        {
            var dbContextType = GetConcreteType<TDbContext>();
            return (TDbContext)_iocResolver.Resolve(dbContextType, new
            {
                existingConnection = existingConnection,
                contextOwnsConnection = contextOwnsConnection
            });
        }

        protected virtual Type GetConcreteType<TDbContext>()
        {
            var dbContextType = typeof(TDbContext);
            return !dbContextType.IsAbstract
                ? dbContextType
                : _dbContextTypeMatcher.GetConcreteType(dbContextType);
        }
    }
}
