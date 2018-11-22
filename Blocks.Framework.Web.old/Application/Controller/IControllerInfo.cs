using System;
using System.Collections.Generic;
using Blocks.Framework.Web.Application.Filters;

namespace Blocks.Framework.Web.Application.Controller
{
    public interface IControllerInfo<TActionInfo> where TActionInfo : IControllerActionInfo
    {
        /// <summary>
        /// Name of the service.
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// Service interface type.
        /// </summary>
        Type ServiceInterfaceType { get; }

        /// <summary>
        /// Api Controller type.
        /// </summary>
        Type ApiControllerType { get; }

        /// <summary>
        /// Interceptor type.
        /// </summary>
        Type InterceptorType { get; }

        /// <summary>
        /// Is API Explorer enabled.
        /// </summary>
        bool? IsApiExplorerEnabled { get;   }

        /// <summary>
        /// Dynamic Attribute for this controller.
        /// </summary>
        IFilter[] Filters { get; set; }

        /// <summary>
        /// All actions of the controller.
        /// </summary>
        IDictionary<string, TActionInfo> Actions { get;  }
    }
}