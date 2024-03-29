﻿using Blocks.Framework.Localization.Convert;
using Blocks.Framework.Utility.Extensions;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Blocks.Framework.Localization
{
    /// <summary>
    /// Represents a string that can be localized.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(LocalizedConvert))]
    public class LocalizableString : ILocalizableString
    {
        /// <summary>
        /// Unique name of the localization source.
        /// </summary>
        public virtual string SourceName { get; private set; }

        /// <summary>
        /// Unique Name of the string to be localized.
        /// </summary>
        public virtual string Name { get; private set; }


        /// <summary>
        /// Unique Name of the string to be localized.
        /// </summary>
        object[] args { get;  set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        private LocalizableString()
        {
            
        }

        /// <param name="name">Unique Name of the string to be localized</param>
        /// <param name="sourceName">Unique name of the localization source</param>
        public LocalizableString(string sourceName, string name, params object[] args)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (sourceName == null)
            {
                throw new ArgumentNullException("sourceName");
            }

            Name = name;
            SourceName = sourceName;
            this.args = args;
        }

        public string Localize(ILocalizationContext context)
        {
            //return context.LocalizationManager.GetString(SourceName, Name);
            return context.LocalizationManager.GetString(SourceName,Name).SafeFormat(this.args);

        }
        //
        //        public string Localize(ILocalizationContext context, CultureInfo culture)
        //        {
        //            return context.LocalizationManager.GetString(SourceName, Name, culture);
        //        }

        public override string ToString()
        {
            return Name;

//            return string.Format("[LocalizableString: {0}, {1}]", Name, SourceName);
        }
    }
}