using Abp.Events.Bus.Handlers;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Event
{
    public interface IDomainEventHandler<in TEventData> : IDomainEventHandler,IEventHandler<TEventData>
    {
      
    }
    
    
    public interface IDomainEventHandler : IEventHandler,ITransientDependency
    {
        
    }
}