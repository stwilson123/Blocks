using System.Web.Mvc;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness
{
    public interface IThemeAwareViewEngine : ISingletonDependency {//: IDependency {
        ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache, bool useDeepPaths);
        ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache, bool useDeepPaths);
    }
}