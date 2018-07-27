using System;
using System.Collections;
using System.Collections.Generic;
using Abp.Dependency;
using Blocks.Framework.ApplicationServices.Controller.Builder;
using Blocks.Framework.ApplicationServices.Controller.Factory;


namespace Blocks.Framework.Web.Mvc.Controllers.Builder
{
    public class BatchMvcControllerBuilder<T> : BatchDefaultControllerBuilder<T>
    {
        protected override string[] ControllerPostfixes
        {
            get { return new string[] {"Controller"}; }
        }

        public BatchMvcControllerBuilder(IDefaultControllerBuilderFactory controllerBuilderFactory, string servicePrefix,IEnumerable<Type> servicesTypes) 
            : base(  controllerBuilderFactory, servicePrefix,servicesTypes)
        {
        }
    }
}