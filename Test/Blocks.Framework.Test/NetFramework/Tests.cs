using System.Linq;
using Blocks.Framework.Data.Paging;
using Newtonsoft.Json;
using Xunit;

namespace Blocks.Framework.Test.NetFramework
{
    public class Tests
    {
        [Fact]
        public void TestGetAllInterfaces()
        {
            var interfaces = typeof(TestInterface).GetInterfaces();
            Assert.True(interfaces.Contains(typeof(IBaseInterface)));
            Assert.True(interfaces.Contains(typeof(IInterface)));

        }
        
        [Fact]
        public void TestDyanmicJson()
        {
            var sourceObj = new sourceObj() {a = "123"};
            var jsonStr = JsonConvert.SerializeObject(sourceObj);
            var transferObj = JsonConvert.DeserializeObject<object>(jsonStr);
            Assert.Equal(jsonStr,JsonConvert.SerializeObject(transferObj));

        }

        class sourceObj
        {
            public string a { get; set; }
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