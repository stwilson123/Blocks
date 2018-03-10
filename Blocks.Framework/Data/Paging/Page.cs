using Blocks.Framework.Json;
using Blocks.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.Services.DataTransfer;

namespace Blocks.Framework.Data.Pager
{
    [Serializable]
    public class Page : IDataTransferObject
    {
        private int _page;

        /// <summary>
        ///     每页数据条数
        /// </summary>
        [DataTransfer("rows")]
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set {
                //validte 
                _pageSize = value;

            }
        }

        private int _pageSize { get; set; } = 10;

        /// <summary>
        ///     页码
        /// </summary>
        [DataTransfer("page")]
        public int page
        {
            get
            {
                if (_page == 0) return 1;
                return _page;
            }
            set { _page = value; }
        }

        /// <summary>
        ///     排序字段
        /// </summary>
        [DataTransfer("sidx")]
        public string sortColumn { get; set; }

        /// <summary>
        ///     排序方式
        /// </summary>
        [DataTransfer("sord")]
        public string sortOrder { get; set; }


        /// <summary>
        ///     总页数
        /// </summary>
        public int total
        {
            get
            {
                if (pageSize == 0) return 0;
                return records % pageSize == 0 ? records / pageSize : records / pageSize + 1;
                //}
            }
        }
        /// <summary>
        ///     排序
        /// </summary>
        public string OrderBy
        {
            get
            {
                if (string.IsNullOrEmpty(sortColumn))
                    return "ID DESC";
                return sortColumn + " " + sortOrder;
            }
        }

        /// <summary>
        ///     数据开始索引
        /// </summary>
        public int StartIndex
        {
            get
            {
                if (pageSize == 0)
                    pageSize = 10;

                // int size = this.pageSize * this.page;
                int prePage = page - 1;
                if (prePage < 0) prePage = 0;
                int start = pageSize * prePage;
                return start + 1;
            }
        }

        /// <summary>
        ///     数据列结束索引
        /// </summary>
        public int EndIndex
        {
            get
            {
                if (pageSize == 0)
                    pageSize = 10;

                // int size = this.pageSize * this.page;
                int end = pageSize * page;
                if (end <= 0) end = pageSize;
                return end;
            }
        }

        /// <summary>
        ///     数据总条数
        /// </summary>
        public int records { get; set; }
        
        
        public Filters filters { get; set; }

    }
}
