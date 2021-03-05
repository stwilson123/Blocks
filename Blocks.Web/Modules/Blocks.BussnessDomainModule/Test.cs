using System.Transactions;
using Blocks.Core.Auditing;
using Blocks.Framework.Domain.Uow;
using Blocks.Framework.Event;

namespace Blocks.BussnessDomainModule
{
    [UnitOfWork(TransactionScopeOption.RequiresNew)]
    public class Test: IDomainEventHandler<AuditSaveEventData>
    {
        
        public void HandleEvent(AuditSaveEventData eventData)
        {
            ;
        }
        public virtual void testc()
        {
            throw new System.NotImplementedException();
        }
    }
}