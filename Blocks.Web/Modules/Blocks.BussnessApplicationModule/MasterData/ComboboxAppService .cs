using System;
using Blocks.BussnessDomainModule;
using Blocks.BussnessDomainModule.MasterData;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.Framework.ApplicationServices;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Data.Combobox;
using Blocks.Framework.Data.Paging;

namespace Blocks.BussnessApplicationModule.MasterData
{
    public class ComboboxAppService : AppService,IComboboxAppService
    {
        public ComboboxAppService(ComboboxDomainEvent comboboxDomain)
        {
            this.ComboboxDomain = comboboxDomain;
        }

        private ComboboxDomainEvent ComboboxDomain { get; set; }
        
        public  PageList<ComboboxData>  GetComboboxList(SearchModel a)
        {
            return ComboboxDomain.GetComboboxList(a);
        }

    
    }
}