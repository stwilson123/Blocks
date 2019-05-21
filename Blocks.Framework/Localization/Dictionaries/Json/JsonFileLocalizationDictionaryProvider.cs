using System.IO;
using System.Threading.Tasks;
using Abp;
using Blocks.Framework.Localization.Provider;


namespace Blocks.Framework.Localization.Dictionaries.Json
{
    /// <summary>
    ///     Provides localization dictionaries from json files in a directory.
    /// </summary>
    public class JsonFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        ///     Creates a new <see cref="JsonFileLocalizationDictionaryProvider" />.
        /// </summary>
        /// <param name="directoryPath">Path of the dictionary that contains all related XML files</param>
        public JsonFileLocalizationDictionaryProvider(string sourceName, string directoryPath) : base(sourceName)
        {
            _directoryPath = directoryPath;
        }
        
        public override  Task Initialize()
        {
            var fileNames = Directory.GetFiles(_directoryPath, "*.json", SearchOption.TopDirectoryOnly);

            
            foreach (var fileName in fileNames)
            {
                var dictionary = CreateJsonLocalizationDictionary(fileName);
                if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                {
                    throw new AbpInitializationException(SourceName + " source contains more than one dictionary for the culture: " + dictionary.CultureInfo.Name);
                }

                Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                if (fileName.EndsWith(SourceName + ".json"))
                {
                    if (DefaultDictionary != null)
                    {
                        throw new AbpInitializationException("Only one default localization dictionary can be for source: " + SourceName);
                    }

                    DefaultDictionary = dictionary;
                }
            }

            return Task.FromResult(true);
        }

        protected virtual JsonLocalizationDictionary CreateJsonLocalizationDictionary(string fileName)
        {
            return JsonLocalizationDictionary.BuildFromFile(fileName);
        }
    }
}