using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Json;
using Blocks.Framework.Ioc.Dependency;
using Castle.Core.Logging;

namespace Blocks.Core.Auditing
{
    public class AuditingStore : IAuditingStore, ITransientDependency
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates  a new <see cref="AuditingStore"/>.
        /// </summary>


        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
            Logger.Debug(auditInfo.ToString());
            return Task.FromResult(true);
        }
    }
}