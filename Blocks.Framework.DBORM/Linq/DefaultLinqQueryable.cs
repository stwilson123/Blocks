﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Abp.AutoMapper;
using Blocks.Framework.Exceptions.Helper;
using System.Linq.Dynamic.Core;
using System.Data.Entity;
using Blocks.Framework.DBORM.Linq.Extends;
using Blocks.Framework.Data.Paging;
using System.Collections.ObjectModel;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Localization;

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

            tableAlias.Add((typeof(TOuter), outerParam.Name));
            tableAlias.Add((typeof(TInner), innerParam.Name));

            var querable = transferQuaryable();
            if (querable != null)
            {
                iQuerable = iQuerable.Join(dbContext.Set(typeof(TInner)), $"{((MemberExpression)outerKeySelector.Body).Member.Name}",
                       $"{((MemberExpression)innerKeySelector.Body).Member.Name}", tableAlias.CreateResultSelector());
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
            
            
            if (page.filters != null)
            {
                var whereString = getStringForGroup(page.filters,null);
                iQuerable = querable.Where(whereString);
              
            }
           
            
            
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

          
            var pageResult = iQuerable.OrderBy(page.OrderBy).PageResult(page.page, page.pageSize);

            var pagelist = new PageList<dynamic>()
            {
                Rows = pageResult.Queryable.ToDynamicList(),
                PagerInfo = new Page()
                {
                    page = pageResult.PageCount,
                    pageSize = pageResult.PageSize,
                    records = pageResult.RowCount,
                    sortColumn = page.sortColumn,
                    sortOrder = page.sortOrder
                }
            };

            return pagelist;
        }
        
        string getStringForGroup(Group group,List<DbParam> listDbParam)
        {
            var alias = group.rules.Select(t =>  t.field.Contains('.') ?  t.field.Substring(0,t.field.IndexOf('.')) : "").Where(t => !string.IsNullOrEmpty(t));
            var s = "{";
            if (group.groups != null) {
                for (var index = 0; index < group.groups.Count; index++) {
                    if (s.Length > 1) {
                        s += " " + group.groupOp + " ";
                    }
                    try {
                        s += this.getStringForGroup(group.groups[index],null);
                    } catch (Exception ex) {throw;}
                }
            }

            if (group.rules != null) {
                try{
                    for (var index = 0; index < group.rules.Count; index++) {
                        if (s.Length > 1) {
                            s += " " + group.groupOp + " ";
                        }
                        s += this.getStringForRule(group.rules[index]);
                    }
                } catch (Exception ex) { throw;}
            }

            s += "}";

            if (s == "{}") {
                return ""; // ignore groups that don't have rules
            }
            s  = s.Insert(0, $"({string.Join(",", alias)})");
            return s;
        }
        
       string getStringForRule (Rule rule)
        {
            var opUF = "";
            var opC = "";
            var cm = "";
            var ret = "";var val = "";
              //  numtypes = ['int', 'integer', 'float', 'number', 'currency']; // jqGrid

            if (Rule.opend.ContainsKey(rule.op))
            {
                opUF = Rule.opend[rule.op];
                opC = rule.op;
            }
            cm = rule.field;
            val = rule.data;
            if(opC == "bw" || opC == "bn") { val = val+"%"; }
            if(opC == "ew" || opC == "en") { val = "%"+val; }
            if(opC == "cn" || opC == "nc") { val = "%"+val+"%"; }
            if(opC == "in" || opC == "ni") { val = " ("+val+")"; }
//            if(p.errorcheck) { checkData(rule.data, cm); }
//            if($.inArray(cm.searchtype, numtypes) != -1 || opC == 'nn' || opC == 'nu') { ret = rule.field + " " + opUF + " " + val; }
            else { ret = rule.field + " " + opUF + " \"" + val + "\""; }
            return ret;
        }
    }




    class DbParam
    {
        public string param { set; get; }
        
        public object value { set; get; }

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