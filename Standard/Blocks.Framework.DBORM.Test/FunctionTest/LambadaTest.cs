using System;
using System.Linq.Expressions;
using Blocks.Framework.DBORM.Linq.Extends;
using Xunit;

namespace EntityFramework.Test.FunctionTest
{
    public class LambadaTest
    {
        [Fact]
        public void DefaultConfigIsDetectChanges()
        {
            Expression<Func<LambadaTest, LambadaTest>> expression = (input) => new LambadaTest();
            var a = new { };
            ExpressionUtils.Convert(expression, a.GetType());
        }
     
    }
}