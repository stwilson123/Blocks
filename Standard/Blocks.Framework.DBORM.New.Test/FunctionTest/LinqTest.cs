using EntityFramework.Test.Model;
using Xunit;

namespace EntityFramework.Test.FunctionTest
{
    public class LinqTest: BlocksTestBase
    {
        [Fact]
        public void Distinct_Test()
        {
            var rep =  Resolve<ITestRepository>();

            var firstData = rep.GetTestEntityDistinct();
        }
    }
}