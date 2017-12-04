using System;
using System.Web.Mvc;
using Abp.Dependency;

namespace Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness
{
    public interface IConfiguredEnginesCache : ISingletonDependency {
        IViewEngine BindBareEngines(Func<IViewEngine> factory);
        IViewEngine BindShallowEngines(string themeName, Func<IViewEngine> factory);
        IViewEngine BindDeepEngines(string themeName, Func<IViewEngine> factory);
    }
}