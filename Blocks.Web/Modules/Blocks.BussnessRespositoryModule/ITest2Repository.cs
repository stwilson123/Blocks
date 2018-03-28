using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.Combobox;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.BussnessEntityModule;
using Blocks.Framework.Data;
using Blocks.Framework.Data.Combobox;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessRespositoryModule
{
    public interface ITest2Repository : IRepository<TESTENTITY2>
    {

        PageList<ComboboxData> GetPageList(SearchModel search);
    }
}