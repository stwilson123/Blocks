using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blocks.Framework.DBORM.Linq
{
    public interface ILinqQueryable<TEntity>  where TEntity : Abp.Domain.Entities.Entity<Guid>
    {
        IQueryable<TEntity> iQuerable {  get; }


        List<TEntity> SelectToList(Expression<Func<TEntity, dynamic>> selector);
    }
}