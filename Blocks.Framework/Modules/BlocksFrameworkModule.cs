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
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Ioc;

namespace Blocks.Framework.Modules
{
    [DependsOn(typeof(CacheModule))]
    [DependsOn(typeof(EnvironmentModule))]
    public class BlocksFrameworkModule: AbpModule
    {
         
       
        public override void PreInitialize()
        {
            //TODO it should be move to more early first Config, but it is belong to Adp
            IocManager.IocContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(IocManager.IocContainer.Kernel));
            //            IocManager.Register<WindsorInstanceProvier,WindsorInstanceProvier>();
            //
            //            var iocProvider = IocManager.Resolve<WindsorInstanceProvier>(IocManager);
            //            iocProvider.RegisterKernelCompoentEvnet();


            Configuration.Localization.Sources.Add(
               new DictionaryBasedLocalizationSource(
                   BlocksFrameworkLocalizationSource.LocalizationSourceName,
                   new XmlEmbeddedFileLocalizationDictionaryProvider(
                       Assembly.GetExecutingAssembly(), "Blocks.Framework.Localization.Source"
                   )
               )
           );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

           
        }
    }
}