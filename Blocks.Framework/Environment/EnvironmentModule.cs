using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Folders;
using Blocks.Framework.Ioc;
using Castle.MicroKernel;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Abp.PlugIns;
using Blocks.Framework.Environment.Exception;
using Blocks.Framework.Environment.Extensions.Models;
using Castle.Core;

namespace Blocks.Framework.Environment
{
    public class EnvironmentModule: BlocksModule
    {
        public EnvironmentModule()
        {
           
        }
        public override void PreInitialize()
        {
            IocManager.IocContainer.Kernel.ComponentModelBuilder.AddContributor(new ProcessModelEvent());
            IocManager.AddConventionalRegistrar(new DependencyConventionalRegistrar(IocManager)); 
            ExtensionLocations extensionLocations = new ExtensionLocations();

            IocManager.Register<IExtensionFolders, ModuleFolders>((IKernel kernel, IDictionary parameters) =>
            {
                parameters.Add("paths", extensionLocations.ModuleLocations);
            });
            IocManager.Register<IExtensionFolders, ThemeFolders>((IKernel kernel, IDictionary parameters) =>
            {
                parameters.Add("paths", extensionLocations.ThemeLocations);
            });

        }

        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

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
 
            var moduleId = instanceType.Assembly.GetName().Name;
            var userProperty = FindFeatureProperty(instanceType);
            if (userProperty != null)
            {
                userProperty.SetValue(instance,new Lazy<FeatureDescriptor>(
                    () =>
                    {
                        return IocManager.Resolve<IExtensionManager>().AvailableFeatures()
                            .FirstOrDefault(f => f.Id == moduleId);
                    }));
            }
            
            var extensionProperty = FindExtensionProperty(instanceType);
            if (extensionProperty != null)
            {
                extensionProperty.SetValue(instance,IocManager.Resolve<IExtensionManager>().AvailableExtensions()
                    .FirstOrDefault(f => f.Id == moduleId));
            }
        }
        
        private static PropertyInfo FindFeatureProperty(TypeInfo type)
        {
            return type.GetProperty("Feature", typeof(Lazy<FeatureDescriptor>));
        }
        
        private static PropertyInfo FindExtensionProperty(TypeInfo type)
        {
            return type.GetProperty("Extension", typeof(ExtensionDescriptor));
        }
    }
}