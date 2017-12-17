using System;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Dependency;
using Blocks.Framework.Web.Mvc.Route;

namespace Blocks.Framework.Web.Mvc.Controllers
{
    /// <summary>
    /// This class is used to allow MVC to use dependency injection system while creating MVC controllers.
    /// </summary>
    public class BlocksWebMvcControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Reference to DI kernel.
        /// </summary>
        private readonly IIocResolver _iocManager;
        
     
        
        /// <summary>
        /// Creates a new instance of WindsorControllerFactory.
        /// </summary>
        /// <param name="iocManager">Reference to DI kernel</param>
        public BlocksWebMvcControllerFactory(IIocResolver iocManager)
        {
            _iocManager = iocManager;
    
        }

        /// <summary>
        /// Called by MVC system and releases/disposes given controller instance.
        /// </summary>
        /// <param name="controller">Controller instance</param>
        public override void ReleaseController(IController controller)
        {
            _iocManager.Release(controller);
        }

        /// <summary>
        /// Called by MVC system and creates controller instance for given controller type.
        /// </summary>
        /// <param name="requestContext">Request context</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            return _iocManager.Resolve<IController>(controllerType);
        }


        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            if (controllerName == null)
            {
                return base.GetControllerType(requestContext, controllerName);
            }
            string area = requestContext.RouteData.GetAreaName();
         
            var serviceKey = ControllerConventionalRegistrar.GetControllerSerivceName(area,controllerName) + "Controller";
            
            object instance = default(object);
            if (!string.IsNullOrEmpty(area) && _iocManager.IsRegistered(serviceKey))
            {
                instance = _iocManager.Resolve<IController>(serviceKey);
            }
            return instance != null ? instance.GetType() : base.GetControllerType(requestContext, controllerName);
        }
    }
}