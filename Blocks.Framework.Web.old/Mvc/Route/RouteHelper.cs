using System.Collections.Generic;

namespace Blocks.Framework.Web.Mvc.Route
{
    public static class RouteHelper
    {
        public static string GetUrl(IDictionary<string,object> routeValue)
        {
            var controllerServiceName = routeValue["area"]?.ToString() + "/" +routeValue["controller"]?.ToString() 
                                       + "/" + routeValue["action"]?.ToString();
            return controllerServiceName;
        }


        public static bool RouteEquals(this IDictionary<string, object> routeValue,IDictionary<string,object> referRouteValue)
        {

            if (routeValue["area"]?.ToString() != referRouteValue["area"]?.ToString())
                return false;
            if (routeValue["controller"]?.ToString() != referRouteValue["controller"]?.ToString())
                return false;
            if (routeValue["action"]?.ToString() != referRouteValue["action"]?.ToString())
                return false;
            return true;
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