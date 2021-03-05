using System;
using Blocks.Framework.Navigation;
using Xunit;

namespace Blocks.Framework.Web.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //(new NavigationDefinition(null)).Items[0].GetUniqueId();
        }
        
        [Fact]
        public void jsonDeserialize_PrivateProperty_Object()
        {

            var deserializedObj = Newtonsoft.Json.JsonConvert.DeserializeObject<jsonObj>("{'str':'123'}");




        }
    }
    
    public class jsonObj : IJsonObj
    {
        public string str { get; set; }
    }

    public interface IJsonObj
    {
         string str { get; set; }
    }
}