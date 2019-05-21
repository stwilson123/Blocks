using System.Collections.Generic;
using System.Threading.Tasks;
using Blocks.Framework.Localization.Dictionaries;

namespace Blocks.Framework.Localization.Provider
{
    public abstract class LocalizationDictionaryProviderBase : ILocalizationDictionaryProvider
    {
        public string SourceName { get; private set; }

        public ILocalizationDictionary DefaultDictionary { get; protected set; }

        public IDictionary<string, ILocalizationDictionary> Dictionaries { get; private set; }

        protected LocalizationDictionaryProviderBase(string sourceName)
        {
            Dictionaries = new Dictionary<string, ILocalizationDictionary>();
            SourceName = sourceName;
        }

        public virtual Task Initialize()
        {
            return Task.FromResult(true);
        }

        public void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            ILocalizationDictionary existingDictionary;
            if (!Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }
}