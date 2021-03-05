using Blocks.Web.Security.AntiForgery;

namespace Blocks.Web.Security.AntiForgery
{
    public class BlocksAntiForgeryConfiguration : IBlocksAntiForgeryConfiguration
    {
        public string TokenCookieName { get; set; }

        public string TokenHeaderName { get; set; }

        public BlocksAntiForgeryConfiguration()
        {
            TokenCookieName = "XSRF-TOKEN";
            TokenHeaderName = "X-XSRF-TOKEN";
        }
    }
}