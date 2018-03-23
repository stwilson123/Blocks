using Blocks.BussnessApplicationModule.TestAppService.DTO;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessApplicationModule.MasterData
{
    public interface IMasterDataAppService : IAppService
    {
        PageList<PageResult>  GetPageList(SearchModel a);

        string Add(MasterDataInfo masterDataInfo);
    }
}