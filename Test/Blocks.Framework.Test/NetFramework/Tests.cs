using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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

        [Fact]
        public void TestDictionaryDefault()
        {
            IDictionary<string,string> iDic = new Dictionary<string, string>();
            var defaultValue = iDic.FirstOrDefault();
            Assert.NotNull(defaultValue);
            Assert.Null(defaultValue.Key);

            Assert.Null(defaultValue.Value);

        }

        [Fact]
        public void TestEnum()
        {
            Assert.Equal(typeof(int), Enum.GetUnderlyingType(typeof(permissionDefault)));
            Assert.Equal(typeof(byte), Enum.GetUnderlyingType(typeof(permission)));
            
            var a = ( permissionDefault.Add & permissionDefault.Delete).ToString();

        }
        [Fact]
        public void TestDyanmics()
        {
            dynamic testDy = new ExpandoObject();
            var dicDy = (ICollection<KeyValuePair<string, object>>) testDy;
            dicDy.Add(new KeyValuePair<string, object>("a",123));
            testDy.b = 2;

            var DyA = testDy.a;
            var DyB = testDy.b;
            var propertise = testDy.GetType().GetProperties();

        }
        class sourceObj
        {
            public string a { get; set; }
        }
        
    }
    [Flags]
    enum permissionDefault
    {
        Add = 0,
        Edit = 0x0001,
        Delete =0x0002
    }
    enum permission : byte
    {
        Add,
        Edit,
        Delete
    }
    interface IBaseInterface
    {
        
    }

    interface IInterface : IBaseInterface
    {
        
    }
    public class BaseTest :IInterface
    {
        protected int i { get; set; }
    }
    public class TestInterface : BaseTest
    {
        
    }
}