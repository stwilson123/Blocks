using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.BussnessDomainModule;
using Blocks.BussnessDomainModule.RPC;
using Blocks.BussnessDTOModule.MasterData;
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


        public string GetValueOverride()
        {
            return testDomain.GetValueOverride();
        }
        public string Add(string id)
        {
            return "";
            //  return testDomain.AddValue(Guid.NewGuid().ToString());
        }

       

      

        public List<string> ProxFunction(BussnessDTOModule.MasterData.ProxModel input)
        {
            testDomain.Update(null);
            return input.dic.Select(t => t.Key).ToList();
        }
    }
}