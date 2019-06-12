using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blocks.Framework.Auditing
{
    /// <summary>
    /// Decides which properties of auditing class to be serialized
    /// </summary>
    public class AuditingContractResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly List<Type> _ignoredTypes;
        private readonly IDictionary<Type, Func<object, string>> _typeConverts;

        public AuditingContractResolver(List<Type> ignoredTypes,IDictionary<Type,Func<object,string>> typeConverts)
        {
            _ignoredTypes = ignoredTypes;
            _typeConverts = typeConverts;
        }

//        protected override JsonStringContract CreateStringContract(Type objectType)
//        {
//            foreach (var typeConvert in _typeConverts)
//            {
//                
//                if (typeConvert.Key.GetTypeInfo().IsAssignableFrom(objectType))
//                {
//                   var jsonContract = new JsonContract()
//                    break;
//                }
//            }
//            return base.CreateStringContract(objectType);
//        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member.IsDefined(typeof(DisableAuditingAttribute)) || member.IsDefined(typeof(JsonIgnoreAttribute)))
            {
                property.ShouldSerialize = instance => false;
            }

            foreach (var ignoredType in _ignoredTypes)
            {
                if (ignoredType.GetTypeInfo().IsAssignableFrom(property.PropertyType))
                {
                    property.ShouldSerialize = instance => false;
                    break;
                }
            }
            
            foreach (var typeConvert in _typeConverts)
            {
     
                if (typeConvert.Key.GetTypeInfo().IsAssignableFrom(property.PropertyType))
                {
                    property.Converter = new AuditingConverter(typeConvert.Value);
                    break;
                }
            }
          
            return property;
        }
        
    }

    public class AuditingConverter : JsonConverter
    {
        private readonly Func<object, string> _func;


        public AuditingConverter(Func<object,string> func)
        {
            _func = func;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(_func(value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
