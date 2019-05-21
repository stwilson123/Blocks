using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using Abp.Dependency;
using Blocks.Framework.Localization.Configuartions;
using Blocks.Framework.Localization.Dictionaries;
using Blocks.Framework.Utility.Extensions;
using Castle.Core.Logging;


namespace Blocks.Framework.Localization.Source
{
    /// <summary>
    /// Null object pattern for <see cref="Blocks.Localization.Sources.ILocalizationSource"/>.
    /// </summary>
    internal class DefaultLocalizationSource : ILocalizationSource
    {
        private readonly ILocalizationConfiguration _localizationConfiguration;


        public string Name { get;  }
        
        public ILogger Logger { get; set; }
        
        public ILocalizationDictionaryProvider DictionaryProvider { get; }


        private readonly IReadOnlyList<ILocalizableString> _localizableArray = new List<ILocalizableString>();

        public DefaultLocalizationSource(string sourceName, ILocalizationDictionaryProvider dictionaryProvider,ILocalizationConfiguration localizationConfiguration)
        {
            _localizationConfiguration = localizationConfiguration;
            Name = sourceName;
            DictionaryProvider = dictionaryProvider;
        }

        public void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            
        }

        public string GetString(string name)
        {
            return GetString(name, CultureInfo.CurrentUICulture);
        }

        public string GetString(string name, CultureInfo culture)
        {
            var value = GetStringOrNull(name, culture);

            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name, culture);
            }

            return value;
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return GetStringOrNull(name, CultureInfo.CurrentUICulture, tryDefaults);
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            var cultureName = culture.Name;
            var dictionaries = DictionaryProvider.Dictionaries;

            //Try to get from original dictionary (with country code)
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(cultureName, out originalDictionary))
            {
                var strOriginal = originalDictionary.GetOrNull(name);
                if (strOriginal != null)
                {
                    return strOriginal.Value;
                }
            }

            if (!tryDefaults)
            {
                return null;
            }

            //Try to get from same language dictionary (without country code)
            if (cultureName.Contains("-")) //Example: "tr-TR" (length=5)
            {
                ILocalizationDictionary langDictionary;
                if (dictionaries.TryGetValue(GetBaseCultureName(cultureName), out langDictionary))
                {
                    var strLang = langDictionary.GetOrNull(name);
                    if (strLang != null)
                    {
                        return strLang.Value;
                    }
                }
            }

            //Try to get from default language
            var defaultDictionary = DictionaryProvider.DefaultDictionary;
            if (defaultDictionary == null)
            {
                return null;
            }

            var strDefault = defaultDictionary.GetOrNull(name);
            if (strDefault == null)
            {
                return null;
            }

            return strDefault.Value;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture, includeDefaults);
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            var dictionaries = DictionaryProvider.Dictionaries;

            //Create a temp dictionary to build
            var allStrings = new Dictionary<string, LocalizedString>();

            if (includeDefaults)
            {
                //Fill all strings from default dictionary
                var defaultDictionary = DictionaryProvider.DefaultDictionary;
                if (defaultDictionary != null)
                {
                    foreach (var defaultDictString in defaultDictionary.GetAllStrings())
                    {
                        allStrings[defaultDictString.Name] = defaultDictString;
                    }
                }

                //Overwrite all strings from the language based on country culture
                if (culture.Name.Contains("-"))
                {
                    ILocalizationDictionary langDictionary;
                    if (dictionaries.TryGetValue(GetBaseCultureName(culture.Name), out langDictionary))
                    {
                        foreach (var langString in langDictionary.GetAllStrings())
                        {
                            allStrings[langString.Name] = langString;
                        }
                    }
                }
            }

            //Overwrite all strings from the original dictionary
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(culture.Name, out originalDictionary))
            {
                foreach (var originalLangString in originalDictionary.GetAllStrings())
                {
                    allStrings[originalLangString.Name] = originalLangString;
                }
            }

            return allStrings.Values.ToImmutableList();
        }
        
        protected virtual string ReturnGivenNameOrThrowException(string name, CultureInfo culture)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(
                _localizationConfiguration,
                Name,
                name,
                culture,
                Logger
            );
        }
        
        private static string GetBaseCultureName(string cultureName)
        {
            return cultureName.Contains("-")
                ? cultureName.Left(cultureName.IndexOf("-", StringComparison.Ordinal))
                : cultureName;
        }
    }
}