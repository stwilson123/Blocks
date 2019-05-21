using System.Collections.Generic;
using Blocks.Framework.Localization.Source;
using LocalizationSourceExtensionInfo = Blocks.Framework.Localization.Source.LocalizationSourceExtensionInfo;

namespace Blocks.Framework.Localization.Configuartions
{
    /// <summary>
    /// A specialized list to store <see cref="ILocalizationSource"/> object.
    /// </summary>
    internal class LocalizationSourceList : List<ILocalizationSource>, ILocalizationSourceList
    {
        public IList<LocalizationSourceExtensionInfo> Extensions { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationSourceList()
        {
            Extensions = new List<LocalizationSourceExtensionInfo>();
        }
    }
}