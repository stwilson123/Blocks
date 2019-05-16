using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Blocks.Framework.Ioc;
using Blocks.Framework.Modules;
using System.Reflection;
using BlocksModule = Blocks.Framework.Ioc.BlocksModule;

namespace Blocks.Core
{
    public class BlocksStartModule : BlocksModule
    {

        public override void PreInitialize()
        {
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