using System.Threading;
using EntityFramework.Test.Model;
using Xunit;

namespace EntityFramework.Test.FunctionTest
{
    public class RepositoryTest : BlocksTestBase
    {
        [Fact]
        public void FirstOrDefault()
        {
            var rep =  Resolve<TestRepository>();
            var testEntity = rep.FirstOrDefault();
        }
        
    }
}