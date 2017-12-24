using Blocks.Framework.Modules;
using Blocks.Framework.Web.Modules;

namespace Blocks.BussenssWebModule
{
    public class BlocksBussenssModule : BlocksWebModule
    {
        public override void InitializeEvent()
        {
            var types = typeof(BussnessWebConfiguration).GetInterfaces();
        }
    }
}
