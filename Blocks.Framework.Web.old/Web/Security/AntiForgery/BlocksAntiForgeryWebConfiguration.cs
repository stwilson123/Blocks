using Blocks.Framework.Web.Web.HttpMethod;
using System.Collections.Generic;

namespace Blocks.Framework.Web.Web.Security.AntiForgery
{
    public class BlocksAntiForgeryWebConfiguration : IBlocksAntiForgeryWebConfiguration
    {
        public bool IsEnabled { get; set; }

        public HashSet<HttpVerb> IgnoredHttpVerbs { get; }

        public BlocksAntiForgeryWebConfiguration()
        {
            IsEnabled = true;
            IgnoredHttpVerbs = new HashSet<HttpVerb> { HttpVerb.Get, HttpVerb.Head, HttpVerb.Options, HttpVerb.Trace };
        }
    }
}