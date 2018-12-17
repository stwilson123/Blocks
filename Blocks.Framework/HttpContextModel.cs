using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Blocks.Framework
{
    public class HttpContextModel
    {
        public Uri RequestUrl { get; set; }


        public CookieContainer CookieCollection { get; set; }


        public WebHeaderCollection webHeaderCollection { get; set; }
    }
}
