using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.Linq
{
    public interface IDbLinqQueryable<TEntity> : IDbLinqQueryable where TEntity : Blocks.Framework.Data.Entity.Entity 
    {
        IQueryable iQuerable {  get; }
        
        IDbLinqQueryable<TEntity> Where(LambdaExpression predicate);

        List<dynamic> SelectToList(LambdaExpression selector);

        List<dynamic> SelectToList();


        Task<List<dynamic>> SelectToListAsync(LambdaExpression selector);

        IDbLinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TOuter : class where TInner : class;

        IDbLinqQueryable<TEntity> LeftJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector)  where TOuter : class where TInner : class;


        IDbLinqQueryable<TEntity> Take(int count);

        IDbLinqQueryable<TEntity> Skip(int count);

        IDbLinqQueryable<TEntity> OrderBy<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector) where TSource : Data.Entity.Entity;

        IDbLinqQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IDbLinqQueryable<TEntity> OrderByDescending<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector) where TSource : Data.Entity.Entity;
        IDbLinqQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);


        IDbLinqQueryable<TEntity> ThenBy<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector);


        IDbLinqQueryable<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IDbLinqQueryable<TEntity> ThenByDescending<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector);

        IDbLinqQueryable<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);


        ILinqGroupQueryable<TKey> GroupBy<TKey>(LambdaExpression keySelector);

        PageList<dynamic> Paging(LambdaExpression selector,Page page);
         
        PageList<dynamic> Paging(LambdaExpression selector,Page page,bool distinct);
        
        long Count();
        string ToString(); 
    }

    public interface ILinqGroupQueryable<TKey> 
    {

        List<dynamic> SelectToList(LambdaExpression selector);

        List<TDto> SelectToList<T1, TDto>(Expression<Func<TKey, T1, TDto>> selector) where T1 : IEnumerable<Data.Entity.Entity>;
        
        PageList<dynamic> Paging(LambdaExpression selector, Page page);

        PageList<dynamic> Paging(LambdaExpression selector, Page page, bool distinct);

        ILinqGroupQueryable<TKey> OrderByDescending<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>;

        ILinqGroupQueryable<TKey> OrderBy<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>;

        ILinqGroupQueryable<TKey> ThenByDescending<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>;

        ILinqGroupQueryable<TKey> ThenBy<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>;

        ILinqGroupQueryable<TKey> Where<T1>(Expression<Func<TKey, T1, bool>> predicate);

        ILinqGroupQueryable<TKey> Where<T1, T2>(Expression<Func<TKey, T1, T2, bool>> predicate);

        ILinqGroupQueryable<TKey> Where<T1, T2, T3>(Expression<Func<TKey, T1, T2, T3, bool>> predicate);

        ILinqGroupQueryable<TKey> Where<T1, T2, T3, T4>(Expression<Func<TKey, T1, T2, T3, T4, bool>> predicate);

        ILinqGroupQueryable<TKey> Where<T1, T2, T3, T4, T5>(Expression<Func<TKey, T1, T2, T3, T4, T5, bool>> predicate);

        ILinqGroupQueryable<TKey> Where<T1, T2, T3, T4, T5, T6>(Expression<Func<TKey, T1, T2, T3, T4, T5, T6, bool>> predicate);

    }

    public interface IGrouping<out TKey, out TElement>  where TElement : class
    {
        TKey Key { get; }

        
    }

    public interface IDbLinqQueryable
    {

    }
}