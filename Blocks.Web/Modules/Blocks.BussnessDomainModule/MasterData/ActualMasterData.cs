using System.Collections.Generic;
using System.Linq;
using Blocks.BussnessDomainModule.RPC;
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
    public class ActualMasterData : Framework.Domain.Service.IDomainService
    {
        public IDomainEventBus EventBus { get; set; }

        public Localizer L { get; set; }

        public TestRPC testRPC { get; set; }

        public ITest2Rpc Test2Rpc { get; set; }

        public ActualMasterData(ITestRepository testRepository, ITest2Repository test2Repository)
        {

             this.testRepository = testRepository;
            this.test2Repository = test2Repository;
        }

        private ITestRepository testRepository { get; set; }

        private ITest2Repository test2Repository { get; set; }


        public  string Add(MasterData data)
        {
            var newMasterData = new TESTENTITY()
            {
                STRING = data.city,
                ISACTIVE = SafeConvert.ToInt64(data.isActive),
                COMMENT = data.comment,
                TESTENTITY2ID = data.combobox,
                REGISTERTIME =data.registerTime,
                
            };
            var Id = testRepository.Insert(newMasterData).Id;
            return Id;
//            testRepository.Update(t => t.Id == "57627fde-0332-4db0-9036-ce3ec5e48496", t => 
//                new TESTENTITY()
//                {
//                    
//                    COMMENT = DateTime.Now.ToString("HH:mm:ss")
//                }
//            );
            //throw new Exception();
            //test2Repository.Update(t => t.Id == "123",t => new TESTENTITY2() {
            //     Text = DateTime.Now.ToString("HH:mm:ss tt zz")

            //});
         
            return null;
        }
        
        
        public    PageList<PageResult>  GetPageList(SearchModel search)
        {
            EventBus.Trigger(new TaskEventData {id = "123123"});

            var a =  testRepository.GetPageList(search);
            return a;
        }


        public   void TestException()
        {
            throw new BlocksBussnessException("101", L("TestException"), null);

        }

        public   string ProxTest(string input)
        {
            return testRPC.ProxFunction(new RPC.ProxModel() { dic = new Dictionary<string, string>() {

                { input,input}
            } }).FirstOrDefault();
        }
    }
}