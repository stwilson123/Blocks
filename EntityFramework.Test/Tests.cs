using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Blocks.Framework.DBORM.Linq;
using EntityFramework.Test.Properties;
using Xunit;
using Queryable = System.Linq.Queryable;

namespace EntityFramework.Test
{
    public class Tests
    {
        

        public static long Get(AbpUsers a)
        {
            return a.Id;
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string) || seqType == typeof(byte[]))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.GetTypeInfo().IsGenericType)
                foreach (var genericArgument in seqType.GetGenericArguments())
                {
                    var type = typeof(IEnumerable<>).MakeGenericType(genericArgument);
                    if (type.IsAssignableFrom(seqType))
                        return type;
                }
            var interfaces = seqType.GetInterfaces();
            if (interfaces != null && interfaces.Length > 0)
                foreach (var seqType1 in interfaces)
                {
                    var ienumerable = FindIEnumerable(seqType1);
                    if (ienumerable != null)
                        return ienumerable;
                }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
                return FindIEnumerable(seqType.BaseType);
            return null;
        }

        [Fact]
        public void Test1()
        {
            using (var context = new BlocksEntities1())
            {
                var linq = context.AbpUsers.SqlQuery(@"SELECT  *
  FROM[Blocks].[dbo].[AbpUsers] a INNER JOIN dbo.AbpUsers b  ON a.CreatorUserId = b.CreatorUserId");
                var strLinq = linq.ToString();
                var dataLinq = linq.ToList();
            }
        }


        [Fact]
        public void Test2()
        {
            // var ab = FindIEnumerable(dynamicType.GetType());
            Expression<Func<AbpUsers, long?>> expression = users => users.DeleterUserId;
          
            var sourceProperties = new Dictionary<string, Type>()
            {
                { "AbpUsers2",typeof(AbpUsers) }, { "AbpUsers4",typeof(AbpUsers) }, 

                
            };
            Type dynamicType = LinqRuntimeTypeBuilder.GetDynamicType(sourceProperties);
           
            NewExpression selector = Expression.New(dynamicType.GetConstructor(Type.EmptyTypes));
            dynamic dynamicObject = new { };
            Func<AbpUsers,AbpUsers, dynamic> func = (a,b) =>
            {
                dynamicObject["a"] = a;
                dynamicObject["b"] = b;

                return dynamicObject;
            };
            Expression<Func<AbpUsers,AbpUsers, dynamic>> expression1 = (a,b) => func(a,b);
            using (var context = new BlocksEntities1())
            {
                
                var linq = context.AbpUsers.Join(context.AbpUsers, a => a.Id, expression);

//                var linq = context.AbpUsers.Join(context.AbpUsers, a => a.Id, expression,(a,b) => new {a.AbpUsers2, a.AbpUsers4, b});
//                var linq2 = linq.Join(contexdt.AbpUsers, a => expression.Compile()(a.AbpUsers4), b => b.Id,
//                    (a, b) => new {a.AbpUsers2, a.AbpUsers4, b});
//                var linq2 = context.AbpUsers.Join(linq, a => a.Id, b => b.b.CreatorUserId, (a, b) => new { a, b })
//                    .Select(t => t.a);

                  var strLinq = linq.ToString();
//                var dataLinq = linq.ToList();
            }
        }
    }



    public static class EFLinqExtensions
    {
        public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer,
            IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {
            return outer.Provider.CreateQuery<TResult>(Expression.Call(null,
                GetMethodInfo(Queryable.Join, outer, inner, outerKeySelector, innerKeySelector, resultSelector),
                outer.Expression, GetSourceExpression(inner), (Expression) Expression.Quote(outerKeySelector),
                (Expression) Expression.Quote(innerKeySelector), (Expression) Expression.Quote(resultSelector)));
        }

        public static IQueryable<dynamic> Join<TOuter, TInner, TKey>(this IQueryable<TOuter> outer,
            IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector)
        {
            
            var sourceProperties = new Dictionary<string, Type>()
            {
                { "AbpUsers2",typeof(AbpUsers) }, { "AbpUsers4",typeof(AbpUsers) }, 

                
            };
            Type dynamicType = LinqRuntimeTypeBuilder.GetDynamicType(sourceProperties);
 
            NewExpression selector = Expression.New(dynamicType.GetConstructor(Type.EmptyTypes));
            var lambdaLInq = Expression.Lambda<Func<TOuter,TInner, dynamic>>(selector);
            return outer.Provider.CreateQuery<dynamic>(Expression.Call(null,
               
                GetMethodInfo1<IQueryable<TOuter>,IEnumerable<TInner>,Expression<Func<TOuter, TKey>>,Expression<Func<TInner, TKey>>,Expression<Func<TOuter, TInner, dynamic>>,IQueryable<dynamic>>(
                    Queryable.Join , outer, inner, outerKeySelector, innerKeySelector),
                outer.Expression, GetSourceExpression(inner), (Expression) Expression.Quote(outerKeySelector),
                (Expression) Expression.Quote(innerKeySelector), (Expression) Expression.Quote(selector)));
        }
        //IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector
        private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6> f, T1 unused1,
            T2 unused2, T3 unused3, T4 unused4, T5 unused5)
        {
            return f.Method;
        }
        
        private static MethodInfo GetMethodInfo1<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6> f, T1 unused1,
            T2 unused2, T3 unused3, T4 unused4)
        {
            return f.Method;
        }

        private static Expression GetSourceExpression<TSource>(IEnumerable<TSource> source)
        {
            var queryable = source as IQueryable<TSource>;
            if (queryable != null)
                return queryable.Expression;
            return Expression.Constant(source, typeof(IEnumerable<TSource>));
        }
    }
}