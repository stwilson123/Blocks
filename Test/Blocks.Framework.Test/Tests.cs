using System;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Json;
using Blocks.Framework.Test.Interface;
using Blocks.Framework.Test.Model;
using Newtonsoft.Json;
using Xunit;

namespace Blocks.Framework.Test
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);


            var a = JsonConvert.DeserializeObject<Filters>(
                "{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"COLLECT_STATION_NO\",\"op\":\"eq\",\"data\":\"11\"},{\"field\":\"COLLECT_STATION_NO\",\"op\":\"eq\",\"data\":\"12\"}],\"groups\":[{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"COLLECT_STATION_NO\",\"op\":\"eq\",\"data\":\"1\"}],\"groups\":[{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"BDTA_WORKPROCEDURE.WORKPROCEDURE_TYPE\",\"op\":\"eq\",\"data\":\"12\"}],\"groups\":[]}]}]}");
        }
        
        
        
        [Fact]
        public void TestIsClass()
        {
            Assert.True(new Tests().GetType().IsClass);
            
            Assert.False(typeof(int).IsClass);
            Assert.False(typeof(decimal).IsClass);
            Assert.False(typeof(int?).IsClass);
            Assert.False(typeof(char).IsClass);
            Assert.True(string.Empty.GetType().IsClass && string.Empty is string);
        }


        [Fact]
        public void TestTransfer()
        {
            ILog log = new NullLog();
            var newLog = (NullLog)log;
        }


        [Fact]
        public void StringSplit()
        {
            
            
            
            Assert.Equal(new string[]{ "aa","ab" },"aa||ab".Split("|",2));
        }
    }
}