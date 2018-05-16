﻿namespace Blocks.Framework.Web.Web.Configuration
{
    public class AbpWebLocalizationConfiguration : IAbpWebLocalizationConfiguration
    {
        public string CookieName { get; set; }

        public AbpWebLocalizationConfiguration()
        {
            CookieName = "Abp.Localization.CultureName";
        }
    }
}