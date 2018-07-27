using System;
using Abp.Dependency;
using Blocks.Framework.ApplicationServices.Controller;
using Blocks.Framework.ApplicationServices.Controller.Builder;
using Blocks.Framework.ApplicationServices.Manager;

namespace Blocks.Framework.Web.Mvc.Controllers.Builder
{
    public class MvcControllerBuilder<T,TControllerActionBuilder> : DefaultControllerBuilder<T,TControllerActionBuilder> where TControllerActionBuilder : MvcControllerActionBuilder<T>
    {
        public bool ConventionalVerbs { get; set; }
        protected override Type ApiControllerType
        {
            get
            {
                return typeof(BlocksWebMvcController);
            }
        }

        public MvcControllerBuilder(string serviceName, IIocResolver iocResolver, IControllerRegister defaultControllerManager) : base(serviceName, iocResolver, defaultControllerManager)
        {
        }

        public IDefaultControllerBuilder<T> WithConventionalVerbs()
        {
            ConventionalVerbs = true;
            return this;
        }
        public override void Build()
        {
            var controllerInfo = new DefaultControllerInfo<MvcControllerActionInfo>(
                ServiceName,
                ServiceInterfaceType,
                ApiControllerType,
                null, //TODO
                Filters,
                IsApiExplorerEnabled
            );
            
            foreach (var actionBuilder in _actionBuilders.Values)
            {
                if (actionBuilder.DontCreate)
                {
                    continue;
                }
                actionBuilder.Build();
                controllerInfo.Actions[actionBuilder.ActionName] = actionBuilder.GetResult() as MvcControllerActionInfo;
            }

            _defaultControllerManager.Register(controllerInfo);
        }
    }
}