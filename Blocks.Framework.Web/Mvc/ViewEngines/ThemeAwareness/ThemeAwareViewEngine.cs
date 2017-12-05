using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Abp.Web.Mvc.Extensions;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.FileSystems.Extensions;
using Blocks.Framework.Web.Mvc.Route;
using Castle.Core.Logging;

namespace Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness
{
   public class ThemeAwareViewEngine : IThemeAwareViewEngine {
     //   private readonly WorkContext _workContext;
        private readonly IEnumerable<IViewEngineProvider> _viewEngineProviders;
        private readonly IConfiguredEnginesCache _configuredEnginesCache;
        private readonly IExtensionManager _extensionManager;
//        private readonly ShellDescriptor _shellDescriptor;
        private readonly IViewEngine _nullEngines = new ViewEngineCollectionWrapper(Enumerable.Empty<IViewEngine>().Concat(new[]{ new RazorViewEngine()}));

        public ThemeAwareViewEngine(
           // WorkContext workContext,
            IEnumerable<IViewEngineProvider> viewEngineProviders,
            IConfiguredEnginesCache configuredEnginesCache,
            IExtensionManager extensionManager
           // ShellDescriptor shellDescriptor) {
        ) {
           // _workContext = workContext;
            _viewEngineProviders = viewEngineProviders;
            _configuredEnginesCache = configuredEnginesCache;
            _extensionManager = extensionManager;
//            _shellDescriptor = shellDescriptor;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache, bool useDeepPaths) {
            var engines = _nullEngines;

            if (partialViewName.StartsWith("/") || partialViewName.StartsWith("~")) {
                engines = BareEngines();
            }
//            else if (_workContext.CurrentTheme != null) {
//                engines = useDeepPaths ? DeepEngines(_workContext.CurrentTheme) : ShallowEngines(_workContext.CurrentTheme);
//            }

            return engines.FindPartialView(controllerContext, partialViewName, useCache);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache, bool useDeepPaths) {
            var engines = _nullEngines;

            if (viewName.StartsWith("/") || viewName.StartsWith("~")) {
                engines = BareEngines();
            }
//            else if (_workContext.CurrentTheme != null) {
//                engines = useDeepPaths ? DeepEngines(_workContext.CurrentTheme) : ShallowEngines(_workContext.CurrentTheme);
//            }
            var areaName = controllerContext.RouteData.GetAreaName();
            if (!string.IsNullOrEmpty(areaName))
            {
                   engines = useDeepPaths ? DeepEngines(areaName) : ShallowEngines(areaName);
            }
            return engines.FindView(controllerContext, viewName, masterName, useCache);
        }


        private IViewEngine BareEngines() {
            return _configuredEnginesCache.BindBareEngines(() => new ViewEngineCollectionWrapper(_viewEngineProviders.Select(vep => vep.CreateBareViewEngine())));
        }

        private IViewEngine ShallowEngines(string areaName) {
            //return _configuredEnginesCache.BindShallowEngines(theme.ThemeName, () => new ViewEngineCollectionWrapper(_viewEngineProviders.Select(vep => vep.CreateBareViewEngine())));
            return DeepEngines(areaName);
        }

