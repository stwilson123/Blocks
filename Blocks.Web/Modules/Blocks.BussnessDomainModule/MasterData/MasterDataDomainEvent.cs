using Abp.Domain.Services;
using Blocks.BussnessApplicationModule.TestAppService.DTO;
using Blocks.BussnessEntityModule;
using Blocks.BussnessRespositoryModule;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Utility.SafeConvert;

namespace Blocks.BussnessDomainModule.MasterData
{
    public class MasterDataDomainEvent : IDomainService
    {
        public MasterDataDomainEvent(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        private ITestRepository testRepository { get; set; }
        
        public virtual string Add(MasterData data)
        {

            var newMasterData = new TESTENTITY();
            newMasterData.STRING = data.city;
            newMasterData.ISACTIVE = SafeConvert.ToInt64(data.isActive);
            newMasterData.COMMENT = data.comment;
            newMasterData.TESTENTITY2ID = "";
            return testRepository.Insert(newMasterData).Id;
        }
        
        
        public virtual  PageList<PageResult>  GetPageList(SearchModel search)
        {
            var a =  testRepository.GetPageList(search);
            return a;
        }
    }
}