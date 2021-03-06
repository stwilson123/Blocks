﻿using System.Data.Entity;
using Abp.MultiTenancy;
using Blocks.Framework.Domain.Uow;

namespace Blocks.Framework.DBORM.DBContext
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
    public class UnitOfWorkDbContextProvider : IDbContextProvider 
       
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        

        public TDbContext GetDbContext<TDbContext>() where TDbContext : DbContext
        {
            return GetDbContext<TDbContext>(null);

        }

        public TDbContext GetDbContext<TDbContext>(MultiTenancySides? multiTenancySide) where TDbContext : DbContext
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(multiTenancySide);

        }
    }
}