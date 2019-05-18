using Blocks.Framework.AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization.Convert
{
    public class LocalizedConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ILocalizableString).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            writer.WriteValue(value.AutoMapTo<string>());
        }
    }
}
