using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using AutoMapper;
using Blocks.Framework.Collections;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Folders;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc;
using Blocks.Framework.Localization.Provider;
using Blocks.Framework.Types;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;

namespace Blocks.Framework.Localization
{
    public class LocalizationModule: BlocksModule
    {
        public LocalizationModule()
        {

        }
        public override void PreInitialize()
        {
            
         
            
            Configuration.Localization.Sources.Add(
               new DictionaryBasedLocalizationSource(
                   BlocksFrameworkLocalizationSource.LocalizationSourceName,
                   new XmlEmbeddedFileLocalizationDictionaryProvider(
                       Assembly.GetExecutingAssembly(), "Blocks.Framework.Localization.Source"
                   )
               )
           );

           

            Configuration.Modules.AbpAutoMapper().Configurators.Add((IMapperConfigurationExpression configuration) =>
            {
                var localizationContext = IocManager.Resolve<ILocalizationContext>();

                configuration.CreateMap<ILocalizableString, string>().ConvertUsing(ls => ls?.Localize(localizationContext));
                configuration.CreateMap<LocalizableString, string>().ConvertUsing(ls => ls?.Localize(localizationContext));
            });


            IocManager.Register<ILanguagesManager, LanguageManager>(Abp.Dependency.DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
         
            IocManager.Resolve<IExtensionManager>().AvailableExtensions().ForEach(e =>
               Configuration.Localization.Sources.Add(
                  new DictionaryBasedLocalizationSource(e.Name, new DbLocalizationDictionaryProvider(IocManager))
                )
             );

        }
        
        public override void PostInitialize()
        {
            

            //TODO Facecade validate avaliable features
            #region MyRegion
            //var availablFeatures = IocManager.Resolve<IExtensionManager>().AvailableFeatures().ToList();
            //var allDependencies = availablFeatures.SelectMany(f => f.Dependencies);
            //var notExistsDependcies = allDependencies.Where(d => !availablFeatures.Any(f => d == f.Name));
            //if (notExistsDependcies.Any())
            //{
            //    throw new ExtensionNotFoundException($"These dependenies [{string.Join(",", notExistsDependcies)}] can't found it's feature");
            //}
            //var listAssemblies = IocManager.Resolve<AbpPlugInManager>()
            //    .PlugInSources
            //    .GetAllAssemblies()
            //    .Where(t => allDependencies.Contains(t.GetName().Name)).Distinct();

            //            foreach (var assembly in listAssemblies)
            //            {
            //                IocManager.RegisterAssemblyByConvention(assembly);
            //            }
            #endregion

        }


        public override void OnRegistered(IKernel kernel, ComponentModel model)
        {
            ;
        }

        public override void OnActivated(object instance)
        {


            TypeInfo instanceType = instance.GetType().GetTypeInfo();
            if (instance is IProxyTargetAccessor)
            {
                instanceType = instanceType.BaseType.GetTypeInfo();
            }
            if (instanceType == null)
                return;
            var moduleId = instanceType.Assembly.GetName().Name;
            var userProperty = FindFeatureProperty(instanceType);
            if (userProperty != null)
            {
                Localizer LocalizerDelegate = (text, args) =>
                {
                    var AvailableExtensions = IocManager.Resolve<IExtensionManager>().AvailableFeatures();
                    var ModuleName = AvailableExtensions.FirstOrDefault(f => f.Id == moduleId)?.Name;
                    if (string.IsNullOrEmpty(ModuleName))
                        ModuleName = AvailableExtensions.FirstOrDefault(t => t.SubAssembly.Contains(moduleId))?.Name;
                    
                    Check.NotNull(ModuleName, $"Can't find [{moduleId}] in AvailableFeatures");
                    return new LocalizableString(ModuleName,text, args);
                   
                };
                userProperty.SetValue(instance, LocalizerDelegate);
            }
                
        }
        
        private static PropertyInfo FindFeatureProperty(TypeInfo type)
        {
            return type.GetProperty("L", typeof(Localizer));
        }
        
        
    }
}