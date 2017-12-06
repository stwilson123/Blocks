using System;
using Abp.Dependency;
using Abp.Modules;

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

            IocManager.Register<ICacheManager,DefaultCacheManager>((kernel, componentModel, creationContext) =>
            {
                return (DefaultCacheManager)kernel.Resolve<ICacheManager>(new { component = typeof(string), cacheHolder = kernel.Resolve<ICacheHolder>()});

            },DependencyLifeStyle.Transient);
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
