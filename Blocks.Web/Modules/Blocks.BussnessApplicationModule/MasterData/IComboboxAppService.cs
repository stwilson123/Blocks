using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.Data.Combobox;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessApplicationModule.MasterData
{
    public interface IComboboxAppService : IAppService
    {
        PageList<ComboboxData>  GetComboboxList(SearchModel a);
 
    }
}