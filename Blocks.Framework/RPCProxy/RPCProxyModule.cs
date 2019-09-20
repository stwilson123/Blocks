using Blocks.Framework.Ioc;
using Blocks.Framework.Modules;
using Blocks.Framework.RPCProxy.Manager;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BlocksModule = Blocks.Framework.Ioc.BlocksModule;

namespace Blocks.Framework.RPCProxy
{
    public class RPCProxyModule : BlocksModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<RPCApiManager>();
            IocManager.Register<RPCInterceptor>(lifeStyle: Abp.Dependency.DependencyLifeStyle.Transient);

        }
        public override void PostInitialize()
        {

        

            foreach (var proxyType in IocManager.Resolve<RPCApiManager>().GetAll())
            {
                var rpcClientProxies = proxyType.GetInterfaces().Where(t => typeof(IRPCClientProxy).IsAssignableFrom(t))
                    .ToArray();
                if (rpcClientProxies.Length > 0)
                {
                    IocManager.IocContainer.Register(
                        Component
                            .For(rpcClientProxies)
                            .ImplementedBy(proxyType)
                            .Activator<DefaultBlocksComponentActivator>()
                            .Interceptors(typeof(RPCInterceptor))
                            .LifestyleTransient()
                    );
                }
                else
                {
                    IocManager.IocContainer.Register(
                        Component
                            .For(proxyType)
                            .Activator<DefaultBlocksComponentActivator>()
                            .Interceptors(typeof(RPCInterceptor))
                            .LifestyleTransient()
                    );
                }
               

                //LogHelper.Logger.DebugFormat(
                //    "Dynamic web api controller is created for type '{0}' with service name '{1}'.",
                //    controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }

        }
    }
}
