using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.License
{
    public class ProductInfo
    {
        public string ProductVersion { get; set; }
        public string CPUSerialNumber { get; set; }
        public string MainBoardSerialNumber { get; set; }
        public Int32 UserConnectNum { get; set; }

        public Int32 ExpirtedMin { get; set; }
    }
}
