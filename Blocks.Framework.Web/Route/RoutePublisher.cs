using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;
using Abp.Dependency;

namespace Blocks.Framework.Web.Route
{
    public class RoutePublisher : IRoutePublisher
    {
        private readonly RouteCollection _routeCollection;
        private readonly IIocManager _iocManager;
        public RoutePublisher(RouteCollection routeCollection, IIocManager iocManager)
        {
            _routeCollection = routeCollection;
            _iocManager = iocManager;
        }

        public void Publish(IEnumerable<RouteDescriptor> routes, Func<IDictionary<string, object>, Task> pipeline = null)
        {
            var routesArray = routes
           .OrderByDescending(r => r.Priority)
           .ToArray();

            // this is not called often, but is intended to surface problems before
            // the actual collection is modified
            var preloading = new RouteCollection();
            foreach (var routeDescriptor in routesArray)
            {
                // extract the WebApi route implementation
                var httpRouteDescriptor = routeDescriptor as HttpRouteDescriptor;
                if (httpRouteDescriptor != null)
                {
                    var httpRouteCollection = new RouteCollection();
                    httpRouteCollection.MapHttpRoute(httpRouteDescriptor.Name, httpRouteDescriptor.RouteTemplate, httpRouteDescriptor.Defaults, httpRouteDescriptor.Constraints);
                    routeDescriptor.Route = httpRouteCollection.First();
                }

                preloading.Add(routeDescriptor.Name, routeDescriptor.Route);
            }

            using (_routeCollection.GetWriteLock())
            {
                // HACK: For inserting names in internal dictionary when inserting route to RouteCollection.
                var routeCollectionType = typeof(RouteCollection);
                var namedMap = (Dictionary<string, RouteBase>)routeCollectionType.GetField("_namedMap", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_routeCollection);

                // new routes are added
                foreach (var routeDescriptor in routesArray)
                {
                    // Loading session state information. 
                    var defaultSessionState = SessionStateBehavior.Default;
                    object extensionId = default(object);
                    if (routeDescriptor.Route is System.Web.Routing.Route)
                    {

                        var route = routeDescriptor.Route as System.Web.Routing.Route;
                        if (route.DataTokens != null && route.DataTokens.TryGetValue("area", out extensionId) ||
                           route.Defaults != null && route.Defaults.TryGetValue("area", out extensionId))
                        {
                            //TODO Extension Handler
                            //extensionDescriptor = _extensionManager.GetExtension(extensionId.ToString());
                        }
                    }
                    var shellRoute = new ShellRoute(routeDescriptor.Route, _iocManager, pipeline) {
                        IsHttpRoute = routeDescriptor is HttpRouteDescriptor,
                       // SessionState = sessionStateBehavior
                    };
                    var area = extensionId != null ? extensionId.ToString() : string.Empty;
                    if(!namedMap.Any(t => t.Key == area))

                    {
                        if (routeDescriptor.Route != null)
                            _routeCollection.Add(shellRoute);
                        namedMap[routeDescriptor.Name] = shellRoute;
                    }
                }
            }
        }
    }
}
