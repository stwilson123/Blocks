using System.Collections.Generic;
using Blocks.Core;
using Blocks.Core.Navigation;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.FileSystems.WebSite;
using Blocks.Framework.Localization;

namespace Blocks.BussnessWebModule.Module_Start
{
    public class MobileNavigationProvider : JsonNavigationProviderBase
    {
        public override Localizer L { get; set; }
        public override ExtensionDescriptor Extension { get; set; }
        protected override IDictionary<Platform, string> filePaths { get; }

        public MobileNavigationProvider(IWebSiteFolder webSiteFolder) : base(webSiteFolder)
        {
            filePaths = new Dictionary<Platform, string>()
            {
                {Platform.Mobile, "Module_Start/Config/MobileNavigation.json"},
            };
        }
    }
}