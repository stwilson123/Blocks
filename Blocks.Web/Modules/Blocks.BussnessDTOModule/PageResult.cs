using System;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessDTOModule
{
    public class PageResult : IDataTransferObject
    {
        public string Id { get; set; }
        
        public string tenancyName { get; set; }
        
        public string city { get; set; }
        
        public long isActive { get; set; }

        public string comment { get; set; }
        
        public string comboboxText { get; set; }
        
        public DateTime registerTime { get; set; }

    }
}