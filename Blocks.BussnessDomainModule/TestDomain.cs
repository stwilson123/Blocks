using System;
using Abp.Domain.Services;
using Blocks.BussnessEntityModule;
using Blocks.BussnessRespositoryModule;

namespace Blocks.BussnessDomainModule
{
    public class TestDomain : IDomainService
    {
        public TestDomain(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        private ITestRepository testRepository { get; set; }

        public virtual string GetValue(string value)
        {
            return testRepository.GetValue(value);
        }
        
        
        public virtual Guid AddValue(Guid value)
        {
             
            return testRepository.Insert(new TestEntity()).Id;
        }
    }
}