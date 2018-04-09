using System;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessDTOModule.MasterData
{
    public class MasterDataInfo: IDataTransferObject
    {
        public string tenancyName { get; set; }
        
        public string city { get; set; }

        public string combobox { get; set; }
        
        public bool isActive { get; set; }

        public string comment { get; set; }
        
        public DateTime registerTime { get; set; }
    }
}