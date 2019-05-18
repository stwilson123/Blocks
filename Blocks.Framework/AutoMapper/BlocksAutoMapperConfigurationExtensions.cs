using Abp.Configuration.Startup;

namespace Blocks.Framework.AutoMapper
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Abp.AutoMapper module.
    /// </summary>
    public static class BlocksAutoMapperConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Abp.AutoMapper module.
        /// </summary>
        public static IBlocksAutoMapperConfiguration BlocksAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.Get<IBlocksAutoMapperConfiguration>();
        }
    }
}