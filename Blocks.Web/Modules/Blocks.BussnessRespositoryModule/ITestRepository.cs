using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.BussnessEntityModule;
using Blocks.Framework.Data;
using Blocks.Framework.Data.Paging;
using System.Collections.Generic;

namespace Blocks.BussnessRespositoryModule
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);

        string GetValueOverride(string value);

        PageList<PageResult> GetPageList(SearchModel search);

        List<PageResult> GetList();
    }
}