using Blocks.BussnessDomainModule;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public class TestAppService : ITestAppService
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
    }
}