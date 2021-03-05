using System;
using Abp.Dependency;
using Castle.Core.Logging;

namespace Blocks.Web.Security.AntiForgery
{
    public class BlocksAntiForgeryManager : IBlocksAntiForgeryManager, IBlocksAntiForgeryValidator, ITransientDependency
    {
        public ILogger Logger { protected get; set; }

        public IBlocksAntiForgeryConfiguration Configuration { get; }

        public BlocksAntiForgeryManager(IBlocksAntiForgeryConfiguration configuration)
        {
            Configuration = configuration;
            Logger = NullLogger.Instance;
        }

        public virtual string GenerateToken()
        {
            return Guid.NewGuid().ToString("D");
        }

        public virtual bool IsValid(string cookieValue, string tokenValue)
        {
            return cookieValue == tokenValue;
        }
    }
}