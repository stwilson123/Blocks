using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.PlugIns;
using Blocks.Framework.Configurations;
using Blocks.Framework.Environment.Exception;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Modules
{
    public abstract class BlocksModule : AbpModule
    {
        private Assembly currentAssmebly { get { return this.GetType().GetTypeInfo().Assembly; } }

        private ExtensionDescriptor extensionDescriptor
        {
            get
            {
                var currentAssmeblyName = currentAssmebly.GetName().Name;
                var Extension = IocManager.Resolve<IExtensionManager>().AvailableExtensions()
             .FirstOrDefault(t => t.Id == currentAssmeblyName);
                if (Extension == null)
                    throw new ExtensionNotFoundException(StringLocal.Format($"{currentAssmeblyName} can't found extension depond on it."));
                return Extension;
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
            // var currentAssmeblyName = currentAssmebly.GetName().Name;
            //var extensionName = extensionDescriptor.Name;
            IocManager.RegisterAssemblyByConvention(currentAssmebly);

            
             
            InitializeEvent();
        }


        public override void PostInitialize()
        {


            var currentAssmeblyName = currentAssmebly.GetName().Name;
         //   var extensionName = extensionDescriptor.Name;

            
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

         
    }
}
