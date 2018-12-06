using Blocks.Framework.Ioc;
using Blocks.Framework.Modules;
using Blocks.Framework.RPCProxy.Manager;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
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
                IocManager.IocContainer.Register(
                    Component.For(proxyType)
                        .Activator<DefaultBlocksComponentActivator>()
                        .Interceptors(typeof(RPCInterceptor))
                        .LifestyleTransient()

                );

                //LogHelper.Logger.DebugFormat(
                //    "Dynamic web api controller is created for type '{0}' with service name '{1}'.",
                //    controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }

        }
    }
}
