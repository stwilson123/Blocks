using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Dependency;
using Castle.MicroKernel.Lifestyle.Scoped;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace Blocks.Framework.Web
{
    public static class WorkContextExtensions {
        public static IIocManager GetContext(this IIocManager workContextAccessor, ControllerContext controllerContext) {
              //  return workContextAccessor.GetContext(controllerContext.RequestContext.HttpContext);
            return workContextAccessor;
        }
//
//        public static WorkContext GetLogicalContext(this IWorkContextAccessor workContextAccessor) {
//            var wca = workContextAccessor as ILogicalWorkContextAccessor;
//            return wca != null ? wca.GetLogicalContext() : null;
//        }

        public static IIocManager GetWorkContext(this RequestContext requestContext) {
            if (requestContext == null)
                return null;

            var routeData = requestContext.RouteData;
            if (routeData == null || routeData.DataTokens == null)
                return null;
            
            object workContextValue;
            if (!routeData.DataTokens.TryGetValue("IWorkContextAccessor", out workContextValue)) {
                workContextValue = FindWorkContextInParent(routeData);
            }

            if (!(workContextValue is IIocManager))
                return null;

            var workContextAccessor = (IIocManager)workContextValue;
            return workContextAccessor; //.GetContext(requestContext.HttpContext);
        }

        public static IIocManager GetWorkContext(this HttpControllerContext controllerContext) {
            if (controllerContext == null)
                return null;

            var routeData = controllerContext.RouteData;
            if (routeData == null || routeData.Values == null)
                return null;

            object workContextValue;
            if (!routeData.Values.TryGetValue("IWorkContextAccessor", out workContextValue)) {
                return null;
            }

            if (workContextValue == null || !(workContextValue is IIocManager))
                return null;

            var workContextAccessor = (IIocManager)workContextValue;
            return workContextAccessor;
        }

        private static object FindWorkContextInParent(RouteData routeData) {
            object parentViewContextValue;
            if (!routeData.DataTokens.TryGetValue("ParentActionViewContext", out parentViewContextValue)
                || !(parentViewContextValue is ViewContext)) {
                return null;
            }

            var parentRouteData = ((ViewContext)parentViewContextValue).RouteData;
            if (parentRouteData == null || parentRouteData.DataTokens == null)
                return null;

            object workContextValue;
            if (!parentRouteData.DataTokens.TryGetValue("IWorkContextAccessor", out workContextValue)) {
                workContextValue = FindWorkContextInParent(parentRouteData);
            }

            return workContextValue;
        }

        public static IIocManager GetWorkContext(this ControllerContext controllerContext) {
            if (controllerContext == null)
                return null;

            return GetWorkContext(controllerContext.HttpContext);
        }

//        public static IWorkContextScope CreateWorkContextScope(this ILifetimeScope lifetimeScope, HttpContextBase httpContext) {
//            return lifetimeScope.Resolve<IWorkContextAccessor>().CreateWorkContextScope(httpContext);
//        }
//
//        public static IWorkContextScope CreateWorkContextScope(this ILifetimeScope lifetimeScope) {
//            return lifetimeScope.Resolve<IWorkContextAccessor>().CreateWorkContextScope();
//        }
    }
}