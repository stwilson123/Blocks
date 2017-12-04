using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Web.Route
{
    public interface IHttpRouteProvider //: IDependency
    {
        void GetRoutes(ICollection<RouteDescriptor> routes,string areaName);
    }
}
