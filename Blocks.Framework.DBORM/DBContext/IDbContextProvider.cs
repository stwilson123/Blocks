using System.Data.Entity;
using Abp.MultiTenancy;

namespace Blocks.Framework.DBORM.DBContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider 
       
    {
        TDbContext GetDbContext<TDbContext>()  where TDbContext : DbContext;

        TDbContext GetDbContext<TDbContext>(MultiTenancySides? multiTenancySide ) where TDbContext : DbContext;
    }
}