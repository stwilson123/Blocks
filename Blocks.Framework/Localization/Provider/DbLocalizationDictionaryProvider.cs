using Abp.Dependency;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Dictionaries.Xml;
using Blocks.Framework.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Localization.Provider
{

    public class DbLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly IIocManager iocManager;

        public DbLocalizationDictionaryProvider(string sourceName,IIocManager iocManager) :base(sourceName)
        {
            this.iocManager = iocManager;
        }

        public override async Task Initialize()
        {
            var cultrues = Culture.Culture.getCultures();
            foreach (var cultrueTask in cultrues.Select(c => new { localizationTask = DbLocalizationDictionary.Create(SourceName, c, iocManager),c}))
            {
                var localization = await cultrueTask.localizationTask;
                if(!Dictionaries.ContainsKey(cultrueTask.c))
                    Dictionaries.Add(cultrueTask.c, localization);
                Dictionaries[cultrueTask.c] = localization;
                DefaultDictionary = localization;
            }
        }


    }
}
