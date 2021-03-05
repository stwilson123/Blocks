using Abp.MultiTenancy;
using Blocks.Framework.Domain.Uow;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.DBContext
{
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// Gets a DbContext as a part of active unit of work.
        /// This method can be called when current unit of work is an <see cref="EfUnitOfWork"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
        /// <param name="unitOfWork">Current (active) unit of work</param>
        public static TDbContext GetDbContext<TDbContext, TEntity>(this IActiveUnitOfWork unitOfWork)
            where TDbContext : DbContext
        {
            return GetDbContext<TDbContext, TEntity>(unitOfWork, null);
        }

        public static TDbContext GetDbContext<TDbContext, TEntity>(this IActiveUnitOfWork unitOfWork, MultiTenancySides? multiTenancySide)
            where TDbContext : DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, nameof(unitOfWork));
            }

            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext, TEntity>(multiTenancySide);
        }
    }
}
