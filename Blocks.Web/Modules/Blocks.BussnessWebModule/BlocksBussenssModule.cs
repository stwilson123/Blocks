using Blocks.BussnessWebModule.Module_Start;
using Blocks.Framework.Web.Modules;

namespace Blocks.BussnessWebModule
{
    public class BlocksBussenssModule : BlocksWebModule
    {
        public override void PreInitializeEvent()
        {
            Configuration.Navigation.Providers.Add<BlocksNavigationProvider>();

        }

        public override void InitializeEvent()
        {
        }
    }
}
