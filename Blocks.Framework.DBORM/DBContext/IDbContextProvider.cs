using System.Data.Entity;
using Abp.MultiTenancy;

namespace Blocks.Framework.DBORM.DBContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext> 
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();

        TDbContext GetDbContext(MultiTenancySides? multiTenancySide );
    }
}