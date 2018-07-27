using System.Linq;
using Abp.Modules;
using Blocks.Framework.Collections;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace Blocks.Framework.Ioc
{
    public class ProcessModelEvent : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            kernel.Resolve<IAbpModuleManager>().Modules.ForEach(m =>
            {
                if (m.Instance is BlocksModule)
                    ((BlocksModule) m.Instance).OnRegistered(kernel, model);
            });
        }
    }
}