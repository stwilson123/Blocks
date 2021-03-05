

using Blocks.Web.Configuration;
using Blocks.Web.Security.AntiForgery;

namespace Blocks.Web.Configuration
{
    /// <summary>
    /// Used to configure ABP Web Common module.
    /// </summary>
    public interface IBlocksWebModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (ABP hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

      

        /// <summary>
        /// Used to configure Anti Forgery security settings.
        /// </summary>
        IBlocksAntiForgeryConfiguration AntiForgery { get; }

        /// <summary>
        /// Used to configure embedded resource system for web applications.
        /// </summary>
        IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }
 
    }
}