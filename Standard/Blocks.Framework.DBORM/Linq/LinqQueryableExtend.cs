using Blocks.Framework.AutoMapper;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Services.DataTransfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.Linq
{
    public static class LinqQueryableExtend
    {
        #region where

        public static IDbLinqQueryable<TEntity> Where<TEntity, T1>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }
        public static IDbLinqQueryable<TEntity> Where<TEntity, T1, T2>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }
        public static IDbLinqQueryable<TEntity> Where<TEntity, T1, T2, T3>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }

        public static IDbLinqQueryable<TEntity> Where<TEntity, T1, T2, T3, T4>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }

        public static IDbLinqQueryable<TEntity> Where<TEntity, T1, T2, T3, T4, T5>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }

        public static IDbLinqQueryable<TEntity> Where<TEntity, T1, T2, T3, T4, T5, T6>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }

        public static IDbLinqQueryable<TEntity> Where<TEntity, T1, T2, T3, T4, T5, T6, T7>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> predicate) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Where((LambdaExpression)predicate);
        }
        #endregion



        #region select

        public static List<TEntity> SelectToDynamicList<TEntity>(this IDbLinqQueryable<TEntity> dbLinqQueryable) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList().AutoMapTo<List<TEntity>>();
        }
        public static List<TEntity> SelectToDynamicList<TEntity, T1, TOut>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TOut>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TEntity>>();
        }
        public static List<TEntity> SelectToDynamicList<TEntity, T1, T2, TOut>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TOut>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TEntity>>();
        }

        public static List<TEntity> SelectToDynamicList<TEntity, T1, T2, T3, TOut>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TOut>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TEntity>>();
        }


        public static List<TDto> SelectToList<TEntity, T1, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, T3, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }
        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }


        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        }
        #endregion


        #region select async
        static async Task<T> SelectEntityToAsync<TEntity, T>(IDbLinqQueryable<TEntity> dbLinqQueryable, LambdaExpression selector) where TEntity : Data.Entity.Entity
        {
            var result = await dbLinqQueryable.SelectToListAsync((LambdaExpression)selector);

            return result.AutoMapTo<T>();
            //return Task.Factory.StartNew((s) =>
            //{
            //    Trace.TraceInformation("ThreadId:" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //    return dbLinqQueryable.SelectToList((LambdaExpression)s).AutoMapTo<T>();
            //}, selector);
        }


        public static Task<List<TEntity>> SelectToDynamicListAsync<TEntity, T1, TOut>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TOut>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TEntity>>(dbLinqQueryable, (LambdaExpression)selector);
        }
        public static Task<List<TEntity>> SelectToDynamicListAsync<TEntity, T1, T2, TOut>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TOut>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TEntity>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TEntity>> SelectToDynamicListAsync<TEntity, T1, T2, T3, TOut>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TOut>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TEntity>>(dbLinqQueryable, (LambdaExpression)selector);
        }


        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, T5, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, T5, T6, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }
        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, T5, T6, T7, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }


        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }

        public static Task<List<TDto>> SelectToListAsync<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return SelectEntityToAsync<TEntity, List<TDto>>(dbLinqQueryable, (LambdaExpression)selector);
        }
        #endregion
        #region paging
        public static PageList<TDto> Paging<TEntity, T1, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector, page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, T5, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, T5, T6, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, T5, T6, T7, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>> selector, Page page, bool distinct = false) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector,page, distinct).AutoMapTo<PageList<TDto>>();
        }


        #endregion


        #region groupby
        public static ILinqGroupQueryable<TKey> GroupBy<TEntity, T1, TKey>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TKey>> keySelector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.GroupBy<TKey>((LambdaExpression)keySelector);
        }

        public static ILinqGroupQueryable<TKey> GroupBy<TEntity, T1, T2, TKey>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TKey>> keySelector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.GroupBy<TKey>((LambdaExpression)keySelector);

        }
        public static ILinqGroupQueryable<TKey> GroupBy<TEntity, T1, T2, T3, TKey>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TKey>> keySelector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.GroupBy<TKey>((LambdaExpression)keySelector);
        }
        public static ILinqGroupQueryable<TKey> GroupBy<TEntity, T1, T2, T3, T4, TKey>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, TKey>> keySelector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.GroupBy<TKey>((LambdaExpression)keySelector);
        }

        public static ILinqGroupQueryable<TKey> GroupBy<TEntity, T1, T2, T3, T4, T5, TKey>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, TKey>> keySelector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.GroupBy<TKey>((LambdaExpression)keySelector);
        }

        public static ILinqGroupQueryable<TKey> GroupBy<TEntity, T1, T2, T3, T4, T5, T6, TKey>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, TKey>> keySelector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.GroupBy<TKey>((LambdaExpression)keySelector);
        }
        //public static List<TDto> SelectToList<TEntity, T1, T2, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}

        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}

        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}

        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}

        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}
        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}


        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}

        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}

        //public static List<TDto> SelectToList<TEntity, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TDto>> selector) where TEntity : Data.Entity.Entity
        //{
        //    return dbLinqQueryable.SelectToList((LambdaExpression)selector).AutoMapTo<List<TDto>>();
        //}
        #endregion


    }
}
