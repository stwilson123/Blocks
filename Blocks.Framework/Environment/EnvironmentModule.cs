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

namespace Blocks.Framework.Environment
{
    public class EnvironmentModule: AbpModule
    {
        public override void PreInitialize()
        {
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
            
            IocManager.AddConventionalRegistrar(new DependencyConventionalRegistrar(IocManager)); 

        }
        
        public override void PostInitialize()
        {
            //TODO Facecade validate avaliable features
            #region MyRegion
            var availablFeatures = IocManager.Resolve<IExtensionManager>().AvailableFeatures().ToList();
            var allDependencies = availablFeatures.SelectMany(f => f.Dependencies);
            var notExistsDependcies = allDependencies.Where(d => !availablFeatures.Any(f => d == f.Name));
            if (notExistsDependcies.Any())
            {
                throw new ExtensionNotFoundException($"These dependenies [{string.Join(",", notExistsDependcies)}] can't found it's feature");
            }
            var listAssemblies = IocManager.Resolve<AbpPlugInManager>()
                .PlugInSources
                .GetAllAssemblies()
                .Where(t => allDependencies.Contains(t.GetName().Name)).Distinct();

            foreach (var assembly in listAssemblies)
            {
                IocManager.RegisterAssemblyByConvention(assembly);
            }
            #endregion

        }
    }
}