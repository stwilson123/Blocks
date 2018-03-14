using System;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Json;
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
    }
}