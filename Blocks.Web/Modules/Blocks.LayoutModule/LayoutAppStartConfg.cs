using Blocks.Framework.Web.Configuartions;

namespace Blocks.LayoutModule
{
    public class LayoutAppStartConfg : IWebFrameworkConfiguration
    {
        public string AppModule { get; }
        public string RespositoryModule { get; }
        public string DomainModule { get; }
    }
}