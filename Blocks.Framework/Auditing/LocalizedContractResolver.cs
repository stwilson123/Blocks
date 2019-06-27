using System;
using System.Collections.Generic;
using System.Reflection;
using Blocks.Framework.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blocks.Framework.Auditing
{
    public class LocalizedContractResolver : AuditingContractResolver
    {

        public LocalizedContractResolver(List<Type> ignoredTypes, IDictionary<Type, Func<object, string>> typeConverts) : base(ignoredTypes, typeConverts)
        {
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            
            return base.CreateProperties(type, memberSerialization);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var attribute = member.GetCustomAttribute<LocalizedDescriptionAttribute>();
            if (attribute == null)
                property.ShouldSerialize = o => false;

            property.PropertyName = attribute?.Name ?? property.PropertyName;
            
            return property;
        }
    }
}