namespace Blocks.Framework.Web.Web.Configuration
{
    public interface IAbpWebLocalizationConfiguration
    {
        /// <summary>
        /// Default: "Abp.Localization.CultureName".
        /// </summary>
        string CookieName { get; set; }
    }
}