using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blocks.Framework.DBORM.Linq
{
    public interface IDbLinqQueryable<TEntity> : IDbLinqQueryable where TEntity : Blocks.Framework.Data.Entity.Entity
    {
        IQueryable iQuerable {  get; }
        
        IDbLinqQueryable<TEntity> Where(LambdaExpression predicate);

        List<dynamic> SelectToList(LambdaExpression selector);

        IDbLinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible;

        IDbLinqQueryable<TEntity> LeftJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible;
        
        IDbLinqQueryable<TEntity> Take(int count);

        IDbLinqQueryable<TEntity> Skip(int count);

        IDbLinqQueryable<TEntity> OrderBy<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector);

        IDbLinqQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        PageList<dynamic> Paging(LambdaExpression selector,Page page);
         
        
        long Count();
        string ToString(); 
    }

    public interface IDbLinqQueryable
    {

    }
}