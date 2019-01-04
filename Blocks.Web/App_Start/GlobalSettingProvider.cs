using System.Collections.Generic;
using Abp.Configuration;
using Blocks.Framework.Configurations;
using Blocks.Framework.Configurations.Provider;
using Blocks.Framework.Web.Configuartions;
using System.Configuration;

namespace Blocks.Web
{
    public class GlobalSettingProvider : BlocksSettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    typeof(DatabaseType).Name,
                    DatabaseType.Oracle.ToString()
                ),
                new SettingDefinition(
                    typeof(DebugState).Name,
                     DebugState.Release.ToString()
                ),
                new SettingDefinition(
                    Framework.DBORM.Configurations.ConfigKey.Schema,
                    ConfigurationManager.AppSettings.Get(Framework.DBORM.Configurations.ConfigKey.Schema)
                )
            };
        }
    }
}