using System;
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
        private IUserContext _userContext;
        public MasterDataAppService(MasterDataDomainEvent masterDataDomain, IUserContext userContext)
        {
            this.masterDataDomain = masterDataDomain;
            _userContext = userContext;
        }

        private MasterDataDomainEvent masterDataDomain { get; set; }
        
        public  PageList<PageResult>  GetPageList(SearchModel a)
        {
            

            return masterDataDomain.GetPageList(a);
            //return testDomain.GetValue(a);
        }

        public string Add(MasterDataInfo masterDataInfo)
        {
            throw new BlocksBussnessException("201", StringLocal.Format("Error"),"");
            return masterDataDomain.Add(masterDataInfo.AutoMapTo<BussnessDomainModule.MasterData.MasterData>());
        }
    }
}