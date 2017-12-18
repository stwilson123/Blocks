using System;
using System.Collections.Concurrent;
using Abp.Collections.Extensions;
using NSubstitute.Routing.Handlers;
using Xunit;

namespace Blocks.Tests.Custom
{
    public class CollectionTest
    {
        [Fact]
        public void Test()
        {
            var dic = new ConcurrentDictionary<string, string>();
            try
            {
                dic.GetOrAdd("t", v =>
                {
                    Exception(v);
                    return v;
                });

            }
            catch (Exception ex)
            {

            }
            try
            {
                dic.GetOrAdd("t", v =>
                {

                    Exception(v);
                    return v;
                });

            }
            catch (Exception ex)
            {

            }

           
        }

        public void Exception(string str)
        {
            throw  new Exception(str);

        }

        [Fact]
        public void Test1()
        {
            var tes = new class1();
           var ass= typeof(IInterface11).IsAssignableFrom(typeof(class1));
        }
        
        public class class1 :IInterface2
        {
            
        }
        
        public interface IInterface1
        {
            
        }
        public interface IInterface11
        {
            
        }
        
        
        public interface IInterface2 : IInterface1,IInterface11
        {
            
        }
    }
}