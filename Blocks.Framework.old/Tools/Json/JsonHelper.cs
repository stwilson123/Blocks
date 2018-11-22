using System;
using Newtonsoft.Json;

namespace Blocks.Framework.Tools.Json
{
    public class JsonHelper
    {
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        
        
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}