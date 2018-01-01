using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security;
using Blocks.Framework.DBORM.Linq;
using Xunit;

public class MyClass12
{
    class TestMemberInitClass
    {
        public TestInput sample { get; set; }
    }

   public  class TestInput
    {
        public int sample { get; set; }
    }

    [Fact]
    public void Main()
    {
        // This expression creates a new TestMemberInitClass object
        // and assigns 10 to its sample property.
        var sourceProperties = new Dictionary<string, Type>()
        {
            { "PTestEntity",typeof(TestInput) }
        };
        Type dynamicType = LinqRuntimeTypeBuilder.GetDynamicType(sourceProperties);
        
        
       // var pExpresion = Expression.Parameter(typeof(TestInput), "i");
        var paramerExpression = sourceProperties.Select(t =>  Expression.Parameter(t.Value, t.Key));
        var memberInitExpression = paramerExpression.Select(t => Expression.Bind(dynamicType.GetMember(t.Name).FirstOrDefault(), t));

        Expression testExpr = Expression.MemberInit(
            Expression.New(dynamicType),
            memberInitExpression
        );

        // The following statement first creates an expression tree,
        // then compiles it, and then runs it.
        var testLambda = Expression.Lambda<Func<TestInput,dynamic>>(testExpr,paramerExpression);
        var test = testLambda.Compile()(new TestInput(){ sample  = 12});
        var a = test.sample.sample;
        Console.WriteLine(test.sample);
    }
}