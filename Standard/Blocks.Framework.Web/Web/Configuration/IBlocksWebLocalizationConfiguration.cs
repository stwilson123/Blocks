namespace Blocks.Framework.Web.Web.Configuration
{
    public interface IBlocksWebLocalizationConfiguration
    {
        /// <summary>
        /// Default: "Abp.Localization.CultureName".
        /// </summary>
        string CookieName { get; set; }
    }
}