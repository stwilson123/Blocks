﻿using System;
using Abp.Domain.Services;
using Abp.Events.Bus;
using Blocks.BussnessApplicationModule.TestAppService.DTO;
using Blocks.BussnessEntityModule;
using Blocks.BussnessRespositoryModule;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Data.Paging;
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
            EventBus.Trigger(new TaskEventData {id = "123123"});
            testRepository.FirstOrDefault(t => t.Id == "123");
            return testRepository.GetValue(value);
        }
        
        
        public virtual string Add(MasterData.MasterData data)
        {

            var newMasterData =  data.AutoMapTo<TESTENTITY>();
            return testRepository.Insert(newMasterData).Id;
        }
        
        public virtual string GetList(string value)
        {
            EventBus.Trigger(new TaskEventData {id = "123123"});
            testRepository.FirstOrDefault(t => t.Id == "123");
            return testRepository.GetValue(value);
        }
        
        
        public virtual  PageList<PageResult>  GetPageList(SearchModel search)
        {
            var a =  testRepository.GetPageList(search);
            return a;
        }
    }
    
    public class TaskEventData : DomainEventData
    {
        public string id { get; set; }
    } 
}