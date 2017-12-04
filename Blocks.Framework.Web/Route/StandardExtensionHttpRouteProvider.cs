using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Blocks.Framework.Web.Route
{
    public class StandardExtensionHttpRouteProvider : IHttpRouteProvider
    {

        public void GetRoutes(ICollection<RouteDescriptor> routes,string areaName)
        {
            var displayPathsPerArea = new List<string>() { areaName };

            foreach (var item in displayPathsPerArea)
            {
                var areaNameTmp = item;
                var displayPath = item;//.Distinct().Single();

                var routeDescriptor = new HttpRouteDescriptor
                {
                    Name = areaName + "HttpRoute",
                    Priority = -10,
                    RouteTemplate = "api/" + displayPath + "/{controller}/{id}",
                    Defaults = new { area = areaName, controller = "api", id = RouteParameter.Optional }
                };

                routes.Add(routeDescriptor);
            }
        }
    }
}
