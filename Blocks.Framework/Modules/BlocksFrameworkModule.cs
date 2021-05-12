using System;
using System.Reflection;
using Abp.Modules;
using Blocks.Framework.Caching;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Blocks.Framework.Environment.Extensions.Folders;
using Castle.MicroKernel;
using System.Collections;
using Abp.Localization.Dictionaries;
using Blocks.Framework.Environment;
using Blocks.Framework.Environment.Configuration;
using Abp.Localization.Sources;
using Blocks.Framework.Localization;
using Abp.Localization.Dictionaries.Xml;
using Blocks.Framework.Auditing;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Event;
using Blocks.Framework.Ioc;
using Blocks.Framework.Navigation;
using Blocks.Framework.Localization.Provider;
using Blocks.Framework.RPCProxy;
using Blocks.Framework.Domain;
using Blocks.Framework.Security;
using Blocks.Framework.License;

namespace Blocks.Framework.Modules
{
    [DependsOn(typeof(LicenseModule))]
    [DependsOn(typeof(BlocksAutoMapperModule))]
    [DependsOn(typeof(CacheModule))]
    
//    [DependsOn(typeof(EventModule))]
    [DependsOn(typeof(EnvironmentModule))]
    [DependsOn(typeof(LocalizationModule))]
    [DependsOn(typeof(AuditModule))]

    [DependsOn(typeof(NavigationModule))]
    [DependsOn(typeof(SecurityModule))]
    
    
    [DependsOn(typeof(RPCProxyModule))]

    [DependsOn(typeof(DomianModule))]

    public class BlocksFrameworkModule: AbpModule
    {
         
       
        public override void PreInitialize()
        {
            AuditingInterceptorRegistrar.Initialize(IocManager);

            //TODO it should be move to more early first Config, but it is belong to Adp
            IocManager.IocContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(IocManager.IocContainer.Kernel));
             
            Configuration.Auditing.IsEnabled = true;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            //            IocManager.Register<WindsorInstanceProvier,WindsorInstanceProvier>();
            //
            //            var iocProvider = IocManager.Resolve<WindsorInstanceProvier>(IocManager);
            //            iocProvider.RegisterKernelCompoentEvnet();
        }

        public override void Initialize()
        {


            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Modules.BlocksAutoMapper().Configurators.Add(FrameworkProfile.DefaultAutoMapperConfig());
           // Configuration.Modules.AbpAutoMapper().Configurators.Add(FrameworkProfile.DefaultAutoMapperConfig());
        }
    }
}