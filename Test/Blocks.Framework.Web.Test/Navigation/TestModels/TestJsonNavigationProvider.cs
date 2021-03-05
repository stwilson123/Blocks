using System.Collections.Generic;
using Blocks.Core;
using Blocks.Core.Navigation;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.FileSystems.WebSite;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Web.Test.Navigation.TestModels
{
    public class TestJsonNavigationProvider : JsonNavigationProviderBase
    {
        public override Localizer L { get; set; }
        public override ExtensionDescriptor Extension { get; set; }
        protected override IDictionary<Platform, string> filePaths { get; }

        public TestJsonNavigationProvider(IWebSiteFolder webSiteFolder) : base(webSiteFolder)
        {
            filePaths = new Dictionary<Platform, string>()
            {
                {Platform.Mobile, "Navigation/TestModels/Config/MobileNavigation.json"},
               // {Platform.MainMenu, ""},

            };
        }
    }
}