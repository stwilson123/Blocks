using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blocks.Framework.Localization.Culture;
using Blocks.Framework.Utility.Extensions;

namespace Blocks.Framework.Localization.Provider
{

    class DbLocalizationDictionary : LocalizationDictionary
    {
        private readonly string sourceName;

        public DbLocalizationDictionary(string sourceName, CultureInfo cultureInfo) : base(cultureInfo: cultureInfo)
        {
            this.sourceName = sourceName;
            
        }

        public static ILocalizationDictionary Create(string sourceName,string cultureCode, IIocManager iocManager)
        {
         
            var dictionary = new DbLocalizationDictionary(sourceName,CultureInfo.GetCultureInfo(cultureCode));
            var dublicateNames = new List<string>();
            var providers = iocManager.ResolveAll<ILocalizationProvider>();

            foreach (var provider in providers)
            {
                foreach (var item in provider.getLocalizationDicionary(sourceName,cultureCode))
                {
                    if (string.IsNullOrEmpty(item.Key))
                    {
                        throw new LocalizationException(StringLocal.Format("The key is empty in given dictionary."));
                    }

                    if (dictionary.Contains(item.Key))
                    {
                        dublicateNames.Add(item.Key);
                    }

                    dictionary[item.Key] = item.Value.NormalizeLineEndings();
                }

            }

            if (dublicateNames.Count > 0)
            {
                throw new LocalizationException(
                    StringLocal.Format("A dictionary can not contain same key twice. There are some duplicated names: " +
                    string.Join(", ", dublicateNames)));
            }

            return dictionary;
        }

    }
}
