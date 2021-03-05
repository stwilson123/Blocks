using Microsoft.EntityFrameworkCore;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Blocks.Framework.DBORM.DBContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider 
       
    {
        TDbContext GetDbContext<TDbContext, TEntity>()  where TDbContext : DbContext;

        TDbContext GetDbContext<TDbContext, TEntity>(MultiTenancySides? multiTenancySide) where TDbContext : DbContext;
    }
}