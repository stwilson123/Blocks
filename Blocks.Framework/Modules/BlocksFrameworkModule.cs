using System;
using Abp.Modules;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace Blocks.Framework.Modules
{
    public class BlocksFrameworkModule: AbpModule
    {
        public override void PreInitialize()
        {
            //TODO it should be move to more early first Config, but it is belong to Adp
            IocManager.IocContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(IocManager.IocContainer.Kernel));
//            IocManager.Register<WindsorInstanceProvier,WindsorInstanceProvier>();
//
//            var iocProvider = IocManager.Resolve<WindsorInstanceProvier>(IocManager);
//            iocProvider.RegisterKernelCompoentEvnet();
        }

        public override void Initialize()
        {
             
        }
    }
}