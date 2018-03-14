using Blocks.Framework.Data.Pager;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessApplicationModule.TestAppService.DTO
{
    public class PageResult : IDataTransferObject
    { 
        public  string ID { get; set; }
        
        public  string CollectStationNo { get; set; }
    }
}