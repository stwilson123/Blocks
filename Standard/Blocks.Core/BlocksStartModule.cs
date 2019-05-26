using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Blocks.Framework.Ioc;
using Blocks.Framework.Modules;
using System.Reflection;
using Abp;
using Blocks.Framework.Web.Mvc.Controllers;
using BlocksModule = Blocks.Framework.Ioc.BlocksModule;

namespace Blocks.Core
{
    public class BlocksStartModule : BlocksModule
    {

        public override void PreInitialize()
        {
            
            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Blocks.MvcController",
                    type => typeof(BlocksWebMvcController).IsAssignableFrom(type)
                )
            );
            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "Blocks",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Blocks.Core.Localization.Source"
                        )
                    )
                );
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(this.GetType().Assembly);
        }
    }
}