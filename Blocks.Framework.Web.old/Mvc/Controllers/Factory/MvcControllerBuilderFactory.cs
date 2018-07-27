using System;
using System.Collections.Generic;
using Abp.Dependency;
using Blocks.Framework.ApplicationServices.Controller.Builder;
using Blocks.Framework.ApplicationServices.Controller.Factory;
using Blocks.Framework.Web.Mvc.Controllers.Builder;
using Blocks.Framework.Web.Mvc.Controllers.Manager;

namespace Blocks.Framework.Web.Mvc.Controllers.Factory
{
    public class MvcControllerBuilderFactory : DefaultControllerBuilderFactory
    {
        public MvcControllerBuilderFactory(IIocManager iocManager) : base(iocManager)
        {
        }

        public override IDefaultControllerBuilder<T> For<T>(string serviceName)
        {
            return new MvcControllerBuilder<T,MvcControllerActionBuilder<T>>(serviceName, _iocManager,_iocManager.Resolve<MvcControllerManager>());
        }

        public override IBatchDefaultControllerBuilder<T> ForAll<T>(string servicePrefix,IEnumerable<Type> serviceTypes)
        {
            return new BatchMvcControllerBuilder<T>(this, servicePrefix,serviceTypes);
        }
    }
}