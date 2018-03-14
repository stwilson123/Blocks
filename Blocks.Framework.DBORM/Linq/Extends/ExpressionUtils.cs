using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.Linq.Extends
{
    public static class ExpressionUtils
    {
        //public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(Expression<Func<TSource, bool>> source)
        //{
        //    var parameter = Expression.Parameter(typeof(TTarget), source.Parameters[0].Name);
        //    var body = new ParameterConverter { source = source.Parameters[0], target = parameter }.Visit(source.Body);
        //    return Expression.Lambda<Func<TTarget, bool>>(body, parameter);
        //}

        public static LambdaExpression Convert<TSource, TOut>(Expression<Func<TSource, TOut>> source,Type TTarget)
        {
            var parameter = Expression.Parameter(TTarget, source.Parameters[0].Name);
            var body = new ParameterConverter { source = source.Parameters[0], target = parameter }.Visit(source.Body);
            return Expression.Lambda(body, parameter);
        }

        public static LambdaExpression Convert(LambdaExpression source, Type TTarget)
        {
            var parameter = Expression.Parameter(TTarget, source.Parameters[0].Name);
            var body = new ParameterConverter { source = source.Parameters[0], target = parameter }.Visit(source.Body);
            return Expression.Lambda(body, parameter);
        }

        class ParameterConverter : ExpressionVisitor
        {
            public ParameterExpression source;
            public ParameterExpression target;
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == source ? target : base.VisitParameter(node);
            }
            protected override Expression VisitMember(MemberExpression node)
            {
                var paramNode = node.Expression.NodeType == ExpressionType.Parameter ? (ParameterExpression)node.Expression : null;
                if (paramNode != null)
                {
                    var newMember = Expression.PropertyOrField(target, paramNode.Name);
                    var newMember1 = Expression.PropertyOrField(newMember, node.Member.Name);
                    return newMember1;
                }

                return node.Expression == source ? Expression.PropertyOrField(target, node.Member.Name) : base.VisitMember(node);
            }
        }
    }


    public static class IQueryableExtend
    {
        public static IQueryable Select(this IQueryable source, LambdaExpression lambda)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(lambda, nameof(lambda));

            var callExpression = Expression.Call(typeof(Queryable), nameof(Queryable.Select), new[] { source.ElementType, lambda.Body.Type }, source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery(callExpression);
        }

        public static IOrderedQueryable OrderBy<T>(this IQueryable<T> source, LambdaExpression lambda)
        {
            
            return ((IQueryable)source).OrderBy(lambda);
        }

        public static IOrderedQueryable OrderBy(this IQueryable source, LambdaExpression lambda)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(lambda, nameof(lambda));

            var callExpression = Expression.Call(typeof(Queryable), nameof(Queryable.OrderBy), new[] { source.ElementType, lambda.ReturnType }, source.Expression, Expression.Quote(lambda));
            return (IOrderedQueryable)source.Provider.CreateQuery(callExpression);
        }

    }
}
