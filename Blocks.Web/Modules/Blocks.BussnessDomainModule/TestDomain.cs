﻿using System;
using Abp.Domain.Services;
using Abp.Events.Bus;
using Blocks.BussnessEntityModule;
using Blocks.BussnessRespositoryModule;
using Blocks.Framework.Event;

namespace Blocks.BussnessDomainModule
{
    public class TestDomain : IDomainService
    {
        public IDomainEventBus EventBus { get; set; }
        public TestDomain(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        private ITestRepository testRepository { get; set; }

        public virtual string GetValue(string value)
        {
           // EventBus.Trigger(new TaskEventData {id = "123123"});
            return testRepository.GetValue(value);
        }
        
        
        public virtual Guid AddValue(Guid value)
        {
             
            return testRepository.Insert(new TestEntity()).Id;
        }
    }
    
    public class TaskEventData : DomainEventData
    {
        public string id { get; set; }
    } 
}