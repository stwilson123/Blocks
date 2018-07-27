using Abp.Modules;
using Blocks.Framework.Navigation.Filters;
using Blocks.Framework.Navigation.Manager;

namespace Blocks.Framework.Navigation
{
    public class NavigationModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<INavigationManager,NavigationManager>();
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<INavigationManager>().Initialize();
        }
    }
}