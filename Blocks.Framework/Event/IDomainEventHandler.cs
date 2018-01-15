using Abp.Events.Bus.Handlers;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Event
{
    public interface IDomainEventHandler<in TEventData> : IDomainEventHandler, IEventHandler<TEventData>,ITransientDependency
    {
        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        void HandleEvent(TEventData eventData);
    }
    
    
    public interface IDomainEventHandler : IEventHandler
    {
        
    }
}