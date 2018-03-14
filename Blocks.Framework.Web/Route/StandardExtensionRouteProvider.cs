using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Blocks.Framework.Web.Route
{
    public class StandardExtensionRouteProvider : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes, string areaName)
        {
            var displayPathsPerArea = new List<string>() { areaName };

            //foreach (var item in displayPathsPerArea)
            //{
               // var areaNameTmp = item;
                var extensionDescriptors = displayPathsPerArea.Distinct();

                var displayPaths = new System.Collections.Generic.HashSet<string>();

                foreach (var extensionDescriptor in extensionDescriptors)
                {
                    var displayPath = extensionDescriptor;

                    if (!displayPaths.Contains(displayPath))
                    {
                        displayPaths.Add(displayPath);

                        SessionStateBehavior defaultSessionState = SessionStateBehavior.Default;
                       // Enum.TryParse(extensionDescriptor.SessionState, true /*ignoreCase*/, out defaultSessionState);


                        //routes.Add(new RouteDescriptor
                        //{
                        //    Priority = -10,
                        //    SessionState = defaultSessionState,
                        //    Route = new System.Web.Routing.Route(
                        //        "Admin/" + displayPath + "/{action}/{id}",
                        //        new RouteValueDictionary {
                        //            {"area", areaName},
                        //            {"controller", "admin"},
                        //            {"action", "index"},
                        //            {"id", ""}
                        //        },
                        //        new RouteValueDictionary(),
                        //        new RouteValueDictionary {
                        //            {"area", areaName}
                        //        },
                        //        new MvcRouteHandler())
                        //});

                        routes.Add(new RouteDescriptor
                        {
                            Name = areaName + "Route",
                            Priority = -10,
                            SessionState = defaultSessionState,
                            Route = new System.Web.Routing.Route(
                                displayPath + "/{controller}/{action}/{id}",
                                new RouteValueDictionary {
                                    {"area", areaName},
                                    {"controller", "home"},
                                    {"action", "index"},
                                    {"id", ""}
                                },
                                new RouteValueDictionary(),
                                new RouteValueDictionary {
                                    {"area", areaName}
                                },
                                new MvcRouteHandler())
                        });
                    }
                }
            //}
        }
    }
}
