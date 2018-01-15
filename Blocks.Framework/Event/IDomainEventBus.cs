using System;
using System.Threading.Tasks;
using Abp.Events.Bus;
 

namespace Blocks.Framework.Event
{
    public interface IDomainEventBus
    {
        void Trigger<TEventData>(TEventData eventData) where TEventData : IDomainEventData;
        
        
//         #region Register
//
//        /// <summary>
//        /// Registers to an event.
//        /// Given action is called for all event occurrences.
//        /// </summary>
//        /// <param name="action">Action to handle events</param>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        IDisposable  Register<TEventData>(Action<TEventData> action) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Registers to an event. 
//        /// Same (given) instance of the handler is used for all event occurrences.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="handler">Object to handle the event</param>
//        IDisposable Register<TEventData>(IDomainEventHandler<TEventData> handler) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Registers to an event.
//        /// A new instance of <see cref="THandler"/> object is created for every event occurrence.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <typeparam name="THandler">Type of the event handler</typeparam>
//        IDisposable Register<TEventData, THandler>() where TEventData : IDomainEventData where THandler : IDomainEventHandler<TEventData>, new();
//
//        /// <summary>
//        /// Registers to an event.
//        /// Same (given) instance of the handler is used for all event occurrences.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="handler">Object to handle the event</param>
//        IDisposable Register(Type eventType, IDomainEventHandler handler);
//
//        /// <summary>
//        /// Registers to an event.
//        /// Given factory is used to create/release handlers
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="handlerFactory">A factory to create/release handlers</param>
//        IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Registers to an event.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="handlerFactory">A factory to create/release handlers</param>
//        IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory);
//
//        #endregion
//
//        #region Unregister
//
//        /// <summary>
//        /// Unregisters from an event.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="action"></param>
//        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Unregisters from an event.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="handler">Handler object that is registered before</param>
//        void Unregister<TEventData>(IDomainEventHandler<TEventData> handler) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Unregisters from an event.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="handler">Handler object that is registered before</param>
//        void Unregister(Type eventType, IDomainEventHandler handler);
//
//        /// <summary>
//        /// Unregisters from an event.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="factory">Factory object that is registered before</param>
//        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Unregisters from an event.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="factory">Factory object that is registered before</param>
//        void Unregister(Type eventType, IEventHandlerFactory factory);
//
//        /// <summary>
//        /// Unregisters all event handlers of given event type.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        void UnregisterAll<TEventData>() where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Unregisters all event handlers of given event type.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        void UnregisterAll(Type eventType);
//
//        #endregion
//
//        #region Trigger
//
//        /// <summary>
//        /// Triggers an event.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="eventData">Related data for the event</param>
//        void Trigger<TEventData>(TEventData eventData) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Triggers an event.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="eventSource">The object which triggers the event</param>
//        /// <param name="eventData">Related data for the event</param>
//        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Triggers an event.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="eventData">Related data for the event</param>
//        void Trigger(Type eventType, IDomainEventData eventData);
//
//        /// <summary>
//        /// Triggers an event.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="eventSource">The object which triggers the event</param>
//        /// <param name="eventData">Related data for the event</param>
//        void Trigger(Type eventType, object eventSource, IDomainEventData eventData);
//
//        /// <summary>
//        /// Triggers an event asynchronously.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="eventData">Related data for the event</param>
//        /// <returns>The task to handle async operation</returns>
//        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Triggers an event asynchronously.
//        /// </summary>
//        /// <typeparam name="TEventData">Event type</typeparam>
//        /// <param name="eventSource">The object which triggers the event</param>
//        /// <param name="eventData">Related data for the event</param>
//        /// <returns>The task to handle async operation</returns>
//        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEventData;
//
//        /// <summary>
//        /// Triggers an event asynchronously.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="eventData">Related data for the event</param>
//        /// <returns>The task to handle async operation</returns>
//        Task TriggerAsync(Type eventType, IDomainEventData eventData);
//
//        /// <summary>
//        /// Triggers an event asynchronously.
//        /// </summary>
//        /// <param name="eventType">Event type</param>
//        /// <param name="eventSource">The object which triggers the event</param>
//        /// <param name="eventData">Related data for the event</param>
//        /// <returns>The task to handle async operation</returns>
//        Task TriggerAsync(Type eventType, object eventSource, IDomainEventData eventData);
//
//
//        #endregion
    }
}