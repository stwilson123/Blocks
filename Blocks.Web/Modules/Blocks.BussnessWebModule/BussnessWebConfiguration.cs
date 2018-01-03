using Blocks.Framework.Data.Entity;
using Blocks.Framework.Web.Configuartions;

namespace Blocks.BussnessWebModule
{
    public class BussnessWebConfiguration : IWebFrameworkConfiguration,IEntityConfiguration
    {
        public string AppModule { get; } = "Blocks.BussnessApplicationModule";
        public string RespositoryModule { get; } = "Blocks.BussnessRespositoryModule";
        public string DomainModule { get; } = "Blocks.BussnessDomainModule";
        public string EntityModule { get; } = "Blocks.BussnessEntityModule";
    }
}