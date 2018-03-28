using System;
using Blocks.BussnessDomainModule;
using Blocks.BussnessDomainModule.MasterData;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessApplicationModule.MasterData
{
    public class MasterDataAppService : AppService,IMasterDataAppService
    {
        public MasterDataAppService(MasterDataDomainEvent masterDataDomain)
        {
            this.masterDataDomain = masterDataDomain;
        }

        private MasterDataDomainEvent masterDataDomain { get; set; }
        
        public  PageList<PageResult>  GetPageList(SearchModel a)
        {
            

            return masterDataDomain.GetPageList(a);
            //return testDomain.GetValue(a);
        }

        public string Add(MasterDataInfo masterDataInfo)
        {
            
            return masterDataDomain.Add(masterDataInfo.AutoMapTo<BussnessDomainModule.MasterData.MasterData>());
        }
    }
}