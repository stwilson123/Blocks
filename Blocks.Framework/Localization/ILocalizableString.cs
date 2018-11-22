using Abp.Localization;

namespace Blocks.Framework.Localization
{
    /// <summary>
    /// Represents a string that can be localized when needed.
    /// </summary>
    public interface ILocalizableString
    {
        
        /// <summary>
        /// Unique name of the localization source.
        /// </summary>
        string SourceName { get; }


        /// <summary>
        /// Localizes the string in current culture.
        /// </summary>
        /// <param name="context">Localization context</param>
        /// <returns>Localized string</returns>
        string Localize(ILocalizationContext context);
        //
        //        /// <summary>
        //        /// Localizes the string in given culture.
        //        /// </summary>
        //        /// <param name="context">Localization context</param>
        //        /// <param name="culture">culture</param>
        //        /// <returns>Localized string</returns>
        //        string Localize(ILocalizationContext context, CultureInfo culture);
    }
}