using System.Collections.Generic;
using Abp.Configuration;

namespace Blocks.Framework.Configurations.Provider
{
    public abstract class BlocksSettingProvider : Abp.Configuration.SettingProvider
    {
        public abstract override IEnumerable<SettingDefinition> GetSettingDefinitions(
            SettingDefinitionProviderContext context);

    }
}