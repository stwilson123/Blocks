using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abp.Extensions;
using Blocks.Core.Navigation.Models;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.FileSystems.WebSite;
using Blocks.Framework.Localization;
using Blocks.Framework.Navigation;
using Blocks.Framework.Navigation.Provider;
using Blocks.Framework.Tools.Json;
using Blocks.Framework.Utility.Extensions;

namespace Blocks.Core.Navigation
{
    public abstract class JsonNavigationProviderBase : INavigationProvider
    {
        public abstract Localizer L { get; set; }
        public abstract ExtensionDescriptor Extension { get; set; }
        protected IWebSiteFolder _webSiteFolder { get; set; }

        public JsonNavigationProviderBase(IWebSiteFolder webSiteFolder)
        {
            _webSiteFolder = webSiteFolder;
        }
        public void SetNavigation(INavigationProviderContext context)
        {
            var platformConfigs = GetConfig();
            foreach (var platformConfig in platformConfigs)
            {
                var platform = platformConfig.Key.ToString();
                var menus = context.Manager.Menus[platform];
                foreach (var navigationItemConfig in platformConfig.Value?.Items)
                {
                    menus.AddBuilder(m =>
                    {
                        var builder =
                     m.Name(navigationItemConfig.Name)
                     .Visible(navigationItemConfig.Visible ?? true)
                         .DisplayName(L(navigationItemConfig.DisplayName ?? navigationItemConfig.DisplayName))
                         .Action(navigationItemConfig.Action, navigationItemConfig.ControllerName,
                             navigationItemConfig.AreaName ?? Extension.Name)
                         .SetNavigationType(navigationItemConfig.NavigationType ?? 1);
                        if (platform == "MainMenu")
                        {
                            builder.HasPermissions(navigationItemConfig.Permission);
                        }
                        else
                        {
                            builder.HasPermissions(platform, navigationItemConfig.Permission);
                        }



                    });

                }
            }
        }

        protected abstract IDictionary<Platform, string> filePaths { get; }

        protected virtual IDictionary<Platform, JsonNavigationConfig> GetConfig()
        {

            if (filePaths.IsNullOrEmpty())
                throw new BlocksCoreException(StringLocal.Format("filePath is null or empty"));

            return filePaths.ToDictionary(f => f.Key, f =>
             {

                 var a = _webSiteFolder.ListFiles(Extension.VirtualPath + "/Module_Start/Config", true).ToList();
                 var jsonString = _webSiteFolder.ReadFile(Extension.VirtualPath.TrimStart('~') + "/" + f.Value);
                 return JsonHelper.DeserializeObject<JsonNavigationConfig>(jsonString);
             });

        }
    }
}