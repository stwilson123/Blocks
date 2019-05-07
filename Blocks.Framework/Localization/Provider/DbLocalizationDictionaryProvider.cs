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

        public DbLocalizationDictionaryProvider(IIocManager iocManager)
        {
            this.iocManager = iocManager;
        }

        public override void Initialize(string sourceName)
        {
            var cultrues = Culture.Culture.getCultures();
            Task.WhenAll(cultrues.Select(c => DbLocalizationDictionary.Create(sourceName, c, iocManager)))
                .Result.ForEach((cultureDic, index) =>
                {
                    if (cultureDic == null)
                        return;

                    Dictionaries[cultrues[index]] = cultureDic;
                    DefaultDictionary = cultureDic;
                });


        }


    }
}
