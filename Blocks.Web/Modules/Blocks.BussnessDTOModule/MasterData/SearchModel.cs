using Blocks.Framework.Data.Pager;
using Blocks.Framework.Services;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessApplicationModule.TestAppService.DTO
{
    public class SearchModel : IDataTransferObject
    {
        public Page page { get; set; }
    }
}