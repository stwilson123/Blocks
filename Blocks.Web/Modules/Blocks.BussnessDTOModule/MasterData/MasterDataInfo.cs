using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessApplicationModule.TestAppService.DTO
{
    public class MasterDataInfo: IDataTransferObject
    {
        public string tenancyName { get; set; }
        
        public string city { get; set; }
        
        public bool isActive { get; set; }

        public string comment { get; set; }
    }
}