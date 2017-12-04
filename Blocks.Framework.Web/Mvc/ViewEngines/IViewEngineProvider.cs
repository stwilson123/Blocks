using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Dependency;

namespace Blocks.Framework.Web.Mvc.ViewEngines
{
    public class CreateThemeViewEngineParams
    {
        public string VirtualPath { get; set; }
    }

    public class CreateModulesViewEngineParams
    {
        public IEnumerable<string> VirtualPaths { get; set; }
        public IEnumerable<string> ExtensionLocations { get; set; }
    }

    public interface IViewEngineProvider : ISingletonDependency
    {
        IViewEngine CreateThemeViewEngine(CreateThemeViewEngineParams parameters);
        IViewEngine CreateModulesViewEngine(CreateModulesViewEngineParams parameters);

        /// <summary>
        ///     Produce a view engine configured to resolve only fully qualified {viewName} parameters
        /// </summary>
        IViewEngine CreateBareViewEngine();
    }
}