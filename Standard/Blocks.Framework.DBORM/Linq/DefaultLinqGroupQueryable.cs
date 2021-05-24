using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Exceptions.Helper;
using System.Linq;
using Blocks.Framework.DBORM.Linq.Extends;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Blocks.Framework.Localization;
using Blocks.Framework.Data.Entity;

namespace Blocks.Framework.DBORM.Linq
{
    public class DefaultLinqGroupQueryable<TEntity, TKey> : ILinqGroupQueryable<TKey> where TEntity : Data.Entity.Entity
    {
        private IQueryable iQuerable;
        private readonly TableAlias tableAlias;

        internal DefaultLinqGroupQueryable(IQueryable iQuerable, DbContext dbContext, TableAlias tableAlias)
        {
            this.iQuerable = iQuerable;
            this.tableAlias = tableAlias;
        }

        public Data.Paging.PageList<dynamic> Paging(LambdaExpression selector, Page page)
        {
            throw new NotImplementedException();
        }

        public Data.Paging.PageList<dynamic> Paging(LambdaExpression selector, Page page, bool distinct)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> SelectToList(LambdaExpression selector)
        {

            return SelectToListCore(selector).ToDynamicList();
        }

        public ILinqGroupQueryable<TKey> OrderByDescending<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>
        {
            ExceptionHelper.ThrowArgumentNullException(keySelector, nameof(keySelector));
            validateParameter(keySelector);

            var querable = iQuerable;
            var a = ExpressionUtils.GroupConvert(keySelector, querable.ElementType, nameof(keySelector));

            iQuerable = querable.OrderBy(a);
            return this;
        }

        public ILinqGroupQueryable<TKey> OrderBy<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>
        {
            ExceptionHelper.ThrowArgumentNullException(keySelector, nameof(keySelector));

            validateParameter(keySelector);
            var querable = iQuerable;
            var a = ExpressionUtils.GroupConvert(keySelector, querable.ElementType, nameof(keySelector));

            iQuerable = querable.OrderBy(a);
            return this;
        }

        public ILinqGroupQueryable<TKey> ThenBy<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>
        {
            ExceptionHelper.ThrowArgumentNullException(keySelector, nameof(keySelector));

            validateParameter(keySelector);
            var querable = iQuerable;
            var a = ExpressionUtils.GroupConvert(keySelector, querable.ElementType, nameof(keySelector));

            iQuerable = querable.ThenBy(a);
            return this;
        }

        public ILinqGroupQueryable<TKey> ThenByDescending<T1>(Expression<Func<TKey, T1, object>> keySelector) where T1 : IEnumerable<Data.Entity.Entity>
        {
            ExceptionHelper.ThrowArgumentNullException(keySelector, nameof(keySelector));

            validateParameter(keySelector);
            var querable = iQuerable;
            var a = ExpressionUtils.GroupConvert(keySelector, querable.ElementType, nameof(keySelector));

            iQuerable = querable.ThenByDescending(a);
            return this;
        }

        public ILinqGroupQueryable<TKey> Where(LambdaExpression predicate)
        {
            ExceptionHelper.ThrowArgumentNullException(predicate, nameof(predicate));

            validateParameter(predicate);
            var querable = iQuerable;
            var a = ExpressionUtils.GroupConvert(predicate, querable.ElementType, nameof(predicate));

            iQuerable = querable.Where(a);
            return this;
        }

        #region Where

        public ILinqGroupQueryable<TKey> Where<T1>(Expression<Func<TKey, T1, bool>> predicate)
        {
            return this.Where((LambdaExpression)predicate);
        }

        public ILinqGroupQueryable<TKey> Where<T1, T2>(Expression<Func<TKey, T1, T2, bool>> predicate)
        {
            return this.Where((LambdaExpression)predicate);
        }

        public ILinqGroupQueryable<TKey> Where<T1, T2, T3>(Expression<Func<TKey, T1, T2, T3, bool>> predicate)
        {
            return this.Where((LambdaExpression)predicate);
        }

        public ILinqGroupQueryable<TKey> Where<T1, T2, T3, T4>(Expression<Func<TKey, T1, T2, T3, T4, bool>> predicate)
        {
            return this.Where((LambdaExpression)predicate);
        }

        public ILinqGroupQueryable<TKey> Where<T1, T2, T3, T4, T5>(Expression<Func<TKey, T1, T2, T3, T4, T5, bool>> predicate)
        {
            return this.Where((LambdaExpression)predicate);
        }

