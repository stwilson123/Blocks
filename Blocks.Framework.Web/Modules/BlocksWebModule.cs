using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Exception;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Web.Configuartions;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Route;

namespace Blocks.Framework.Web.Modules
{
    public abstract class BlocksWebModule : AbpModule
    {
     
        public override void PreInitialize()
        {
           
         
        }

        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public override void Initialize()
        {

            var currentAssmebly = this.GetType().GetTypeInfo().Assembly;
            var currentAssmeblyName = currentAssmebly.GetName().Name;
            IocManager.RegisterAssemblyByConvention(currentAssmebly);

            

//            var AppModule =  System.AppDomain.CurrentDomain.GetAssemblies()
//                .FirstOrDefault(t => t.FullName.IndexOf("BussnessApplicationModule") > 0);
//
//            IocManager.RegisterAssemblyByConvention(AppModule);
//
//           
//            var RepModule =  System.AppDomain.CurrentDomain.GetAssemblies()
//                .FirstOrDefault(t => t.FullName.IndexOf("BussnessRespositoryModule") > 0);
//
//            IocManager.RegisterAssemblyByConvention(RepModule);
//            
//            var DomainModule =  System.AppDomain.CurrentDomain.GetAssemblies()
//                .FirstOrDefault(t => t.FullName.IndexOf("BussnessDomainModule") > 0);

//            IocManager.RegisterAssemblyByConvention(DomainModule);
            
            
//            var RepModule = Assembly.LoadFile(
//                $"{AppModule.Location.Substring(0,AppModule.Location.LastIndexOf(@"\", StringComparison.Ordinal))}\\Blocks.BussnessRespositoryModule.dll"
//            );
//            IocManager.RegisterAssemblyByConvention(RepModule);
//            
//            
//            var DomainModule = Assembly.LoadFile(
//                $"{AppModule.Location.Substring(0,AppModule.Location.LastIndexOf(@"\", StringComparison.Ordinal))}\\Blocks.BussnessDomainModule.dll"
//            );
//            IocManager.RegisterAssemblyByConvention(DomainModule);
//            


            var Extension = IocManager.Resolve<IExtensionManager>().AvailableExtensions()
                .FirstOrDefault(t => t.Id == currentAssmeblyName);
            if(Extension == null)
                throw  new ExtensionNotFoundException($"{currentAssmeblyName} can't found extension depond on it");

            var AppModule = default(Assembly);
            var configKey = $"{Extension.Name}\\{ConfiguartionConventionalRegistrar.AppConfigKey}";
            if (IocManager.IsRegistered(configKey))
            {
                var moduleConfiguration = IocManager.Resolve<IConfiguration>(configKey);
                if (moduleConfiguration is IWebFrameworkConfiguration)
                {
                    var webModuleConfiguration =  (moduleConfiguration as IWebFrameworkConfiguration);
              
                   if(!string.IsNullOrEmpty(webModuleConfiguration.AppModule))
                    {
                        AppModule =  System.AppDomain.CurrentDomain.GetAssemblies()
                            .FirstOrDefault(t => string.Equals(t.GetName().Name,webModuleConfiguration.AppModule,StringComparison.CurrentCultureIgnoreCase));
                        IocManager.RegisterAssemblyByConvention(AppModule);

                        Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                                       .ForAll<IApplicationService>(AppModule, Extension.Name)
                                       .Build();
                    }
                    
                    if(!string.IsNullOrEmpty(webModuleConfiguration.RespositoryModule))
                    {
                        var RepModule =  System.AppDomain.CurrentDomain.GetAssemblies()
                            .FirstOrDefault(t => string.Equals(t.GetName().Name,webModuleConfiguration.RespositoryModule,StringComparison.CurrentCultureIgnoreCase));
                        IocManager.RegisterAssemblyByConvention(RepModule);
                    }
                    
                    if(!string.IsNullOrEmpty(webModuleConfiguration.DomainModule))
                    {
                        var DomainModule =  System.AppDomain.CurrentDomain.GetAssemblies()
                            .FirstOrDefault(t => string.Equals(t.GetName().Name,webModuleConfiguration.DomainModule,StringComparison.CurrentCultureIgnoreCase));
                        IocManager.RegisterAssemblyByConvention(DomainModule);
                    }
                }
            }
            
          


            RouteHandle(Extension.Name);
            InitializeEvent();
        }


        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public virtual void InitializeEvent()
        {

        }
 

        private void RouteHandle(string featureName)
        {
            var listRouteDesc = new List<RouteDescriptor>();
            IocManager.Resolve<IRouteProvider>().GetRoutes(listRouteDesc, featureName);
            IocManager.Resolve<IHttpRouteProvider>().GetRoutes(listRouteDesc, featureName);

            IocManager.Resolve<IRoutePublisher>().Publish(listRouteDesc);
           
        }
    }
}
