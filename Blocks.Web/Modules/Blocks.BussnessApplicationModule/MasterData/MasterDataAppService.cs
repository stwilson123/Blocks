using System;
using Blocks.BussnessDomainModule;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public class MasterDataAppService : AppService,IMasterDataAppService
    {
        public MasterDataAppService(TestDomain testDomain)
        {
            this.testDomain = testDomain;
        }

        private TestDomain testDomain { get; set; }
        
        public string GetPageList(string a)
        {
            return testDomain.GetValue(a);
        }

        public string Add(string id)
        {
            return testDomain.AddValue(Guid.NewGuid().ToString());
        }
    }
}