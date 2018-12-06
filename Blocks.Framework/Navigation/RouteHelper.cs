using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Navigation
{
    public static class RouteHelper
    {
        public static string GetUrl(IDictionary<string, object> routeValue)
        {
            var controllerServiceName = routeValue["area"]?.ToString() + "/" + routeValue["controller"]?.ToString()
                                       + "/" + routeValue["action"]?.ToString();
            return controllerServiceName;
        }
    }
}
