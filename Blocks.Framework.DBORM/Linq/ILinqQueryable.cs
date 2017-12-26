using System;
using System.Linq;

namespace Blocks.Framework.DBORM.Linq
{
    public interface ILinqQueryable<TEntity>  where TEntity : Abp.Domain.Entities.Entity<Guid>
    {
        IQueryable<TEntity> iQuerable {  get; }
        
    }
}