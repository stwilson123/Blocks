using Blocks.Framework.Data.Pager;
using Blocks.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Data.Paging
{
    [Serializable]
    public class PageList<T> : IDataTransferObject
    {
        public Page PagerInfo { get; set; }

        public List<T> Rows { get; set; }
    }
}
