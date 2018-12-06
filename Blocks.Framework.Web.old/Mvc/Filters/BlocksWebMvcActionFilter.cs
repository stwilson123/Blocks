using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Configuration.Startup;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Web.Mvc.Route;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
//        public IVirtualPathProvider pathProvider = new DefaultVirtualPathProvider();
        public IVirtualPathProvider pathProvider { set; get; }

        public ExtensionManager extensionManager { set; get; }

        public INavigationManager navigationManager { set; get; }
        public IAuthorizationService authorizationService { set; get; }
        public IUserContext userContext { set; get; }
        public IAbpStartupConfiguration Configuration { get; internal set; }

        public BlocksWebMvcResultFilter(IAbpStartupConfiguration configuration)
        {
            Configuration = configuration;
        }

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
                        filterContext.Controller.ViewBag.permissions = getPermissions(filterContext);
                        filterContext.Controller.ViewBag.localization = getlocalization(filterContext);

                        
                    }

                }
              
                
            }
        }

        private string[] getPermissions(ResultExecutingContext filterContext)
        {
            var menus = navigationManager.Menus;
            foreach (var menu in menus)
            {
                var userNavItem = menu.Value.Items.FirstOrDefault(i => i.RouteValues.RouteEquals(filterContext.RouteData.Values));
                var permissions = new List<Permission>();


                for (int i = 0; i < userNavItem?.HasPermissions?.Length; i++)
                {
                    var Permission = userNavItem.HasPermissions[i];
                    if (authorizationService.TryCheckAccess(Permission, userContext.GetCurrentUser()).Result)
                        permissions.Add(Permission);
                }
                return permissions.Select(t => t.Name).ToArray();
            }
            return null;
        }

        private object getlocalization(ResultExecutingContext filterContext)
        {
 
            var dictonaryFilter = Configuration.Localization.Sources.Where(s => extensionManager.AvailableFeatures().Any(f => f.Name == s.Name));

     
             
            return dictonaryFilter.ToDictionary(source => source.Name, source => source.GetAllStrings().ToDictionary(str => str.Name, str => str.Value));
        }

        private static bool isMvcViewResult(ResultExecutingContext filterContext)
        {
            return (filterContext.Result is ViewResult) || filterContext.Result is PartialViewResult;

        }
    }
}