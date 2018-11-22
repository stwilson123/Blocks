using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Blocks.Framework.Web.Api.Controllers.Dynamic.Builders;
using Blocks.Framework.Web.Api.Route;

namespace Blocks.Framework.Web.Api.Controllers.Dynamic.Selectors
{
     /// <summary>
    /// This class is used to extend default controller selector to add dynamic api controller creation feature of Abp.
    /// It checks if requested controller is a dynamic api controller, if it is,
    /// returns <see cref="HttpControllerDescriptor"/> to ASP.NET system.
    /// </summary>
    public class BlocksHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;
        private readonly IIocManager _iIocManager;
        /// <summary>
        /// Creates a new <see cref="AbpHttpControllerSelector"/> object.
        /// </summary>
        /// <param name="configuration">Http configuration</param>
        /// <param name="dynamicApiControllerManager"></param>
        public BlocksHttpControllerSelector(HttpConfiguration configuration, DynamicApiControllerManager dynamicApiControllerManager, IIocManager iIocManager)
            : base(configuration)
        {
            _configuration = configuration;
            _dynamicApiControllerManager = dynamicApiControllerManager;
            _iIocManager = iIocManager; 
        }

        /// <summary>
        /// This method is called by Web API system to select the controller for this request.
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>The controller to be used</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //Get request and route data
            if (request == null)
            {
                return base.SelectController(null);
            }

            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                return base.SelectController(request);
            }

            //Get serviceNameWithAction from route
            string serviceNameWithAction;
            if (!routeData.Values.TryGetValue("serviceNameWithAction", out serviceNameWithAction))
            {
                return GetHttpController(request);                
            }

            //Normalize serviceNameWithAction
            if (serviceNameWithAction.EndsWith("/"))
            {
                serviceNameWithAction = serviceNameWithAction.Substring(0, serviceNameWithAction.Length - 1);
                routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            }

            //Get the dynamic controller
            var hasActionName = false;
            var controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceNameWithAction);
            if (controllerInfo == null)
            {
                if (!DynamicApiServiceNameHelper.IsValidServiceNameWithAction(serviceNameWithAction))
                {
                    return base.SelectController(request);
                }
                
                var serviceName = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(serviceNameWithAction);
                controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceName);
                if (controllerInfo == null)
                {
                    return base.SelectController(request);                    
                }

                hasActionName = true;
            }
            
            //Create the controller descriptor
            var controllerDescriptor = new DynamicHttpControllerDescriptor(_configuration, controllerInfo);
            controllerDescriptor.Properties["__AbpDynamicApiControllerInfo"] = controllerInfo;
            controllerDescriptor.Properties["__AbpDynamicApiHasActionName"] = hasActionName;
            return controllerDescriptor;
        }


        private HttpControllerDescriptor GetHttpController(HttpRequestMessage request)
        {
            string area = request.GetRouteData().GetAreaName();
          
            object instance = default(object);
            var controllerName = base.GetControllerName(request);
            
            var serviceKey = ApiControllerConventionalRegistrar.GetControllerSerivceName(area,controllerName) + "Controller";

//            string serviceKey = $"{area}.Api.Controllers.{controllerName}Controller";
            if (!string.IsNullOrEmpty(area) && _iIocManager.IsRegistered(serviceKey))
            {
                instance = _iIocManager.Resolve<IHttpController>(serviceKey);
            }

            if (instance != null)
                return new HttpControllerDescriptor(_configuration, controllerName, instance.GetType());

            return base.SelectController(request);
        }
    }
}