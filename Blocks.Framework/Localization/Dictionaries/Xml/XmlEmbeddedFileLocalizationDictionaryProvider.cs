﻿using System.Reflection;
using System.Threading.Tasks;
using Abp;
using Blocks.Framework.Localization.Provider;

namespace Blocks.Framework.Localization.Dictionaries.Xml
{
    /// <summary>
    /// Provides localization dictionaries from XML files embedded into an <see cref="Assembly"/>.
    /// </summary>
    public class XmlEmbeddedFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;
        
        /// <summary>
        /// Creates a new <see cref="XmlEmbeddedFileLocalizationDictionaryProvider"/> object.
        /// </summary>
        /// <param name="assembly">Assembly that contains embedded xml files</param>
        /// <param name="rootNamespace">Namespace of the embedded xml dictionary files</param>
        public XmlEmbeddedFileLocalizationDictionaryProvider(string sourceName,Assembly assembly, string rootNamespace) : base(sourceName)
        {
            _assembly = assembly;
            _rootNamespace = rootNamespace;
        }

        public override Task Initialize()
        {
            var resourceNames = _assembly.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                if (resourceName.StartsWith(_rootNamespace))
                {
                    using (var stream = _assembly.GetManifestResourceStream(resourceName))
                    {
                        var xmlString = Abp.Localization.Dictionaries.Utf8Helper.ReadStringFromStream(stream);

                        var dictionary = CreateXmlLocalizationDictionary(xmlString);
                        if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                        {
                            throw new AbpInitializationException(SourceName + " source contains more than one dictionary for the culture: " + dictionary.CultureInfo.Name);
                        }

                        Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                        if (resourceName.EndsWith(SourceName + ".xml"))
                        {
                            if (DefaultDictionary != null)
                            {
                                throw new AbpInitializationException("Only one default localization dictionary can be for source: " + SourceName);
                            }

                            DefaultDictionary = dictionary;
                        }
                    }
                }
            }

            return Task.FromResult(true);
        }

        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string xmlString)
        {
            return XmlLocalizationDictionary.BuildFomXmlString(xmlString);
        }
    }
}