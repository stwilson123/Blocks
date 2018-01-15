using Abp.Events.Bus.Handlers;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Event
{
    public interface IDomainEventHandler<in TEventData> : IEventHandler<TEventData>,IDomainEventHandler,ITransientDependency
    {
      
    }
    
    
    public interface IDomainEventHandler : IEventHandler
    {
        
    }
}