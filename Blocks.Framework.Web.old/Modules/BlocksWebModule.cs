using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.PlugIns;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Exception;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Web.Configuartions;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Route;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc;
using Blocks.Framework.Localization;
using Blocks.Framework.Utility.Extensions;
using Blocks.Framework.Web.Api.Configuration.Startup;
using Blocks.Framework.Web.Mvc.Controllers.Factory;
using Blocks.Framework.RPCProxy.Manager;
using Blocks.Framework.RPCProxy;
using Blocks.Framework.ApplicationServices;

namespace Blocks.Framework.Web.Modules
{
    public abstract class BlocksWebModule : AbpModule
    {

        private Assembly currentAssmebly
        {
            get { return this.GetType().GetTypeInfo().Assembly; }
        }

        private ExtensionDescriptor extensionDescriptor
        {
            get
            {
                var currentAssmeblyName = currentAssmebly.GetName().Name;
                var Extension = IocManager.Resolve<IExtensionManager>().AvailableExtensions()
                    .FirstOrDefault(t => t.Id == currentAssmeblyName);
                if (Extension == null)
                    throw new ExtensionNotFoundException(
                        StringLocal.Format($"{currentAssmeblyName} can't found extension depond on it."));
                return Extension;
            }
        }

        private FeatureDescriptor featureDescriptor
        {
            get
            {
                var currentAssmeblyName = currentAssmebly.GetName().Name;
                var Feature = IocManager.Resolve<IExtensionManager>().AvailableFeatures()
                    .FirstOrDefault(t => t.Id == currentAssmeblyName);
                if (Feature == null)
                    throw new ExtensionNotFoundException(
                        StringLocal.Format($"{currentAssmeblyName} can't found extension depond on it."));
                return Feature;
            }
        }
        private IConfiguration moduleConfiguration
        {
            get
            {
                var configKey = $"{extensionDescriptor.Name}\\{ConfiguartionConventionalRegistrar.AppConfigKey}";

                return IocManager.IsRegistered(configKey) ? IocManager.Resolve<IConfiguration>(configKey) : null;
            }
        }

        public override void PreInitialize()
        {
            PreInitializeEvent();
        }

        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public override void Initialize()
        {
            //Configuration.Localization.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        extensionDescriptor.Name,
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //            currentAssmebly, "Localization.Source"
            //        )
            //    )
            //);
            // var currentAssmeblyName = currentAssmebly.GetName().Name;
            var extensionName = extensionDescriptor.Name;
            IocManager.RegisterAssemblyByConvention(currentAssmebly);
             

            var serviceTypes = IocManager.IocContainer.Kernel.GetHandlers().SelectMany(t => t.Services);
            IocManager.Resolve<MvcControllerBuilderFactory>().ForAll<BlocksWebMvcController>(extensionName,
                serviceTypes.Where(t => t.Assembly == currentAssmebly)).Build();
            if (moduleConfiguration != null && moduleConfiguration is IWebFrameworkConfiguration)
            {
                var webModuleConfiguration = moduleConfiguration as IWebFrameworkConfiguration;
                if (!string.IsNullOrEmpty(webModuleConfiguration.AppModule))
                {
                    var AppModule = System.AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(t => string.Equals(t.GetName().Name, webModuleConfiguration.AppModule,
                            StringComparison.CurrentCultureIgnoreCase));
                    IocManager.RegisterAssemblyByConvention(AppModule);

                    Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                        .ForAll<IAppService>(AppModule, extensionName)
                        .Build();

                    IocManager.Resolve<RPCApiManager>().Register(
                        AppModule.GetTypes().Where(t => t.BaseType == typeof(IRPCProxy)).ToArray()
                        );
 

                    featureDescriptor.SubAssembly.AddIfNotContains(AppModule.GetName().Name);

                }

                if (!string.IsNullOrEmpty(webModuleConfiguration.DomainModule))
                {
                    var DomainModule = System.AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(t => string.Equals(t.GetName().Name, webModuleConfiguration.DomainModule,
                            StringComparison.CurrentCultureIgnoreCase));
                    IocManager.RegisterAssemblyByConvention(DomainModule);

                    IocManager.Resolve<RPCApiManager>().Register(
                        DomainModule.GetTypes().Where(t => t.BaseType == typeof(IRPCProxy)).ToArray()
                        );

                    featureDescriptor.SubAssembly.AddIfNotContains(DomainModule.GetName().Name);
                }
            }

            RouteHandle(extensionDescriptor.Name);
            InitializeEvent();
        }


        public override void PostInitialize()
        {
            var currentAssmeblyName = currentAssmebly.GetName().Name;
            var extensionName = extensionDescriptor.Name;

            var databaseType = IocManager.Resolve<ISettingManager>()
                .GetSettingValueForApplication(typeof(DatabaseType).Name);
            if (databaseType == null)
                throw new BlocksException(
                    StringLocal.Format($"{typeof(DatabaseType).Name} global configuartion can't found."));
            if (!Enum.GetNames(typeof(DatabaseType)).Contains(databaseType))
                throw new BlocksException(
                    StringLocal.Format(
                        $"{databaseType} isn't belong to global configuartion {typeof(DatabaseType).Name}."));

            if (moduleConfiguration != null && moduleConfiguration is IWebFrameworkConfiguration)
            {
                var webModuleConfiguration = moduleConfiguration as IWebFrameworkConfiguration;
                if (!string.IsNullOrEmpty(webModuleConfiguration.RespositoryModule))
                {
                    var RepModule = System.AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(t => string.Equals(t.GetName().Name, webModuleConfiguration.RespositoryModule,
                            StringComparison.CurrentCultureIgnoreCase));

                    var listAssemblies = IocManager.Resolve<AbpPlugInManager>()
                        .PlugInSources
                        .GetAllAssemblies()
                        .FirstOrDefault(t => t.GetName().Name == $"{RepModule.GetName().Name}.{databaseType}Module");

                    if (listAssemblies != null)
                    {
                        IocManager.RegisterAssemblyByConvention(listAssemblies);
                        featureDescriptor.SubAssembly.AddIfNotContains(listAssemblies.GetName().Name);
                    }

                    IocManager.RegisterAssemblyByConvention(RepModule);
                    
                    featureDescriptor.SubAssembly.AddIfNotContains(RepModule.GetName().Name);

                }
            }
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


        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public virtual void PreInitializeEvent()
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