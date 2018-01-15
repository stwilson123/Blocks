using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blocks.Framework.DBORM.Linq
{
    public interface ILinqQueryable<TEntity>  where TEntity : Abp.Domain.Entities.Entity<Guid>
    {
        IQueryable<TEntity> iQuerable {  get; }
        
        
        ILinqQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        
        List<TEntity> SelectToList(Expression<Func<TEntity, dynamic>> selector);
    }
}