using System.Threading;
using EntityFramework.Test.Model;
using Xunit;
using System;

namespace EntityFramework.Test.FunctionTest
{
    public class RepositoryTest : BlocksTestBase
    {
        [Fact]
        public void FirstOrDefault()
        {
            var rep =  Resolve<TestRepository>();
            var testEntity = rep.FirstOrDefault(t => t.Id == Guid.NewGuid());
        }
        
    }
}