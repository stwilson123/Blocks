﻿using System.Web.Mvc;
using System.Web.Routing;
using Abp.Dependency;
using Blocks.Framework.Web.Mvc.Controllers;
using Castle.MicroKernel;

namespace Blocks.Framework.Web.Mvc.Route
{
    public static class RouteExtension
    {
        public static string GetAreaName(this RouteBase route)
        {
            //var routeWithArea = route as IRouteWithArea;
            //if (routeWithArea != null)
            //{
            //    return routeWithArea.Area;
            //}

            var castRoute = route as System.Web.Routing.Route;
            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["area"] as string;
            }

            return null;
        }

        public static string GetAreaName(this RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue("area", out area))
            {
                return area as string;
            }

            return GetAreaName(routeData.Route);
        }

       
    }
}