        public ILinqGroupQueryable<TKey> Where<T1, T2, T3, T4, T5, T6>(Expression<Func<TKey, T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            return this.Where((LambdaExpression)predicate);
        }
        #endregion

        private IQueryable SelectToListCore(LambdaExpression selector)
        {
            ExceptionHelper.ThrowArgumentNullException(selector, nameof(selector));
            validateParameter(selector);

            var querable = iQuerable;


            var a = ExpressionUtils.GroupConvert(selector, querable.ElementType, "selectorEx");

            iQuerable = querable.Select(a);
            return iQuerable;
        }


        private void validateParameter(LambdaExpression selector)
        {
            var keyParameter = selector.Parameters.FirstOrDefault(p => p.Type == typeof(TKey));
            if (keyParameter != null && keyParameter.Name != "key")
            {
                throw new BlocksDBORMException(
                       StringLocal.Format(
                           "TKey parameter must be named [key]."));
            }

            var validateParams = selector.Parameters.Where(p => typeof(IEnumerable<Data.Entity.Entity>).IsAssignableFrom(p.Type));
            //more TODO
            this.tableAlias.ValidateParameter(validateParams.Select(p =>
                 Expression.Parameter(p.Type.GetGenericArguments().FirstOrDefault(), p.Name)
            ));
        }

        private IQueryable<TEntity> transferQuaryable()
        {
            return iQuerable is IQueryable<TEntity> ? (IQueryable<TEntity>)iQuerable : null;
        }

        #region SelectToList

        public List<TDto> SelectToList<T1, TDto>(Expression<Func<TKey, T1, TDto>> selector) where T1 : IEnumerable<Data.Entity.Entity>
        {
            return this.SelectToList((LambdaExpression)selector).Cast<TDto>().ToList();
        }
        public List<TDto> SelectToList<T1, T2, TDto>(Expression<Func<TKey, T1, T2, TDto>> selector)
            where T1 : IEnumerable<Data.Entity.Entity>
            where T2 : IEnumerable<Data.Entity.Entity>
        {
            return this.SelectToList((LambdaExpression)selector).Cast<TDto>().ToList();
        }

        public List<TDto> SelectToList<T1, T2, T3, TDto>(Expression<Func<TKey, T1, T2, T3, TDto>> selector)
          where T1 : IEnumerable<Data.Entity.Entity>
          where T2 : IEnumerable<Data.Entity.Entity>
          where T3 : IEnumerable<Data.Entity.Entity>
        {
            return this.SelectToList((LambdaExpression)selector).Cast<TDto>().ToList();
        }

        public List<TDto> SelectToList<T1, T2, T3, T4, TDto>(Expression<Func<TKey, T1, T2, T3, T4, TDto>> selector)
         where T1 : IEnumerable<Data.Entity.Entity>
         where T2 : IEnumerable<Data.Entity.Entity>
         where T3 : IEnumerable<Data.Entity.Entity>
         where T4 : IEnumerable<Data.Entity.Entity>
        {
            return this.SelectToList((LambdaExpression)selector).Cast<TDto>().ToList();
        }


        public List<TDto> SelectToList<T1, T2, T3, T4, T5, TDto>(Expression<Func<TKey, T1, T2, T3, T4, T5, TDto>> selector)
           where T1 : IEnumerable<Data.Entity.Entity>
           where T2 : IEnumerable<Data.Entity.Entity>
           where T3 : IEnumerable<Data.Entity.Entity>
           where T4 : IEnumerable<Data.Entity.Entity>
           where T5 : IEnumerable<Data.Entity.Entity>
        {
            return this.SelectToList((LambdaExpression)selector).Cast<TDto>().ToList();
        }

        public List<TDto> SelectToList<T1, T2, T3, T4, T5, T6, TDto>(Expression<Func<TKey, T1, T2, T3, T4, T5, T6, TDto>> selector)
           where T1 : IEnumerable<Data.Entity.Entity>
           where T2 : IEnumerable<Data.Entity.Entity>
           where T3 : IEnumerable<Data.Entity.Entity>
           where T4 : IEnumerable<Data.Entity.Entity>
           where T5 : IEnumerable<Data.Entity.Entity>
           where T6 : IEnumerable<Data.Entity.Entity>
        {
            return this.SelectToList((LambdaExpression)selector).Cast<TDto>().ToList();
        }

      
        #endregion

    }
}
