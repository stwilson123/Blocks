using System;
using Blocks.BussnessDomainModule;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public class TestAppService : AppService,ITestAppService
    {
        public TestAppService(TestDomain testDomain)
        {
            this.testDomain = testDomain;
        }

        private TestDomain testDomain { get; set; }
        
        public string GetValue(string a)
        {
            return testDomain.GetValue(a);
        }

        public string Add(string id)
        {
            return "";
            //  return testDomain.AddValue(Guid.NewGuid().ToString());
        }
    }
}