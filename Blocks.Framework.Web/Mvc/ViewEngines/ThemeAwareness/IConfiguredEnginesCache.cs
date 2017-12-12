using System;
using System.Web.Mvc;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness
{
    public interface IConfiguredEnginesCache : ISingletonDependency {
        IViewEngine BindBareEngines(Func<IViewEngine> factory);
        IViewEngine BindShallowEngines(string themeName, Func<IViewEngine> factory);
        IViewEngine BindDeepEngines(string themeName, Func<IViewEngine> factory);
    }
}