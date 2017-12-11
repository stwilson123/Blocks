using System.Collections;
using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Blocks.Framework.Environment.Configuration;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Folders;
using Blocks.Framework.Ioc;
using Castle.MicroKernel;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

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
            
            IocManager.AddConventionalRegistrar(new DependencyConventionalRegistrar(IocManager.Resolve<IExtensionManager>())); 

        }

        public override void Initialize()
        {
            
            
          
        }
    }
}