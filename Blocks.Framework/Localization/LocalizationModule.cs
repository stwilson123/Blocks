﻿using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using AutoMapper;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Collections;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Folders;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc;
using Blocks.Framework.Localization.Configuartions;
using Blocks.Framework.Localization.Provider;
using Blocks.Framework.Types;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using ILanguageProvider = Blocks.Framework.Localization.Provider.ILanguageProvider;
using XmlEmbeddedFileLocalizationDictionaryProvider = Blocks.Framework.Localization.Dictionaries.Xml.XmlEmbeddedFileLocalizationDictionaryProvider;

namespace Blocks.Framework.Localization
{
    public class LocalizationModule: BlocksModule
    {
        public LocalizationModule()
        {

        }
        public override void PreInitialize()
        {
            IocManager.Register<ILanguageProvider,LanguageProvider>();
            IocManager.Register<ILocalizationConfiguration,LocalizationConfiguration>();
            IocManager.Register<ILocalizationManager, LocalizationManager>();
            IocManager.Resolve<ILocalizationConfiguration>().Providers.Add(new XmlEmbeddedFileLocalizationDictionaryProvider(
                BlocksFrameworkLocalizationSource.LocalizationSourceName, Assembly.GetExecutingAssembly(), "Blocks.Framework.Localization.Source"
            ));
            
       
//            Configuration.Localization.Sources.Add(
//                new DictionaryBasedLocalizationSource(
//                    BlocksFrameworkLocalizationSource.LocalizationSourceName,
//                    new XmlEmbeddedFileLocalizationDictionaryProvider(
//                        Assembly.GetExecutingAssembly(), "Blocks.Framework.Localization.Source"
//                    )
//                )
//            );

  
//   
//                IocManager.Resolve<ILocalizationConfiguration>().Sources.Add(
//                    new LocalizationSourceList()
//                    {
//                     
//                    }
//                new DictionaryBasedLocalizationSource(
//                        BlocksFrameworkLocalizationSource.LocalizationSourceName,
//                        new XmlEmbeddedFileLocalizationDictionaryProvider(
//                            Assembly.GetExecutingAssembly(), "Blocks.Framework.Localization.Source"
//                        )
//                    )
//                    );

           

            Configuration.Modules.BlocksAutoMapper().Configurators.Add((IMapperConfigurationExpression configuration) =>
            {
                configuration.CreateMap<ILocalizableString, string>().ConvertUsing(ls => ls?.Localize(IocManager.Resolve<ILocalizationContext>()));
                configuration.CreateMap<LocalizableString, string>().ConvertUsing(ls => ls?.Localize(IocManager.Resolve<ILocalizationContext>()));
            });


            IocManager.Register<ILanguagesManager, LanguageManager>(Abp.Dependency.DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
         
            IocManager.Resolve<IExtensionManager>().AvailableExtensions().ForEach(e =>
                IocManager.Resolve<ILocalizationConfiguration>().Providers.Add(
                    new DbLocalizationDictionaryProvider(e.Name,IocManager))
             );
            
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<LocalizationManager>().Initialize();
        }


        public override void OnRegistered(IKernel kernel, ComponentModel model)
        {
            ;
        }

        public override void OnActivated(object instance)
        {
            if (TypeHelper.GetActualType(instance.GetType().GetTypeInfo(), out var instanceType)) return;
            var userProperty = FindFeatureProperty(instanceType);
            if (userProperty != null)
            {
                Localizer LocalizerDelegate = (text, args) =>
                    IocManager.Resolve<LocalzaionHelper>().CreateModuleLocalizableString(instanceType, text, args);
                userProperty.SetValue(instance, LocalizerDelegate);
            }
                
        }

     

        private static PropertyInfo FindFeatureProperty(TypeInfo type)
        {
            return type.GetProperty("L", typeof(Localizer));
        }
        
        
    }
}