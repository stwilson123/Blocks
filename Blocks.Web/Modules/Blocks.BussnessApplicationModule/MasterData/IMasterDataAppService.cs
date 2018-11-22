using System.Web.Http;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Localization;
using Blocks.Framework.Web.Web.HttpMethod;

namespace Blocks.BussnessApplicationModule.MasterData
{
    public interface IMasterDataAppService : IAppService
    {
        Localizer L { get; set; }

        [HttpMethod(HttpVerb.Delete)]
        PageList<PageResult>  GetPageList(SearchModel a);

        void Add(MasterDataInfo masterDataInfo);

        void TestException();
    }
}