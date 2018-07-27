using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Folders;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc;
using Castle.Core;
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
           
            

        }

        public override void Initialize()
        {
            
          

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
 
            var moduleId = instance.GetType().Assembly.GetName().Name;
            var userProperty = FindFeatureProperty(instanceType);
            if (userProperty != null)
            {
                Localizer LocalizerDelegate = (text, args) =>
                {
                    return new LocalizableString(IocManager.Resolve<IExtensionManager>().AvailableExtensions()
                        .FirstOrDefault(f => f.Id == moduleId).Name,text);
                   
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