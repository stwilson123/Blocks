using System;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.BussnessEntityModule;
using Blocks.BussnessRespositoryModule;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Event;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Utility.SafeConvert;

namespace Blocks.BussnessDomainModule.MasterData
{
    public class MasterDataDomainEvent : Framework.Domain.Service.IDomainService
    {
        public IDomainEventBus EventBus { get; set; }

        public Localizer L { get; set; }

        public MasterDataDomainEvent(ITestRepository testRepository, ITest2Repository test2Repository)
        {

             this.testRepository = testRepository;
            this.test2Repository = test2Repository;
        }

        private ITestRepository testRepository { get; set; }

        private ITest2Repository test2Repository { get; set; }


        public virtual string Add(MasterData data)
        {
//            var newMasterData = new TESTENTITY()
//            {
//                STRING = data.city,
//                ISACTIVE = SafeConvert.ToInt64(data.isActive),
//                COMMENT = data.comment,
//                TESTENTITY2ID = data.combobox,
//                REGISTERTIME =data.registerTime
//            };
//            var Id = testRepository.Insert(newMasterData).Id;
//            return Id;
            testRepository.Update(t => t.Id == "8c60362e-8956-429a-b6f4-23b3524d926b", t => 
                new TESTENTITY()
                {
                    
                    COMMENT = DateTime.Now.ToString("HH:mm:ss tt zz")
                }
            );

            //test2Repository.Update(t => t.Id == "123",t => new TESTENTITY2() {
            //     Text = DateTime.Now.ToString("HH:mm:ss tt zz")

            //});
         
            return null;
        }
        
        
        public virtual  PageList<PageResult>  GetPageList(SearchModel search)
        {
            EventBus.Trigger(new TaskEventData {id = "123123"});

            var a =  testRepository.GetPageList(search);
            return a;
        }


        public virtual void TestException()
        {
            throw new BlocksBussnessException("101", L("TestException"), null);

        }
    }
}