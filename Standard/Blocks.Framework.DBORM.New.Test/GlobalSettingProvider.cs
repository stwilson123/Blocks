using System.Collections.Generic;
using System.Configuration;
using Abp.Configuration;
using Blocks.Framework.Configurations.Provider;

namespace EntityFramework.Test
{
    public class GlobalSettingProvider : BlocksSettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    "DatabaseType",
                    "Oracle"
                ),
         
                new SettingDefinition(
                    Blocks.Framework.DBORM.Configurations.ConfigKey.Schema,
                    ConfigurationManager.AppSettings.Get(Blocks.Framework.DBORM.Configurations.ConfigKey.Schema)
                )
            };
        }
    }
}