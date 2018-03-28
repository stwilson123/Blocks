using Blocks.Framework.Data.Pager;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.BussnessDTOModule.MasterData
{
    public class SearchModel : IDataTransferObject
    {
        [DataTransfer("page")]
        public Page page { get; set; }
    }
}