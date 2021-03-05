using Blocks.Framework.Data.Entity;
using Blocks.Framework.Web.Configuartions;

namespace Blocks.BussnessWeb2Module
{
    public class BussnessWebConfiguration : IWebFrameworkConfiguration,IEntityConfiguration
    {
        public string AppModule { get; } = "Blocks.BussnessApplication2Module";
        public string RespositoryModule { get; } = "Blocks.BussnessRespository2Module";
        public string DomainModule { get; } = "";
        public string EntityModule { get; } = "Blocks.BussnessEntity2Module";
    }
}