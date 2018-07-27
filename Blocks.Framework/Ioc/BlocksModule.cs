using System.Linq;
using Abp.Modules;
using Castle.Core;
using Castle.MicroKernel;

namespace Blocks.Framework.Ioc
{
    public class BlocksModule : AbpModule
    {
        public virtual void OnRegistered(IKernel kernel, ComponentModel model)
        {
            ;
        }



        public virtual void OnActivated(object instance)
        {
            
        }
    }
}