using System;
using Blocks.BussnessApplicationModule.TestAppService.DTO;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public interface IMasterDataAppService : IAppService
    {
        PageList<PageResult>  GetPageList(SearchModel a);

        string Add(string id);
    }
}