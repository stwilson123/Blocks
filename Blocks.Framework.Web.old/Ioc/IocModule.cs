using Abp.Modules;

namespace Blocks.Framework.Web.Ioc
{
    public class IocModule: AbpModule
    {
        
        public override void Initialize()
        {
            
            IocManager.AddConventionalRegistrar(new DependencyConventionalRegistrar(IocManager)); 

        }
        
    }
}