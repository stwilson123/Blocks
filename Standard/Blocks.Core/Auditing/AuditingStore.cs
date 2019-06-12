using System.Threading.Tasks;
using Blocks.Framework.Auditing;
using Blocks.Framework.Event;
using Blocks.Framework.Ioc.Dependency;
using Castle.Core.Logging;

namespace Blocks.Core.Auditing
{
    public class AuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly IDomainEventBus _domainEventBus;
        public ILogger Logger { get; set; }


        public AuditingStore(IDomainEventBus domainEventBus)
        {
            _domainEventBus = domainEventBus;
        }
        
        
        /// <summary>
        /// Creates  a new <see cref="AuditingStore"/>.
        /// </summary>
        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
           
            
            _domainEventBus.Trigger(new AuditSaveEventData(){ AuditInfo = auditInfo});
            return Task.FromResult(true);
        }
    }
}