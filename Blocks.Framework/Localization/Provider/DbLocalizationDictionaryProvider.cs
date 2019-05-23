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

        public override Task Initialize()
        {
            var cultrues = Culture.Culture.getCultures();
            return Task.WhenAll(cultrues.Select(c => DbLocalizationDictionary.Create(SourceName, c, iocManager)))
                .ContinueWith(task =>
                {
                    task.Result.ForEach((cultureDic, index) =>
                    {
                        if (cultureDic == null)
                            return;
                        Dictionaries.Add(cultrues[index], cultureDic);
                        DefaultDictionary = cultureDic;
                    });
                });
        }


    }
}
