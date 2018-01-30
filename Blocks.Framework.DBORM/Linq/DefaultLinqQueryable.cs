using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Abp.AutoMapper;
using Blocks.Framework.Exceptions.Helper;
using System.Linq.Dynamic.Core;
using System.Data.Entity;
using Blocks.Framework.DBORM.Linq.Extends;

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
            Expression<Func<TInner, TKey>> innerKeySelector) where TKey : IComparable, IConvertible
        {
          
            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");


            //TODO validate table alias;

            iQuerable.Take
            tableAlias.Add((typeof(TOuter), outerParam.Name));
            tableAlias.Add((typeof(TInner), innerParam.Name));
            if(isFirstInnerJoin)
            {
                iQuerable = iQuerable.Join(dbContext.Set(typeof(TInner)), $"{((MemberExpression)outerKeySelector.Body).Member.Name}",
                    $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector());
                isFirstInnerJoin = false;
            }
            else
            {
                iQuerable = iQuerable.Join(dbContext.Set(typeof(TInner)), $"{outerParam.Name}.{((MemberExpression)outerKeySelector.Body).Member.Name}",
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

 
        
        public IDbLinqQueryable<TEntity> Where<T>(Expression<Func<T, bool>> predicate) 
        {
            var source = iQuerable;
            if (isFirstInnerJoin)
            {
                var newSelect = Expression.Call(typeof(Queryable), nameof(Queryable.Where), new[] { source.ElementType }, source.Expression, predicate);
                iQuerable = source.Provider.CreateQuery(newSelect);
            }
            else
            {
                var a = ExpressionUtils.Convert(predicate, iQuerable.ElementType);
                var newSelect = Expression.Call(typeof(Queryable), nameof(Queryable.Where), new[] { source.ElementType }, source.Expression, Expression.Quote(a));
                iQuerable = source.Provider.CreateQuery(newSelect);
            }
             
            return this;
        }

        public override string ToString()
        {
            return iQuerable.ToString();
        }

        public IDbLinqQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var source = iQuerable;
            if (isFirstInnerJoin)
            {
                var newSelect = Expression.Call(typeof(Queryable), nameof(Queryable.Where), new[] { source.ElementType }, source.Expression, predicate);
                iQuerable = source.Provider.CreateQuery(newSelect);
            }
            else
            {
                var a = ExpressionUtils.Convert(predicate, iQuerable.ElementType);
              
                var newSelect = Expression.Call(typeof(Queryable), nameof(Queryable.Where), new[] { source.ElementType }, source.Expression, Expression.Quote(a));
                iQuerable = source.Provider.CreateQuery(newSelect);
            }
             

            return this;
        }

        public List<TEntity> SelectToList(Expression<Func<TEntity, dynamic>> selector)
        {
            var source = iQuerable;
            if (isFirstInnerJoin)
            {
                var newSelect = Expression.Call(typeof(Queryable), nameof(Queryable.Select), new[] { source.ElementType, selector.Body.Type }, source.Expression, selector);
                iQuerable = source.Provider.CreateQuery(newSelect);
            }
            else
            {
                var a = ExpressionUtils.Convert(selector, iQuerable.ElementType);
                var newSelect = Expression.Call(typeof(Queryable), nameof(Queryable.Select), new[] { source.ElementType, a.Body.Type }, source.Expression, Expression.Quote(a));
                iQuerable = source.Provider.CreateQuery(newSelect);
            }
               
            return null;
        }

        public IDbLinqQueryable<TEntity> Take(int count)
        {
            iQuerable = iQuerable.Take(count);
            return this;
        }

        public IDbLinqQueryable<TEntity> Skip(int count)
        {
            iQuerable = iQuerable.Skip(count);
            return this;

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