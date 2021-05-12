using System.Reflection;
using Abp.Modules;
using Blocks.Framework.Web.Web;

namespace Blocks.Owin
{
    /// <summary>
    /// OWIN integration module for Blocks.
    /// </summary>
    [DependsOn(typeof (BlocksWebModule))]
    public class BlocksOwinModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
