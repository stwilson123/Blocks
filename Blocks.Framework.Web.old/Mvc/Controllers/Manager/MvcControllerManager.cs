using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Blocks.Framework.ApplicationServices.Controller;
using Blocks.Framework.ApplicationServices.Manager;
using Blocks.Framework.Collections.Extensions;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Utility.Extensions;
namespace Blocks.Framework.Web.Mvc.Controllers.Manager
{
    public class MvcControllerManager : ControllerManger<DefaultControllerInfo<MvcControllerActionInfo>,MvcControllerActionInfo>, ISingletonDependency
    {
        public MvcControllerManager(): base()
        {
        }

     
        /// <summary>
        /// Searches and returns a dynamic api controller for given name.
        /// </summary>
        /// <param name="controllerName">Name of the controller</param>
        /// <returns>Controller info</returns>
        public override  DefaultControllerInfo<MvcControllerActionInfo> FindOrNull(string controllerName)
        {
            return _defaultControllers.GetOrDefault(controllerName) as DefaultControllerInfo<MvcControllerActionInfo>;
        }

        public override IReadOnlyList<DefaultControllerInfo<MvcControllerActionInfo>> GetAll()
        {
            return _defaultControllers.Values.OfType<DefaultControllerInfo<MvcControllerActionInfo>>().ToImmutableList();
        }
    }
}