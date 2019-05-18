namespace Blocks.Framework.Web.Web.Configuration
{
    public class BlocksWebLocalizationConfiguration : IBlocksWebLocalizationConfiguration
    {
        public string CookieName { get; set; }

        public BlocksWebLocalizationConfiguration()
        {
            CookieName = "Abp.Localization.CultureName";
        }
    }
}