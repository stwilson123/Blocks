using Blocks.Framework.Ioc;
using Blocks.Framework.Modules;
using BlocksModule = Blocks.Framework.Ioc.BlocksModule;

namespace Blocks.Core
{
    public class BlocksStartModule : BlocksModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(this.GetType().Assembly);
        }
    }
}