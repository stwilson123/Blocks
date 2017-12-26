using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blocks.Framework.Exceptions.Helper;

namespace Blocks.Framework.DBORM.Linq
{
    public static class ValueTypeExtensions
    {
        public  static Entity.Entity  Get<Table>(this Dictionary<ValueTuple<Type,string>,Entity.Entity> valueTuple,string tableAliasName)
        {
            return valueTuple[(typeof(Table), tableAliasName)];
        }
    }
    public class DefaultLinqQueryable<TEntity> : ILinqQueryable<TEntity>  where TEntity : Entity.Entity
    {
        private Dictionary<(Type TableType,string TableAlias), Entity.Entity> linqSqlTableContext;
        public DefaultLinqQueryable(IQueryable<TEntity> iQuerable)
        {
            this.iQuerable = iQuerable;
            this.linqSqlTableContext = new Dictionary<(Type TableType,string TableAlias), Entity.Entity>();
        }

        public IQueryable<TEntity> iQuerable { get; }

        private IQueryable<Dictionary<(Type TableType, string TableAlias), Entity.Entity>> iQueryContext;

        public ILinqQueryable<TEntity> InnerJoin<TOuter, TInner, TKey>(IEnumerable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector) where TInner : Entity.Entity
        {
            var outerParam = outerKeySelector.Parameters.FirstOrDefault();
            var innerParam = innerKeySelector.Parameters.FirstOrDefault();
            ExceptionHelper.ThrowArgumentNullException(outerParam, "outerKeySelector.Parameters");
            ExceptionHelper.ThrowArgumentNullException(innerParam, "innerKeySelector.Parameters");


            iQueryContext = iQuerable.Join(inner, outerKeySelector, innerKeySelector, (outerObj, innerObj) =>
            {
                if (!linqSqlTableContext.ContainsKey((typeof(TOuter), outerParam.Name)))
                {
                    linqSqlTableContext.Add((typeof(TOuter), outerParam.Name), outerObj);
                }
                if (!linqSqlTableContext.ContainsKey((typeof(TOuter), innerParam.Name)))
                {
                    linqSqlTableContext.Add((typeof(TOuter), innerParam.Name), innerObj);
                }
                return linqSqlTableContext;
            });
            iQueryContext.Join(inner,a => a.Get<TInner>("").Id ,(a,b）=> new {a,b })
            return this;
        }


    }
}