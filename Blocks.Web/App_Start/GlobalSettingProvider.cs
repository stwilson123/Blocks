using System.Collections.Generic;
using Abp.Configuration;
using Blocks.Framework.Configurations.Provider;
using Blocks.Framework.Web.Configuartions;

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
                    DatabaseType.Sqlserver.ToString()
                ),
              
            };
        }
    }
}