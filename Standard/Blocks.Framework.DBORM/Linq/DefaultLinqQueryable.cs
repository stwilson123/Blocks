﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Abp.AutoMapper;
using Blocks.Framework.Exceptions.Helper;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Blocks.Framework.DBORM.Linq.Extends;
using Blocks.Framework.Data.Paging;
using System.Collections.ObjectModel;
using System.Linq.Dynamic;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Localization;
using DynamicQueryableExtensions = System.Linq.Dynamic.Core.DynamicQueryableExtensions;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.Linq
{
    public static class ValueTypeExtensions
    {
        public  static Data.Entity.Entity  Get<Table>(this Dictionary<ValueTuple<Type,string>,Data.Entity.Entity> valueTuple,string tableAliasName)
        {
            return valueTuple[(typeof(Table), tableAliasName)];
        }
        public  static Data.Entity.Entity  Get(this Dictionary<ValueTuple<Type,string>,Data.Entity.Entity> valueTuple,Type Table,string tableAliasName)
        {
            return valueTuple[(Table, tableAliasName)];
        }
    }
    public class DefaultLinqQueryable<TEntity> : IDbLinqQueryable<TEntity>  where TEntity : Data.Entity.Entity 
    {
        private Dictionary<(Type TableType,string TableAlias), object> linqSqlTableContext;
        private TableAlias tableAlias;
        private bool isFirstInnerJoin;
        public DefaultLinqQueryable(IQueryable<TEntity> iQuerable, DbContext dbContext)
        {
            this.iQuerable = iQuerable;
            this.dbContext = dbContext;
            this.linqSqlTableContext = new Dictionary<(Type TableType,string TableAlias), object>();
            tableAlias = new TableAlias();
            isFirstInnerJoin = true;
        }

        public IQueryable iQuerable { get; private set; }
        public DbContext dbContext { get; }

        private IQueryable<Dictionary<(Type TableType, string TableAlias), Data.Entity.Entity>> iQueryContext;

        public IDbLinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible where TOuter : class where TInner : class
        {
          
            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");


            //TODO validate table alias;
            var outerParamIsNotExist = false;
            if (tableAlias.Any() && !tableAlias.Any(t => t.TableAlias == outerParam.Name))
                outerParamIsNotExist = true;
            tableAlias.Add((typeof(TOuter), outerParam.Name));
            tableAlias.Add((typeof(TInner), innerParam.Name));

            var querable = transferQuaryable();
            if (querable != null)
            {
                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(), $"{((MemberExpression)outerKeySelector.Body).Member.Name}",
                       $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector());
            }
            else
            {
                if(outerParamIsNotExist)
                      throw new BlocksDBORMException(StringLocal.Format("Can't find table alias in the history join expression.Please check Touter join expression."));

                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(), $"{outerParam.Name}.{((MemberExpression)outerKeySelector.Body).Member.Name}",
                    $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector());
            }
           
             
//            iQueryContext = iQueryContext.Join(inner, a => outerKeySelector(a.Get(outerParam.Type, outerParam.Name)), innerKeySelector,
//                (outerObj, innerObj) =>
//                {
//                    if (!linqSqlTableContext.ContainsKey((typeof(TOuter), outerParam.Name)))
//                    {
//                        linqSqlTableContext.Add((typeof(TOuter), outerParam.Name), outerObj);
//                    }
//                    if (!linqSqlTableContext.ContainsKey((typeof(TOuter), innerParam.Name)))
//                    {
//                        linqSqlTableContext.Add((typeof(TOuter), innerParam.Name), innerObj);
//                    }
//                    return linqSqlTableContext;
//                });
            return this;
        }

        
         public IDbLinqQueryable<TEntity> LeftJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible where TOuter : class where TInner : class
        {
          
            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");


            //TODO validate table alias;

            tableAlias.Add((typeof(TOuter), outerParam.Name));
            tableAlias.Add((typeof(TInner), innerParam.Name));

            var querable = transferQuaryable();
            if (querable != null)
            {
                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(), $"{((MemberExpression)outerKeySelector.Body).Member.Name}",
                       $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector()).DefaultIfEmpty();
            }
            else
            {
                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(), $"{outerParam.Name}.{((MemberExpression)outerKeySelector.Body).Member.Name}",
                    $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector()).DefaultIfEmpty();
            }
           
              
            return this;
        }
        public IDbLinqQueryable<TEntity> Where(LambdaExpression predicate)
        {
            ExceptionHelper.ThrowArgumentNullException(predicate, "predicate");
            validateParamter(predicate.Parameters);

            var querable = transferQuaryable();

            if (querable != null)
            {
                iQuerable = querable.Where(predicate);
            }
            else
            {
                var a = ExpressionUtils.Convert(predicate, iQuerable.ElementType);
                iQuerable = iQuerable.Where(a);
            }

            return this;
        }


        public long Count()
        {
            return transferQuaryable().LongCount();
        }

        public override string ToString()
        {
            return iQuerable.ToString();
        }

       

        //public List<TEntity> SelectToList(Expression<Func<TEntity, dynamic>> selector)
        //{
        //    var querable = transferQuaryable();
        //    if (querable != null)
        //    {
        //        iQuerable = querable.Select(selector);
        //    }
        //    else
        //    {
        //        var source = iQuerable;
        //        var a = ExpressionUtils.Convert(selector, iQuerable.ElementType);
        //        iQuerable = iQuerable.Select(a);
        //    }
        
        //    return iQuerable.ToDynamicList().MapTo<List<TEntity>>();
        //}

        //public List<TDto> SelectToList<TDto>(Expression<Func<TEntity, TDto>> selector)
        //{
        //    throw new NotImplementedException();
        //}

        public List<dynamic> SelectToList(LambdaExpression selector)
        {
            ExceptionHelper.ThrowArgumentNullException(selector, "selector");
            validateParamter(selector.Parameters);

            var querable = transferQuaryable();

            if (querable != null)
            {
                iQuerable = querable.Select(selector);
            }
            else
            {
                var source = iQuerable;
                var a = ExpressionUtils.Convert(selector, iQuerable.ElementType);
                iQuerable = iQuerable.Select(a);
            }
            return iQuerable.ToDynamicList();
        }

        public Task<List<dynamic>> SelectToListAsync(LambdaExpression selector)
        {
            return Task.Factory.StartNew((s) => SelectToList((LambdaExpression)s), selector);
        }

        public IDbLinqQueryable<TEntity> Take(int count)
        {
            iQuerable =  DynamicQueryableExtensions.Take(iQuerable, count);
            return this;
        }

        public IDbLinqQueryable<TEntity> Skip(int count)
        {
            if (count == 0)
            {
                iQuerable =DynamicQueryableExtensions.Skip(iQuerable, count);

            }
            else
            {
                iQuerable = DynamicQueryableExtensions.Skip(iQuerable, count);
            }
            return this;
        }

        public IDbLinqQueryable<TEntity> OrderBy<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector)
        {
            var querable = transferQuaryable();

            if (querable != null)
            {
                iQuerable = querable.OrderBy(keySelector);
            }
            else
            {
                var a = ExpressionUtils.Convert(keySelector, iQuerable.ElementType);
                iQuerable = iQuerable.OrderBy(a);
            }

            return this;
        }

        public IDbLinqQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return OrderBy<TEntity, TKey>(keySelector);
        }

        private IQueryable<TEntity> transferQuaryable()
        {
            return iQuerable is IQueryable<TEntity> ? (IQueryable<TEntity>)iQuerable : null;
        }

      

        private void validateParamter(ReadOnlyCollection<ParameterExpression> parameterCollection)
        {
            ExceptionHelper.ThrowArgumentNullException(parameterCollection, "parameterCollection");
            foreach (var item in parameterCollection)
            {
                if (tableAlias.Any() && !tableAlias.Any(t => t.TableAlias == item.Name))
                    throw new BlocksDBORMException(StringLocal.Format("Can't find table alias in the join expression.Please check join expression."));
            }
        }

        public PageList<dynamic> Paging(LambdaExpression selector, Page page)
        {
            ExceptionHelper.ThrowArgumentNullException(selector, "selector");
            ExceptionHelper.ThrowArgumentNullException(page, "page");
            //TODO check page property 
 

            validateParamter(selector.Parameters);

            var querable = transferQuaryable();
            
            
           
           
            
            
            if (querable != null)
            {
                iQuerable = querable.Select(selector);
            }
            else
            {
                var source = iQuerable;
                var a = ExpressionUtils.Convert(selector, iQuerable.ElementType);
                iQuerable = iQuerable.Select(a);
            }

                 
            if (page.filters != null && page.filters.rules != null && page.filters.rules.Any())
            {
            
                var whereString = PageDynamicSearch.getStringForGroup(page.filters,null);
                iQuerable = DynamicQueryableExtensions.Where(iQuerable, whereString);
              
            }



            var orderByQueryable = !string.IsNullOrEmpty(page.OrderBy) 
                ? DynamicQueryableExtensions.OrderBy(iQuerable, page.OrderBy)
                : iQuerable;
            
            if (page.pageSize == -1)
            {
                var rows = orderByQueryable.ToDynamicList();
                var pagelist = new PageList<dynamic>()
                {
                    Rows = rows,
                    PagerInfo = new Page()
                    {
                        page = page.page,
                        pageSize = page.pageSize,
                        records = rows.Count,
                        sortColumn = page.sortColumn,
                        sortOrder = page.sortOrder
                    }
                };

                return pagelist;
            }
            else
            {
                var pageResult = Blocks.Framework.Data.Paging.DynamicQueryableExtensions.PageResult<dynamic>(orderByQueryable,page.page, page.pageSize);
               
//                var pagelist = new PageList<dynamic>()
//                {
//                    Rows = pageResult.Queryable.ToDynamicList(),
//                    PagerInfo = new Page()
//                    {
//                        page = pageResult.CurrentPage,
//                        pageSize = pageResult.PageSize,
//                        records = pageResult.RowCount,
//                        sortColumn = page.sortColumn,
//                        sortOrder = page.sortOrder
//                    }
//                };
                pageResult.PagerInfo.sortColumn = page.sortColumn;
                pageResult.PagerInfo.sortOrder = page.sortOrder;
                return pageResult;
            }
           
        }
        
    

        public IDbLinqQueryable<TEntity> OrderByDescending<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector)
        {
            var querable = transferQuaryable();

            if (querable != null)
            {
                iQuerable = querable.OrderByDescending(keySelector);
            }
            else
            {
                var a = ExpressionUtils.Convert(keySelector, iQuerable.ElementType);
                iQuerable = iQuerable.OrderByDescending(a);
            }

            return this;
        }

        public IDbLinqQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return OrderByDescending<TEntity, TKey>(keySelector);
        }

        public IDbLinqQueryable<TEntity> ThenBy<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector)
        {
            var querable = transferQuaryable();

            if (querable != null)
            {
                iQuerable = querable.ThenBy(keySelector);
            }
            else
            {
                var a = ExpressionUtils.Convert(keySelector, iQuerable.ElementType);
                iQuerable = iQuerable.ThenBy(a);
            }

            return this;
        }

        public IDbLinqQueryable<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ThenBy<TEntity, TKey>(keySelector);

        }

        public IDbLinqQueryable<TEntity> ThenByDescending<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector)
        {
            var querable = transferQuaryable();

            if (querable != null)
            {
                iQuerable = querable.ThenByDescending(keySelector);
            }
            else
            {
                var a = ExpressionUtils.Convert(keySelector, iQuerable.ElementType);
                iQuerable = iQuerable.ThenByDescending(a);
            }

            return this;
        }

        public IDbLinqQueryable<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ThenByDescending<TEntity, TKey>(keySelector);

        }
    }




   
     
    class TableAlias
    {
        private List<(Type TableType, string TableAlias)> listTableAlias;
        public TableAlias()
        {
            listTableAlias = new List<(Type TableType, string TableAlias)>();
            
        }
        public bool Any()
        {
            return listTableAlias.Any();
        }
        public bool Any(Func<(Type TableType, string TableAlias), bool> predicate)
        {
            return listTableAlias.Any(predicate);
        }

        public void Add((Type TableType, string TableAlias) item)
        {
            if (listTableAlias.Contains(item))
                return;
            listTableAlias.Add(item);
        }

        public string CreateResultSelector()
        {
            if (!listTableAlias.Any())
                throw new Exception("Can't generate resultSelector because listTableAlias is Empty!");
            if (listTableAlias.Count == 2)
                return $"new(inner as {listTableAlias[1].TableAlias}, outer as {listTableAlias[0].TableAlias})";
            var outerAlias = listTableAlias.Take(listTableAlias.Count - 1).Select(t => "outer." + t.TableAlias);
            return $"new(inner as {listTableAlias.Last().TableAlias}, {string.Join(",", outerAlias)})";
        }
    }



}