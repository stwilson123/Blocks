using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Dependency;
using Blocks.Framework.ApplicationServices.Attributes;
using Blocks.Framework.ApplicationServices.Controller.Attributes;
using Blocks.Framework.ApplicationServices.Controller.Helper;
using Blocks.Framework.ApplicationServices.Filters;
using Blocks.Framework.ApplicationServices.Manager;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.Reflection.Extensions;
using Blocks.Framework.Types;

namespace Blocks.Framework.ApplicationServices.Controller.Builder
{
    public class DefaultControllerBuilder<T,TControllerActionBuilder> : IDefaultControllerBuilder<T> where TControllerActionBuilder :DefaultControllerActionBuilder<T>  
    {
        private IIocResolver _iocResolver;
        protected Dictionary<string, TControllerActionBuilder> _actionBuilders;
        public string ServiceName { get; set; }
        public Type ServiceInterfaceType { get; set; }
        public bool? IsApiExplorerEnabled { get; set; }

        public IFilter[] Filters { set; get; }
        protected IControllerRegister _defaultControllerManager;

        protected virtual Type ApiControllerType
        {
            get { return typeof(NopController); }
        }

        /// <summary>
        /// Creates a new instance of ApiControllerInfoBuilder.
        /// </summary>
        /// <param name="serviceName">Name of the controller</param>
        /// <param name="iocResolver">Ioc resolver</param>
        public DefaultControllerBuilder(string serviceName, IIocResolver iocResolver, IControllerRegister defaultControllerManager)
        {
            Check.NotNull(iocResolver, nameof(iocResolver));

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("serviceName null or empty!", "serviceName");
            }

            if (!DynamicApiServiceNameHelper.IsValidServiceName(serviceName))
            {
                throw new ArgumentException("serviceName is not properly formatted! It must contain a single-depth namespace at least! For example: 'myapplication/myservice'.", "serviceName");
            }

            _iocResolver = iocResolver;
            _defaultControllerManager = defaultControllerManager;

            ServiceName = serviceName;
            ServiceInterfaceType = typeof (T);

            _actionBuilders = new Dictionary<string, TControllerActionBuilder>();
            var methodInfos = DynamicApiControllerActionHelper.GetMethodsOfType(typeof(T))
                .Where(methodInfo => methodInfo.GetSingleAttributeOrNull<BlocksActionNameAttribute>() != null);
            foreach (var methodInfo in methodInfos)
            {
                var actionBuilder = (TControllerActionBuilder)typeof(TControllerActionBuilder).New(this, methodInfo, iocResolver);
                var remoteServiceAttr = methodInfo.GetSingleAttributeOrNull<RemoteServiceAttribute>();
                if (remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(methodInfo))
                {
                    actionBuilder.DontCreateAction();
                }
                var actionNameAttr = methodInfo.GetSingleAttributeOrNull<BlocksActionNameAttribute>();
 

                _actionBuilders[actionNameAttr.ActionName] =
                    actionBuilder;
            }
        }
        
        
        
        public IDefaultControllerBuilder<T> WithApiExplorer(bool isEnabled)
        {
            IsApiExplorerEnabled = isEnabled;
            return this;
        }
      
        
     
        public IDefaultControllerActionBuilder<T> GetMethod(string methodName)
        {
            if (!_actionBuilders.ContainsKey(methodName))
            {
                throw new BlocksException(StringLocal.Format("There is no method with name " + methodName + " in type " + typeof(T).Name));
            }

            return _actionBuilders[methodName];
        }
        public IDefaultControllerBuilder<T> ForMethods(Action<IDefaultControllerActionBuilder> action)
        {
            foreach (var actionBuilder in _actionBuilders.Values)
            {
                action(actionBuilder);
            }

            return this;
        }
        public virtual void Build()
        {
            var controllerInfo = new DefaultControllerInfo<DefaultControllerActionInfo>(
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
                controllerInfo.Actions[actionBuilder.ActionName] = actionBuilder.GetResult();
            }

            _defaultControllerManager.Register(controllerInfo);
        }

    }
}