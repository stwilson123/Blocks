using System.Reflection;
using Abp.Events.Bus;
using Abp.Events.Bus.Factories;
using Blocks.Framework.Ioc;
using Castle.MicroKernel;

namespace Blocks.Framework.Event
{
//    public class EventModule: BlocksModule
//    {
//        private IEventBus _eventBus;
//
//        public override void Initialize()
//        {
//            _eventBus = IocManager.Resolve<IEventBus>();
//            IocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
//            
//        }
//        
//        private void Kernel_ComponentRegistered(string key, IHandler handler)
//        {
//            /* This code checks if registering component implements any IEventHandler<TEventData> interface, if yes,
//             * gets all event handler interfaces and registers type to Event Bus for each handling event.
//             */
//            if (!typeof(IDomainEventHandler).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
//            {
//                return;
//            }
//
//            var interfaces = handler.ComponentModel.Implementation.GetTypeInfo().GetInterfaces();
//            foreach (var @interface in interfaces)
//            {
//                if (!typeof(IDomainEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
//                {
//                    continue;
//                }
//
//                var genericArgs = @interface.GetGenericArguments();
//                if (genericArgs.Length == 1)
//                {
//                    _eventBus.Register(genericArgs[0], new IocHandlerFactory(IocManager, handler.ComponentModel.Implementation));
//                }
//            }
//        }
//    }
}