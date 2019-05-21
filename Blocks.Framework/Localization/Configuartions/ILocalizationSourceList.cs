using System.Collections.Generic;
using Blocks.Framework.Localization.Source;
using LocalizationSourceExtensionInfo = Blocks.Framework.Localization.Source.LocalizationSourceExtensionInfo;

namespace Blocks.Framework.Localization.Configuartions
{
    /// <summary>
    /// Defines a specialized list to store <see cref="ILocalizationSource"/> object.
    /// </summary>
    public interface ILocalizationSourceList : IList<ILocalizationSource>
    {
        /// <summary>
        /// Extensions for dictionay based localization sources.
        /// </summary>
        IList<LocalizationSourceExtensionInfo> Extensions { get; }
    }
}