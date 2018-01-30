using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blocks.Framework.DBORM.Linq
{
    public interface IDbLinqQueryable<TEntity>  where TEntity : Blocks.Framework.Data.Entity.Entity
    {
        IQueryable iQuerable {  get; }
        
        
        IDbLinqQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IDbLinqQueryable<TEntity> Where<T>(Expression<Func<T, bool>> predicate);

        List<TEntity> SelectToList(Expression<Func<TEntity, dynamic>> selector);

        IDbLinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible;


        IDbLinqQueryable<TEntity> Take(int count);

        IDbLinqQueryable<TEntity> Skip(int count);

        string ToString();
    }
}