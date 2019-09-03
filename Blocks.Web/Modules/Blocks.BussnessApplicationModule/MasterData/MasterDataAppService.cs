using System;
using System.Threading.Tasks;
using Abp.Events.Bus;
using Blocks.BussnessDomainModule;
using Blocks.BussnessDomainModule.MasterData;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Security;

namespace Blocks.BussnessApplicationModule.MasterData
{
    public class MasterDataAppService : AppService,IMasterDataAppService
    {
        private readonly ActualMasterData _actualMasterData;
        public IUserContext UserContext { get; set; }
        public Localizer L { get; set; }

        public MasterDataAppService(MasterDataDomainEvent masterDataDomain,ActualMasterData actualMasterData)
        {
            _actualMasterData = actualMasterData;
            this.masterDataDomain = masterDataDomain;
        }

        private MasterDataDomainEvent masterDataDomain { get; set; }
        
        
        [LocalizedDescription("query")]
        public  Task<PageList<PageResult>>  GetPageList([LocalizedDescription("START_ABP")]SearchModel a)
        {
            

            return Task.FromResult(masterDataDomain.GetPageList(a));
            //return testDomain.GetValue(a);
        }

        public void Add(MasterDataInfo masterDataInfo)
        {
            var result =  masterDataDomain.Add(masterDataInfo.AutoMapTo<BussnessDomainModule.MasterData.MasterData>());

          
           // return result;


        }

        public void TestException()
        {
            var lException = L("TestException").AutoMapTo<string>();
            _actualMasterData.TestException();
            masterDataDomain.TestException();

           
            throw new BlocksBussnessException("101", L("TestException"), null);

            // return result;


        }

        public string ProxTest()
        {
            return masterDataDomain.ProxTest("abc");
        }
 
    }
}