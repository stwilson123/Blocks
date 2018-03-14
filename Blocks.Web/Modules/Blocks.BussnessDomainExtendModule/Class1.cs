using System;
using Blocks.BussnessDomainModule;
using Blocks.BussnessRespositoryModule;
using Blocks.Framework.Event;

namespace Blocks.BussnessDomainExtendModule
{
    public class Class1 : IDomainEventHandler<TaskEventData>
    {
        public Class1(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        private ITestRepository testRepository { get; set; }
        public void HandleEvent(TaskEventData eventData)
        {
            ;
        }

        
    }
}