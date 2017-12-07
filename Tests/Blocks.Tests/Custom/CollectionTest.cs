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
    }
}