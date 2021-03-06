﻿using System;
using Abp.Dependency;
using Abp.Modules;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Caching {
    public class CacheModule : AbpModule {
//        protected override void Load(ContainerBuilder builder) {
//            builder.RegisterType<DefaultCacheManager>()
//                .As<ICacheManager>()
//                .InstancePerDependency();
//        }

        public override void Initialize()
        {
            
            IocManager.Register<ICacheContextAccessor, DefaultCacheContextAccessor>();

            IocManager.Register<IParallelCacheContext, DefaultParallelCacheContext>();

            //IocManager.Register<ICacheManager,DefaultCacheManager>(DependencyLifeStyle.Transient);

            IocManager.Register<ICacheManager, DefaultCacheManager>((kernel, componentModel, creationContext) =>
            {
                var resolutionContext = creationContext.SelectScopeRoot((t) => t.Length >= 2 ? t[t.Length - 2] : null);
                var handler = resolutionContext != null ? resolutionContext.Handler : null;
                if (handler == null)
                    throw new BlocksException(StringLocal.Format("Can't find suitable handler in resolutionContext."));
                return new DefaultCacheManager(handler.ComponentModel.Implementation.UnderlyingSystemType,  kernel.Resolve<ICacheHolder>());
            }, DependencyLifeStyle.Transient);
        }

//        protected override void AttachToComponentRegistration(Autofac.Core.IComponentRegistry componentRegistry, Autofac.Core.IComponentRegistration registration) {
//            var needsCacheManager = registration.Activator.LimitType
//                .GetConstructors()
//                .Any(x => x.GetParameters()
//                    .Any(xx => xx.ParameterType == typeof(ICacheManager)));
//
//            if (needsCacheManager) {
//                registration.Preparing += (sender, e) => {
//                    var parameter = new TypedParameter(
//                        typeof(ICacheManager),
//                        e.Context.Resolve<ICacheManager>(new TypedParameter(typeof(Type), registration.Activator.LimitType)));
//                    e.Parameters = e.Parameters.Concat(new[] { parameter });
//                };
//            }
//        }
    }
}
