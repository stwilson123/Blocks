

using Blocks.Web.Configuration;
using Blocks.Web.Security.AntiForgery;

namespace Blocks.Web.Configuration
{
    internal class BlocksWebModuleConfiguration : IBlocksWebModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }

        public IBlocksAntiForgeryConfiguration AntiForgery { get; }

        public IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }

        public BlocksWebModuleConfiguration(
            IBlocksAntiForgeryConfiguration abpAntiForgery,
            IWebEmbeddedResourcesConfiguration embeddedResources)
        {
            AntiForgery = abpAntiForgery;
            EmbeddedResources = embeddedResources;
        }
    }
}