using Blocks.BussnessApplicationModule.TestAppService.DTO;
using Blocks.BussnessEntityModule;
using Blocks.Framework.Data;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessRespositoryModule
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);

        string GetValueOverride(string value);

        PageList<PageResult> GetPageList(SearchModel search);
    }
}