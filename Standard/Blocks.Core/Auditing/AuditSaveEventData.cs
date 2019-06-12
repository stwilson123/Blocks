using Blocks.Framework.Auditing;
using Blocks.Framework.Event;

namespace Blocks.Core.Auditing
{
    public class AuditSaveEventData : DomainEventData
    {
        public AuditInfo AuditInfo { get; set; }
    }
}