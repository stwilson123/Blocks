using Blocks.Framework.Web.Configuartions;

namespace Blocks.BussenssWebModule
{
    public class BussnessWebConfig : IWebFrameworkConfiguration
    {
        public string AppModule { get; } = "Blocks.BussnessApplicationModule";
        public string RespositoryModule { get; } = "Blocks.BussnessRespositoryModule";
        public string DomainModule { get; } = "Blocks.BussnessDomainModule";
    }
}