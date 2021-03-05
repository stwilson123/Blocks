using System;
using System.Collections.Generic;
using Blocks.Framework.Localization.Dictionaries;

namespace Blocks.Framework.Localization.Configuartions
{
    /// <summary>
    /// Used for localization configurations.
    /// </summary>
    internal class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <inheritdoc/>
        public IList<LanguageInfo> Languages { get; }

        /// <inheritdoc/>
         ILocalizationSourceList sources { get; set; }

        public ILocalizationSourceList GetSources()
        {
            throw new System.NotImplementedException();
        }

        public IList<ILocalizationDictionaryProvider> Providers { get;  }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        /// <inheritdoc/>
        public bool ReturnGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool WrapGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool HumanizeTextIfNotFound { get; set; }

        public bool LogWarnMessageIfNotFound { get; set; }


        public LocalizationConfiguration()
        {
            Languages = new List<LanguageInfo>();
            sources = new LocalizationSourceList();
            Providers = new List<ILocalizationDictionaryProvider>();
            IsEnabled = true;
            ReturnGivenTextIfNotFound = true;
            WrapGivenTextIfNotFound = true;
            HumanizeTextIfNotFound = false;
            LogWarnMessageIfNotFound = true;
        }
    }
}
