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

       
        

        protected override void SetUpProperties(object instance, CreationContext context)
        {
            base.SetUpProperties(instance, context);

        
            Kernel.Resolve<IAbpModuleManager>().Modules.ForEach(m =>
            {
                if (m.Instance is BlocksModule)
                    ((BlocksModule)m.Instance).OnActivated(instance);
            });

          

        }

    }
}