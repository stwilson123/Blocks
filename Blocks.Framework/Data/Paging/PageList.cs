using Blocks.Framework.Data.Pager;
using Blocks.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blocks.Framework.Localization;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.Framework.Data.Paging
{
    [Serializable]
    public class PageList<T> : IDataTransferObject
    {
        [DataTransfer("pagerInfo")]
        public Page PagerInfo { get; set; }
        
        [DataTransfer("rows")]
        [LocalizedDescription("grid")]
        public List<T> Rows { get; set; }
    }
}
