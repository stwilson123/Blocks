using Abp.Modules;
using Blocks.Framework.Collections;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;

namespace Blocks.Framework.Ioc
{
    public class DefaultBlocksComponentActivator : DefaultComponentActivator
    {
        public DefaultBlocksComponentActivator(ComponentModel model, IKernelInternal kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction) : base(model, kernel, onCreation, onDestruction)
        {
        }

        protected override object InternalCreate(CreationContext context)
        {
            var instance = base.InternalCreate(context);

            Kernel.Resolve<IAbpModuleManager>().Modules.ForEach(m =>
            {
                if (m.Instance is BlocksModule)
                    ((BlocksModule) m.Instance).OnActivated(instance);
            });
          

           
            return instance;
        }
    }
}