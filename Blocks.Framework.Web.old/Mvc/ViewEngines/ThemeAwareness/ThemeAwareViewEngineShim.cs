using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Abp.Dependency;
using Blocks.Framework.Logging;

namespace Blocks.Framework.Web.Mvc.ViewEngines.ThemeAwareness
{
    public class ThemeAwareViewEngineShim : IViewEngine//IViewEngine, IShim {
    {

        private readonly IIocManager _iocManager;

        public ThemeAwareViewEngineShim(IIocManager iocManager)
        {
            _iocManager = iocManager;
            //OrchardHostContainerRegistry.RegisterShim(this);
        }

        // public IOrchardHostContainer HostContainer { get; set; }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
            bool useCache)
        {
            return Forward(
                controllerContext,
                _iocManager,
                dve => dve.FindPartialView(controllerContext, partialViewName, useCache, false /*useDeepPaths*/),
                EmptyViewEngineResult);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName,
            bool useCache)
        {
            return Forward(
                controllerContext,
                _iocManager,
                dve => dve.FindView(controllerContext, viewName, masterName, useCache, false /*useDeepPaths*/),
                EmptyViewEngineResult);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            throw new NotImplementedException();
        }

        static TResult Forward<TResult>(ControllerContext controllerContext,IIocManager iIocManager,
            Func<IThemeAwareViewEngine, TResult> forwardAction, Func<TResult> defaultAction)
        {
//            var workContext = controllerContext.GetWorkContext();
//            if (workContext != null)
//            {
//                var displayViewEngine = workContext.Resolve<IThemeAwareViewEngine>();
//                if (displayViewEngine != null)
//                {
//                    return forwardAction(displayViewEngine);
//                }
//            }
            Stopwatch sw = Stopwatch.StartNew();
            if (iIocManager != null)
            {
                var displayViewEngine = iIocManager.Resolve<IThemeAwareViewEngine>();
                if (displayViewEngine != null)
                {
                    var fa= forwardAction(displayViewEngine);
                    sw.Stop();
                    LogHelper.logger.Debug($"ThemeAwareViewEngineShim cost time {sw.ElapsedMilliseconds}ms");
                    return fa;
                }
            }
           
            return defaultAction();
        }

        static ViewEngineResult EmptyViewEngineResult()
        {
            return new ViewEngineResult(Enumerable.Empty<string>());
        }
    }

}
 