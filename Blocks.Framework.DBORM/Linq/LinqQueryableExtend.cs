using Abp.AutoMapper;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using System;
using System.Collections.Generic;
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
        #endregion

        #region select

        public static List<TEntity> SelectToList<TEntity,T1>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, dynamic>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).MapTo<List<TEntity>>();
        }
        public static List<TEntity> SelectToList<TEntity, T1, T2>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, dynamic>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).MapTo<List<TEntity>>();
        }

        public static List<TEntity> SelectToList<TEntity, T1, T2, T3>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, T3, dynamic>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).MapTo<List<TEntity>>();
        }


        public static List<TDto> SelectToList<TEntity, T1, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList ((LambdaExpression)selector).MapTo<List<TDto>>();
        }

        public static List<TDto> SelectToList<TEntity, T1, T2, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TDto>> selector) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.SelectToList((LambdaExpression)selector).MapTo<List<TDto>>();
        }
        #endregion

        #region paging
        public static PageList<TDto> Paging<TEntity, T1, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, TDto>> selector,Page page) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector, page).MapTo<PageList<TDto>>();
        }

        public static PageList<TDto> Paging<TEntity, T1, T2, TDto>(this IDbLinqQueryable<TEntity> dbLinqQueryable, Expression<Func<T1, T2, TDto>> selector, Page page) where TEntity : Data.Entity.Entity
        {
            return dbLinqQueryable.Paging((LambdaExpression)selector, page).MapTo<PageList<TDto>>();
        }

        #endregion
    }
}
