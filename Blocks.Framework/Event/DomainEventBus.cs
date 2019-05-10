using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp;
using Abp.Events.Bus;
using Abp.Events.Bus.Factories.Internals;
using Abp.Events.Bus.Handlers.Internals;
using Abp.Extensions;
using Abp.Threading.Extensions;
using Blocks.Framework.Ioc.Dependency;
using Castle.Core.Logging;

namespace Blocks.Framework.Event
{
    public class DomainEventBus :  IDomainEventBus, ISingletonDependency
    {
        private IEventBus _eventBus;
        public ILogger Logger { get; set; }

        public DomainEventBus(IEventBus eventBus)
        {
            _eventBus = eventBus;
            Logger = NullLogger.Instance;
        }
        
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IDomainEventData
        {
            Stopwatch sw = Stopwatch.StartNew();
            
            _eventBus.Trigger((object)null, eventData);
            sw.Stop();
            Logger.Debug($"DomainEvent Trigger {typeof(TEventData).FullName} cost time {sw.ElapsedMilliseconds}ms");

           
        }
    }
//    public class DomainEventBus : EventBus,IDomainEventBus
//    {
//        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IDomainEventData
//        {
//            return Register(typeof(TEventData), new ActionEventHandler<TEventData>(action));
//        }
//
//        public IDisposable Register<TEventData>(IDomainEventHandler<TEventData> handler) where TEventData : IDomainEventData
//        {
//            return Register(typeof(TEventData), handler);
//        }
//
//        public IDisposable Register<TEventData, THandler>() where TEventData : IDomainEventData where THandler : IDomainEventHandler<TEventData>, new()
//        {
//            return Register(typeof(TEventData), new TransientEventHandlerFactory<THandler>());
//
//        }
//
//        public IDisposable Register(Type eventType, IDomainEventHandler handler)
//        {
//            return Register(eventType, new SingleInstanceHandlerFactory(handler));
//
//        }
//
//        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IDomainEventData
//        {
//            return Register(typeof(TEventData), handlerFactory);
//
//        }
//
//        public IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory)
//        {
//            GetOrCreateHandlerFactories(eventType)
//                .Locking(factories => factories.Add(handlerFactory));
//
//            return new FactoryUnregistrar(this, eventType, handlerFactory);
//        }
//
//        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IDomainEventData
//        {
//            Check.NotNull(action, nameof(action));
//
//            GetOrCreateHandlerFactories(typeof(TEventData))
//                .Locking(factories =>
//                {
//                    factories.RemoveAll(
//                        factory =>
//                        {
//                            var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
//                            if (singleInstanceFactory == null)
//                            {
//                                return false;
//                            }
//
//                            var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEventData>;
//                            if (actionHandler == null)
//                            {
//                                return false;
//                            }
//
//                            return actionHandler.Action == action;
//                        });
//                });
//        }
//
//        public void Unregister<TEventData>(IDomainEventHandler<TEventData> handler) where TEventData : IDomainEventData
//        {
//            Unregister(typeof(TEventData), handler);
//        }
//
//        public void Unregister(Type eventType, IDomainEventHandler handler)
//        {
//            GetOrCreateHandlerFactories(eventType)
//                .Locking(factories =>
//                {
//                    factories.RemoveAll(
//                        factory =>
//                            factory is SingleInstanceHandlerFactory &&
//                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
//                    );
//                });
//        }
//
//        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IDomainEventData
//        {
//            Unregister(typeof(TEventData), factory);
//        }
//
//        public void Unregister(Type eventType, IEventHandlerFactory factory)
//        {
//            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
//
//        }
//
//        public void UnregisterAll<TEventData>() where TEventData : IDomainEventData
//        {
//            UnregisterAll(typeof(TEventData));
//        }
//
//        public void Trigger<TEventData>(TEventData eventData) where TEventData : IDomainEventData
//        {
//            Trigger((object)null, eventData);
//
//        }
//
//        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEventData
//        {
//            Trigger(typeof(TEventData), eventSource, eventData);
//
//        }
//
//        public void Trigger(Type eventType, IDomainEventData eventData)
//        {
//            Trigger(eventType, null, eventData);
//
//        }
//
//        public void Trigger(Type eventType, object eventSource, IDomainEventData eventData)
//        {
//            var exceptions = new List<Exception>();
//
//            TriggerHandlingException(eventType, eventSource, eventData, exceptions);
//
//            if (exceptions.Any())
//            {
//                if (exceptions.Count == 1)
//                {
//                    exceptions[0].ReThrow();
//                }
//
//                throw new AggregateException("More than one error has occurred while triggering the event: " + eventType, exceptions);
//            }
//        }
//
//        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IDomainEventData
//        {
//            return TriggerAsync((object)null, eventData);
//        }
//
//        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IDomainEventData
//        {
//            ExecutionContext.SuppressFlow();
//
//            var task = Task.Factory.StartNew(
//                () =>
//                {
//                    try
//                    {
//                        Trigger(eventSource, eventData);
//                    }
//                    catch (Exception ex)
//                    {
//                        Logger.Warn(ex.ToString(), ex);
//                    }
//                });
//
//            ExecutionContext.RestoreFlow();
//
//            return task;
//        }
//
//        public Task TriggerAsync(Type eventType, IDomainEventData eventData)
//        {
//            return TriggerAsync(eventType, null, eventData);
//        }
//
//        public Task TriggerAsync(Type eventType, object eventSource, IDomainEventData eventData)
//        {
//            ExecutionContext.SuppressFlow();
//
//            var task = Task.Factory.StartNew(
//                () =>
//                {
//                    try
//                    {
//                        Trigger(eventType, eventSource, eventData);
//                    }
//                    catch (Exception ex)
//                    {
//                        Logger.Warn(ex.ToString(), ex);
//                    }
//                });
//
//            ExecutionContext.RestoreFlow();
//
//            return task;
//        }
//        
//        
//        private IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
//        {
//            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();
//
//            foreach (var handlerFactory in _handlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
//            {
//                handlerFactoryList.Add(new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
//            }
//
//            return handlerFactoryList.ToArray();
//        }
//
//        private static bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
//        {
//            //Should trigger same type
//            if (handlerType == eventType)
//            {
//                return true;
//            }
//
//            //Should trigger for inherited types
//            if (handlerType.IsAssignableFrom(eventType))
//            {
//                return true;
//            }
//
//            return false;
//        }
//        
//        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
//        {
//            return _handlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());
//        }
//
//        private class EventTypeWithEventHandlerFactories
//        {
//            public Type EventType { get; }
//
//            public List<IEventHandlerFactory> EventHandlerFactories { get; }
//
//            public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
//            {
//                EventType = eventType;
//                EventHandlerFactories = eventHandlerFactories;
//            }
//        }
//    }
}