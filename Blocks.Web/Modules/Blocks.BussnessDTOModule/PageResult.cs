using System;
using Blocks.Framework.Localization;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessDTOModule
{
    public class PageResult : IDataTransferObject
    {
        public string Id { get; set; }
        
        [LocalizedDescription("tenancyName")]
        public string tenancyName { get; set; }
        
        [LocalizedDescription("city")]
        public string city { get; set; }
        
        public long isActive { get; set; }

        public string comment { get; set; }
        
        public string comboboxId { get; set; }

        public string comboboxText { get; set; }
        
        public DateTime registerTime { get; set; }

        public ILocalizableString localizableString { get; set; }

    }
}