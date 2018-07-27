using System.Collections.Generic;

namespace Blocks.Framework.Web.Mvc.Route
{
    public class RouteHelper
    {
        public static string GetUrl(IDictionary<string,object> routeValue)
        {
            var controllerServiceName =routeValue["area"]?.ToString() + "/" +routeValue["controller"]?.ToString() 
                                       + "/" + routeValue["action"]?.ToString();
            return controllerServiceName;
        }
        public static string GetControllerPath(IDictionary<string,object> routeValue)
        {
            var controllerServiceName =routeValue["area"]?.ToString() + "/" +routeValue["controller"]?.ToString();
            return controllerServiceName;
        }
    }

    public class ControllerRoute
    {
        public string area { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
    }
}