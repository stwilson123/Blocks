using System.Web;
using System.Web.Mvc;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Web.FileSystems.VirtualPath;

namespace Blocks.Framework.Web.Mvc.Filters
{
    public class BlocksWebMvcActionFilter : IActionFilter, ITransientDependency
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
             
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // throw new System.NotImplementedException();
            
        }
    }


    public class BlocksWebMvcResultFilter : IResultFilter, ITransientDependency
    {
        public IVirtualPathProvider pathProvider = new DefaultVirtualPathProvider();
        public ExtensionManager extensionManager { set; get; }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //throw new System.NotImplementedException();
          
                ;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction && isMvcViewResult(filterContext) )
            {
                ViewResultBase viewResult = (ViewResultBase)filterContext.Result;
                if (viewResult != null)
                {
                    var viewName = filterContext.RouteData.GetRequiredString("action"); ;
                    var viewEngineResult = viewResult.ViewEngineCollection.FindView(filterContext.Controller.ControllerContext, viewName, null);
                    if(viewEngineResult != null && viewEngineResult.View != null)
                    {
                        var viewPath = VirtualPathUtility.ToAbsolute(((RazorView)viewEngineResult.View).ViewPath);
                        viewPath = viewPath.LastIndexOf(".cshtml") > 0 ? viewPath.Substring(0, viewPath.LastIndexOf(".cshtml")) : viewPath;
                        filterContext.Controller.ViewBag.subPageVirtualPath = viewPath;

                        var jsPath = viewPath + ".js";
                        if (pathProvider.FileExists(jsPath))
                            filterContext.Controller.ViewBag.subPageJsVirtualPath = jsPath;

                        var cssPath = viewPath + ".css";
                        if (pathProvider.FileExists(cssPath))
                            filterContext.Controller.ViewBag.subPageCssVirtualPath = cssPath;

                        var extension = extensionManager.GetExtension(filterContext.Controller.GetType().Assembly.GetName().Name);
                        filterContext.Controller.ViewBag.extensionName = extension?.Name;

                    }

                }
              
                
            }
        }

        private static bool isMvcViewResult(ResultExecutingContext filterContext)
        {
            return (filterContext.Result is ViewResult) || filterContext.Result is PartialViewResult;

        }
    }
}