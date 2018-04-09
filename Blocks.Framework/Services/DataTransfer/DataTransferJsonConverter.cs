using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blocks.Framework.Services.DataTransfer
{
    
    
    public class JsonAttribuateContractResolver : DefaultContractResolver
    {

        protected override JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
        {
            var property = base.CreatePropertyFromConstructorParameter(matchingMemberProperty, parameterInfo);
            var attributeArray = parameterInfo.GetCustomAttributes(typeof(DataTransferAttribute), false).FirstOrDefault();
            if (attributeArray != null)
            {
                property.PropertyName = ((DataTransferAttribute)attributeArray).PropertyName;
            }
            return property;
        }


        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var attributeArray = member.GetCustomAttributes(typeof(DataTransferAttribute), false).FirstOrDefault();
            if (attributeArray != null)
            {
                property.PropertyName = ((DataTransferAttribute)attributeArray).PropertyName;
            }
            return property;
        }
    }

    internal static class CachedAttributeGetter<T> where T : Attribute
    {
        private static readonly ThreadSafeStore<object, T> TypeAttributeCache = new ThreadSafeStore<object, T>((object provider) =>
        {
            Type type = provider as Type;
            if (type != null)
            {
                return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            }
            MemberInfo memberInfo = provider as MemberInfo;
            if (memberInfo != null)
            {
                return memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            }
            PropertyInfo propertyInfo = provider as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            }
            return (T)provider.GetType().GetCustomAttributes(typeof(T), false).FirstOrDefault();


        });

        public static T GetAttribute(object type)
        {
            return TypeAttributeCache.Get(type);
        }
    }

    internal class ThreadSafeStore<TKey, TValue>
    {
        private readonly object _lock = new object();
        private Dictionary<TKey, TValue> _store;
        private readonly Func<TKey, TValue> _creator;

        public ThreadSafeStore(Func<TKey, TValue> creator)
        {
            if (creator == null)
            {
                throw new ArgumentNullException("creator");
            }

            _creator = creator;
            _store = new Dictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            TValue value;
            if (!_store.TryGetValue(key, out value))
            {
                return AddValue(key);
            }

            return value;
        }

        private TValue AddValue(TKey key)
        {
            TValue value = _creator(key);

            lock (_lock)
            {
                if (_store == null)
                {
                    _store = new Dictionary<TKey, TValue>();
                    _store[key] = value;
                }
                else
                {
                    // double check locking
                    TValue checkValue;
                    if (_store.TryGetValue(key, out checkValue))
                    {
                        return checkValue;
                    }

                    Dictionary<TKey, TValue> newStore = new Dictionary<TKey, TValue>(_store);
                    newStore[key] = value;

#if HAVE_MEMORY_BARRIER
                    Thread.MemoryBarrier();
#endif
                    _store = newStore;
                }

                return value;
            }
        }
    }

}