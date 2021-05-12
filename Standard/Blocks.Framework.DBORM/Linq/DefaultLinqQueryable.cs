using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blocks.Framework.DBORM.Linq
{
    public static class ValueTypeExtensions
    {
        public static Data.Entity.Entity Get<Table>(
            this Dictionary<ValueTuple<Type, string>, Data.Entity.Entity> valueTuple, string tableAliasName)
        {
            return valueTuple[(typeof(Table), tableAliasName)];
        }

        public static Data.Entity.Entity Get(this Dictionary<ValueTuple<Type, string>, Data.Entity.Entity> valueTuple,
            Type Table, string tableAliasName)
        {
            return valueTuple[(Table, tableAliasName)];
        }
    }

    public class DefaultLinqQueryable<TEntity> : IDbLinqQueryable<TEntity> where TEntity : Data.Entity.Entity
    {
        private Dictionary<(Type TableType, string TableAlias), object> linqSqlTableContext;
        private TableAlias tableAlias;
        private bool isFirstInnerJoin;

        public DefaultLinqQueryable(IQueryable<TEntity> iQuerable, DbContext dbContext)
        {
            this.iQuerable = iQuerable;
            this.dbContext = dbContext;
            this.linqSqlTableContext = new Dictionary<(Type TableType, string TableAlias), object>();
            tableAlias = new TableAlias();
            isFirstInnerJoin = true;
        }

        public IQueryable iQuerable { get; private set; }
        public DbContext dbContext { get; }

        private IQueryable<Dictionary<(Type TableType, string TableAlias), Data.Entity.Entity>> iQueryContext;

        private string JoinExpressionGenernateKey<TInput, TKey>(Expression<Func<TInput, TKey>> selector,string prefixName = "")
        {
            var prefix = string.IsNullOrEmpty(prefixName) ? "" : prefixName + ".";
            if (selector.Body is MemberExpression memberExpression)
            {
                return $"{prefix + memberExpression.Member.Name }";
            }
            else if(selector.Body is NewExpression newExpression)
            {
                var paramsExpressions = new List<string>();
                for (int i = 0; i < newExpression.Arguments.Count; i++)
                {
                    var a = newExpression.Arguments[i];
                    if (a is MemberExpression member)
                    {
                       
                        paramsExpressions.Add($"{prefix + member.Member.Name} as {newExpression.Members[i].Name}");
                    }
                }
                    
               return $"new ({ string.Join(", ", paramsExpressions)})";
            }

            throw new Exception("Not supported expression " + selector.Body);
        }
        public IDbLinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector)  
            where TOuter : class
            where TInner : class
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
            var parsingConfig = new ParsingConfig() { EvaluateGroupByAtDatabase = true, };
            var querable = transferQuaryable();
            if (querable != null)
            {
                iQuerable = DynamicQueryableExtensions.Join(iQuerable,
                    parsingConfig,
                    dbContext.Set<TInner>(),
                    JoinExpressionGenernateKey(outerKeySelector),
                    JoinExpressionGenernateKey(innerKeySelector), tableAlias.CreateResultSelector());
            }
            else
            {
                if (outerParamIsNotExist)
                    throw new BlocksDBORMException(StringLocal.Format(
                        "Can't find table alias in the history join expression.Please check Touter join expression."));

                iQuerable = DynamicQueryableExtensions.Join(iQuerable,
                    parsingConfig,
                    dbContext.Set<TInner>(),
                    JoinExpressionGenernateKey(outerKeySelector, outerParam.Name),
                    JoinExpressionGenernateKey(innerKeySelector), tableAlias.CreateResultSelector());
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
           Expression<Func<TInner, TKey>> innerKeySelector)  
           where TOuter : class
           where TInner : class
        {
            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");


            //TODO validate table alias;

            tableAlias.Add((typeof(TOuter), outerParam.Name));
            tableAlias.Add((typeof(TInner), innerParam.Name));
            var parsingConfig = new ParsingConfig() { RenameParameterExpression = true, EvaluateGroupByAtDatabase = true, };
            var querable = transferQuaryable();
            if (querable != null)
            {
                iQuerable = DynamicQueryableExtensions.GroupJoin(iQuerable,
                    parsingConfig,
                    dbContext.Set<TInner>(),

                   JoinExpressionGenernateKey(outerKeySelector),
                   JoinExpressionGenernateKey(innerKeySelector),
                    "new(inner as inner,outer as outer)"
                );
                iQuerable = DynamicQueryableExtensions.SelectMany(iQuerable,
                    "inner.DefaultIfEmpty()",
                    tableAlias.CreateResultSelector(true),
                    "inner",
                    "outer"
                );
            }
            else
            {
                iQuerable = DynamicQueryableExtensions.GroupJoin(iQuerable,
                    parsingConfig,
                    dbContext.Set<TInner>(),
                   JoinExpressionGenernateKey(outerKeySelector, outerParam.Name),
                    JoinExpressionGenernateKey(innerKeySelector),
                    "new(inner as inner,outer as outer)");
                iQuerable = DynamicQueryableExtensions.SelectMany(iQuerable,
                    "inner.DefaultIfEmpty()",
                    tableAlias.CreateResultSelector(true),
                    "outer",
                    "inner"
                );
                //                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(),
                //                        $"{outerParam.Name}.{((MemberExpression) outerKeySelector.Body).Member.Name}",
                //                        $"{((MemberExpression) innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector())
                //                    .DefaultIfEmpty();
            }


            return this;
        }

        //         public IDbLinqQueryable<TEntity> LeftJoin<TOuter, TInner, TKey>(
        //            Expression<Func<TOuter, TKey>> outerKeySelector,
        //            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible where TOuter : class where TInner : class
        //        {
        //          
        //            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
        //            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
        //            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
        //            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");
        //
        //
        //            //TODO validate table alias;
        //
        //            tableAlias.Add((typeof(TOuter), outerParam.Name));
        //            tableAlias.Add((typeof(TInner), innerParam.Name));
        //
        //            var querable = transferQuaryable();
        //            if (querable != null)
        //            {
        //                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(), $"{((MemberExpression)outerKeySelector.Body).Member.Name}",
        //                       $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector()).DefaultIfEmpty();
        //            }
        //            else
        //            {
        //                iQuerable = DynamicQueryableExtensions.Join(iQuerable, dbContext.Set<TInner>(), $"{outerParam.Name}.{((MemberExpression)outerKeySelector.Body).Member.Name}",
        //                    $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector()).DefaultIfEmpty();
        //            }
        //           
        //              
        //            return this;
        //        }
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
            return DynamicQueryableExtensions.Count(iQuerable);
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

        private IQueryable  SelectToListCore(LambdaExpression selector)
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
            return iQuerable;
        }
        public List<dynamic> SelectToList(LambdaExpression selector)
        {
  
            return SelectToListCore(selector).ToDynamicList();
        }

        public Task<List<dynamic>> SelectToListAsync(LambdaExpression selector)
        {

            var list = new List<dynamic>();
            //var queryCompiler = dbContext.GetService<IQueryCompiler>();
            //var compiledQuery = queryCompiler.CreateCompiledAsyncEnumerableQuery<dynamic>(SelectToListCore(selector));

            //return qc => new AsyncEnumerable<TResult>(compiledQuery(qc));
            //using ()
            //{
            //    while (await asyncEnumerator.MoveNext(cancellationToken))
            //    {
            //        list.Add(asyncEnumerator.Current);
            //    }
            //}

            var sl = SelectToListCore(selector);//.AsAsyncEnumerable().ToList(cancellationToken)

            return (sl as IAsyncEnumerable<dynamic>).ToList();
            //return Task.Factory.StartNew((s) => SelectToList((LambdaExpression) s), selector);
        }

        public IDbLinqQueryable<TEntity> Take(int count)
        {
            iQuerable = DynamicQueryableExtensions.Take(iQuerable, count);
            return this;
        }

        public IDbLinqQueryable<TEntity> Skip(int count)
        {
            if (count == 0)
            {
                iQuerable = DynamicQueryableExtensions.Skip(iQuerable, count);
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
            return iQuerable is IQueryable<TEntity> ? (IQueryable<TEntity>) iQuerable : null;
        }


        private void validateParamter(ReadOnlyCollection<ParameterExpression> parameterCollection)
        {
            ExceptionHelper.ThrowArgumentNullException(parameterCollection, "parameterCollection");
            foreach (var item in parameterCollection)
            {
                if (tableAlias.Any() && !tableAlias.Any(t => t.TableAlias == item.Name))
                    throw new BlocksDBORMException(
                        StringLocal.Format(
                            "Can't find table alias in the join expression.Please check join expression."));
            }
        }

        public PageList<dynamic> Paging(LambdaExpression selector, Page page)
        {
            return Paging(selector, page);
        }


        public PageList<dynamic> Paging(LambdaExpression selector, Page page, bool distinct)
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

            iQuerable = distinct ? iQuerable.Distinct() : iQuerable;

            if (page.filters != null && page.filters.rules != null && page.filters.rules.Any())
            {
                var whereString = PageDynamicSearch.getStringForGroup(page.filters, null);
                iQuerable = DynamicQueryableExtensions.Where(iQuerable, whereString);
            }


            var orderByQueryable = !string.IsNullOrEmpty(page.OrderBy)
                ? DynamicQueryableExtensions.OrderBy(iQuerable, page.OrderBy)
                : iQuerable;
            if (!page.pageSize.HasValue || page.pageSize <= 0)
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
                var pageResult =
                    Blocks.Framework.Data.Paging.DynamicQueryableExtensions.PageResult<dynamic>(orderByQueryable,
                        page.page, page.pageSize.Value);

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

        public string CreateResultSelector(bool isLeftJoin = false)
        {
            if (!listTableAlias.Any())
                throw new Exception("Can't generate resultSelector because listTableAlias is Empty!");
            if (listTableAlias.Count == 2)
            {
                if (!isLeftJoin)
                    return $"new(inner as {listTableAlias[1].TableAlias}, outer as {listTableAlias[0].TableAlias})";
                else
                {
                    return
                        $"new(inner.outer as {listTableAlias[0].TableAlias}, outer as {listTableAlias[1].TableAlias})";
                }
            }


            if (!isLeftJoin)
            {
                var outerAlias = listTableAlias.Take(listTableAlias.Count - 1).Select(t => "outer." + t.TableAlias + " as " +t.TableAlias );
                return $"new(inner as {listTableAlias.Last().TableAlias}, {string.Join(",", outerAlias)})";
            }
            else
            {
                var outerAlias = listTableAlias.Take(listTableAlias.Count - 1).Select(t => "outer.outer." + t.TableAlias + " as " +t.TableAlias );
                return $"new(inner as {listTableAlias.Last().TableAlias}, {string.Join(",", outerAlias)})";
            }
        }
    }
}