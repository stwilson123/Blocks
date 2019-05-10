using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Blocks.Framework.Test.NetFramework
{
    public class TaskTest
    {
        
        [Fact]
        public void TestTaskResultIsAsync()
        {

            var valueObj = new valueClass()
            {
                iValue = 1
            };

            var tasks = new List<Task<valueClass>> { };
            tasks.Add(Test1( new valueClass()
            {
                iValue = 1
            }));
            valueObj.iValue = 2;
            tasks.Add(Test1( new valueClass()
            {
                iValue = 2
            }));

            
            var tasksResult = Task.WhenAll(tasks.ToArray());
            var results = tasksResult.Result;
        }
        
        public Task<valueClass> Test1(valueClass value)
        {
          //  Thread.Sleep(1 * 1000);
            return Task.Factory.StartNew(() => value);

           // return Task.Factory.StartNew((v) => (valueClass)v,value);
        }

        public Task<int> Test2()
        {
            Thread.Sleep(1 * 1000);
            return Task.FromResult(2);
        }


        public class valueClass
        {
            public int iValue { get; set; }
        }
    }
}