        private IViewEngine DeepEngines(string areaName) {
            // return _configuredEnginesCache.BindDeepEngines("Theme", () => {

            return _configuredEnginesCache.BindDeepEngines(areaName, () => {
                // The order for searching for views is:
                // 1. Current "theme"
                // 2. Base themes of the current theme (in "base" order)
                // 3. Active features from modules in dependency order

                var engines = Enumerable.Empty<IViewEngine>();
                // 1. current theme
             //   engines = engines.Concat(CreateThemeViewEngines(theme));

                // 2. Base themes of the current theme (in "base" order)
              //  engines = GetBaseThemes(theme).Aggregate(engines, (current, baseTheme) => current.Concat(CreateThemeViewEngines(baseTheme)));

                // 3. Active features from modules in dependency order
//                var enabledModules = _extensionManager.EnabledFeatures(_shellDescriptor)
//                    .Reverse()  // reverse from (C <= B <= A) to (A => B => C)
//                    .Where(fd => DefaultExtensionTypes.IsModule(fd.Extension.ExtensionType));

//                var moduleVirtualPaths = enabledModules
//                    .Select(fd => fd.Extension.VirtualPath)
//                    .Distinct(StringComparer.OrdinalIgnoreCase) // is Distinct guaranty to keep order?
//                    .ToList();
//
//                var moduleLocations = enabledModules
//                    .Select(fd => fd.Extension.Location)
//                    .Distinct(StringComparer.OrdinalIgnoreCase) 
//                    .ToList();

                // 3. Active features from modules in dependency order
                var enabledModules = _extensionManager.EnabledFeatures(areaName)
                    .Reverse()  // reverse from (C <= B <= A) to (A => B => C)
                    .Where(fd => DefaultExtensionTypes.IsModule(fd.Extension.ExtensionType));

                var moduleVirtualPaths = enabledModules
                    .Select(fd => fd.Extension.VirtualPath)
                    .Distinct(StringComparer.OrdinalIgnoreCase) // is Distinct guaranty to keep order?
                    .ToList();

                var moduleLocations = enabledModules
                    .Select(fd => fd.Extension.Location)
                    .Distinct(StringComparer.OrdinalIgnoreCase) 
                    .ToList();
                
                var moduleParams = new CreateModulesViewEngineParams { VirtualPaths = moduleVirtualPaths, ExtensionLocations = moduleLocations };
                engines = engines.Concat(_viewEngineProviders.Select(vep => vep.CreateModulesViewEngine(moduleParams)));

                return new ViewEngineCollectionWrapper(engines);
            });
        }

//        private IEnumerable<IViewEngine> CreateThemeViewEngines(ExtensionDescriptor theme) {
//            var themeLocation = theme.Location + "/" + theme.Id;
//            var themeParams = new CreateThemeViewEngineParams {VirtualPath = themeLocation};
//            return _viewEngineProviders.Select(vep => vep.CreateThemeViewEngine(themeParams));
//        }
//
//        private IEnumerable<ExtensionDescriptor> GetBaseThemes(ExtensionDescriptor themeExtension) {
//            if (themeExtension.Id.Equals("TheAdmin", StringComparison.OrdinalIgnoreCase)) {
//                // Special case: conceptually, the base themes of "TheAdmin" is the list of all
//                // enabled themes. This is so that any enabled theme can have controller/action/views
//                // in the Admin of the site.
//                return _extensionManager
//                    .EnabledFeatures(_shellDescriptor)
//                    .Reverse()  // reverse from (C <= B <= A) to (A => B => C)
//                    .Select(fd => fd.Extension)
//                    .Where(fd => DefaultExtensionTypes.IsTheme(fd.ExtensionType));
//            }
//            else {
//                var availableFeatures = _extensionManager.AvailableFeatures();
//                var list = new List<ExtensionDescriptor>();
//                while(true) {
//                    if (themeExtension == null)
//                        break;
//
//                    if (String.IsNullOrEmpty(themeExtension.BaseTheme))
//                        break;
//
//                    var baseFeature = availableFeatures.FirstOrDefault(fd => fd.Id == themeExtension.BaseTheme);
//                    if (baseFeature == null) {
//                        Logger.Error("Base theme '{0}' of theme '{1}' not found in list of features", themeExtension.BaseTheme, themeExtension.Id);
//                        break;
//                    }
//
//                    // Protect against potential infinite loop
//                    if (list.Contains((ExtensionDescriptor) baseFeature.Extension)) {
//                        Logger.Error("Base theme '{0}' of theme '{1}' ignored, as it seems there is recursion in base themes", themeExtension.BaseTheme, themeExtension.Id);
//                        break;
//                    }
//
//                    list.Add(baseFeature.Extension);
//
//                    themeExtension = baseFeature.Extension;
//                }
//                return list;
//            }
//        }

        public void ReleaseView(ControllerContext controllerContext, IView view) {
            throw new NotImplementedException();
        }
    }
}