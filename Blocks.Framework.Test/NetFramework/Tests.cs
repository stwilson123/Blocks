using System.Linq;
using Blocks.Framework.Data.Paging;
using Newtonsoft.Json;
using Xunit;

namespace Blocks.Framework.Test.NetFramework
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            var interfaces = typeof(TestInterface).GetInterfaces();
            Assert.True(interfaces.Contains(typeof(IBaseInterface)));
            Assert.True(interfaces.Contains(typeof(IInterface)));

        }
        
        
        
        
    }

    interface IBaseInterface
    {
        
    }

    interface IInterface : IBaseInterface
    {
        
    }
    public class BaseTest :IInterface
    {
        
    }
    public class TestInterface : BaseTest
    {
        
    }
}