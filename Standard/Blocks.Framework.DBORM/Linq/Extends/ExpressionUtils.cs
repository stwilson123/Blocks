using Abp;
using Blocks.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            var body = new ParameterConverter { source = source.Parameters[0], Target = parameter }.Visit(source.Body);
            return Expression.Lambda(body, parameter);
        }

        public static LambdaExpression Convert(LambdaExpression source, Type TTarget)
        {
            var parameter = Expression.Parameter(TTarget, source.Parameters[0].Name);
            var body = new ParameterConverter { source = source.Parameters[0], Target = parameter }.Visit(source.Body);
            return Expression.Lambda(body, parameter);
        }

        public static LambdaExpression GroupConvert(LambdaExpression source, Type TTarget,string parameterName)
        {
            var parameter = Expression.Parameter(TTarget, parameterName);
            var body = new GroupSelectParameterConver { source = source.Parameters[0], Target = parameter }.Visit(source.Body);
            return Expression.Lambda(body, parameter);
        }

        class ParameterConverter : ExpressionVisitor
        {
            public ParameterExpression source;
            private ParameterExpression target;
            public ParameterExpression Target {
                get => target;
                set {
                    target = value;
                    SetTargetElement(target);
                }
            }

            internal virtual void SetTargetElement(ParameterExpression Target)
            {

            }

           
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == source ? Target : base.VisitParameter(node);
            }
            protected override Expression VisitMember(MemberExpression node)
            {
                var paramNode = node.Expression.NodeType == ExpressionType.Parameter ? (ParameterExpression)node.Expression : null;
                if (paramNode != null)
                {
                    var newMember = Expression.PropertyOrField(Target, paramNode.Name);
                    
                    var newMember1 = Expression.PropertyOrField(newMember, node.Member.Name);
                    return newMember1;
                }

                return node.Expression == source ? Expression.PropertyOrField(Target, node.Member.Name) : base.VisitMember(node);
            }
        }



        class GroupSelectParameterConver : ParameterConverter
        {
            public ParameterExpression TargetElementParameter { set; get; }

            internal override void SetTargetElement(ParameterExpression target)
            {
                TargetElementParameter = Expression.Parameter(target.Type.GetGenericArguments().LastOrDefault()) ?? throw new BlocksDBORMException(StringLocal.Format("generic is null"));

            }
            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                var result = default(Expression);
                var paramCount = node.Method.GetParameters().Count();
                if (paramCount == 1)
                {
                    var igroupOpMethod = node.Method.GetGenericMethodDefinition().MakeGenericMethod(new Type[] { TargetElementParameter.Type });
                    result = Expression.Call(null, igroupOpMethod, Target);
                }
                if (paramCount == 2)
                {

                    //ParameterExpression.
                    // var t1 = ExpressionUtils.Convert((LambdaExpression)node.Arguments.LastOrDefault(), TargetElement.Type);
                  //  var t = new ParameterConverter() { source = Expression.Parameter(node.Method.GetParameters().LastOrDefault()),Target = TargetElement }.Visit(node.Arguments.LastOrDefault());
                    if(!CheckIfAnonymousType(TargetElementParameter.Type))
                      result = Expression.Call(null, node.Method, Target, node.Arguments.LastOrDefault());
                    else
                    {
                        var t = ExpressionUtils.Convert((LambdaExpression)node.Arguments.LastOrDefault(), TargetElementParameter.Type);

                        //MethodInfo composePartsMethod = typeof(Enumerable).GetMethod(node.Method.Name,  );

                        var igroupOpMethod = node.Method.GetGenericMethodDefinition().MakeGenericMethod(new Type[] { TargetElementParameter.Type });
                        result = Expression.Call(null, igroupOpMethod, Target, t);
                        // result = Expression.Call(null, igroupOpMethod, Target, t);
                    }
                }
                return result;
            }

            private static bool CheckIfAnonymousType(Type type)
            {
                if (type == null)
                    throw new ArgumentNullException("type");

                return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                    && type.IsGenericType && type.Name.Contains("AnonymousType")
                    && (type.Name.StartsWith("<>"))
                    && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
            }
        }
    }


    public static class IQueryableExtend
    {
        public static IQueryable GroupBy(this IQueryable source, LambdaExpression lambda)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(lambda, nameof(lambda));

          //  Expression.Call(typeof(Queryable), "GroupBy", new Type[2]
          // {
          //source.ElementType,
          //lambda.Body.Type
          // }, source.Expression, (Expression)Expression.Quote((Expression)lambda), (Expression)Expression.Constant((object)equalityComparer, type))


            var callExpression = Expression.Call(typeof(Queryable), nameof(Queryable.GroupBy), new[] { source.ElementType, lambda.Body.Type }, source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery(callExpression);
        }


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

        public static IOrderedQueryable OrderByDescending<T>(this IQueryable<T> source, LambdaExpression lambda)
        {

            return ((IQueryable)source).OrderByDescending(lambda);
        }

        public static IOrderedQueryable OrderByDescending(this IQueryable source, LambdaExpression lambda)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(lambda, nameof(lambda));

            var callExpression = Expression.Call(typeof(Queryable), nameof(Queryable.OrderByDescending), new[] { source.ElementType, lambda.ReturnType }, source.Expression, Expression.Quote(lambda));
            return (IOrderedQueryable)source.Provider.CreateQuery(callExpression);
        }


        public static IOrderedQueryable ThenBy<T>(this IQueryable<T> source, LambdaExpression lambda)
        {

            return ((IQueryable)source).ThenBy(lambda);
        }

        public static IOrderedQueryable ThenBy(this IQueryable source, LambdaExpression lambda)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(lambda, nameof(lambda));

            var callExpression = Expression.Call(typeof(Queryable), nameof(Queryable.ThenBy), new[] { source.ElementType, lambda.ReturnType }, source.Expression, Expression.Quote(lambda));
            return (IOrderedQueryable)source.Provider.CreateQuery(callExpression);
        }

        public static IOrderedQueryable ThenByDescending<T>(this IQueryable<T> source, LambdaExpression lambda)
        {

            return ((IQueryable)source).ThenByDescending(lambda);
        }

        public static IOrderedQueryable ThenByDescending(this IQueryable source, LambdaExpression lambda)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(lambda, nameof(lambda));

            var callExpression = Expression.Call(typeof(Queryable), nameof(Queryable.ThenByDescending), new[] { source.ElementType, lambda.ReturnType }, source.Expression, Expression.Quote(lambda));
            return (IOrderedQueryable)source.Provider.CreateQuery(callExpression);
        }

    }
}
