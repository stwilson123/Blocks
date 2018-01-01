using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Abp.AutoMapper;
using Blocks.Framework.Exceptions.Helper;

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
    public class DefaultLinqQueryable<TEntity> : ILinqQueryable<TEntity>  where TEntity : Data.Entity.Entity 
    {
        private Dictionary<(Type TableType,string TableAlias), object> linqSqlTableContext;
        public DefaultLinqQueryable(IQueryable<TEntity> iQuerable)
        {
            this.iQuerable = iQuerable;
            this.linqSqlTableContext = new Dictionary<(Type TableType,string TableAlias), object>();
        }

        public IQueryable<TEntity> iQuerable { get; }

        private IQueryable<Dictionary<(Type TableType, string TableAlias), Data.Entity.Entity>> iQueryContext;

        public ILinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(IEnumerable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector)   
        {
            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");

            if (iQueryContext == null)
            {
//                iQueryContext = iQuerable.Join(inner, outerKeySelector, innerKeySelector, (outerObj, innerObj) =>
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

        public List<TEntity> SelectToList(Expression<Func<TEntity, dynamic>> selector)
        {
            var select = iQuerable.Select(selector).ToList();

//            var a = Mapper.Map<List<TEntity>>(select);
//            var result = ;
            return select.MapTo<List<TEntity>>();
        }

    }

     
    
   
}