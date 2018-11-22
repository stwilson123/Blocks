using Abp.Dependency;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Dictionaries.Xml;
using System;
using System.Collections.Generic;
using System.Text;

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
            foreach (var cultrue in cultrues)
            {
                var cultureDic = DbLocalizationDictionary.Create(sourceName, cultrue, iocManager);
                if (cultureDic == null || cultrue.Length == 0)
                    continue;
                Dictionaries[cultrue] = cultureDic;
                DefaultDictionary = cultureDic;
            }
             
        }

       
    }
}
