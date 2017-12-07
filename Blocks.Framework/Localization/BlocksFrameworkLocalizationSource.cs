using Abp;
using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Sources;

namespace Blocks.Framework.Localization
{
    public class BlocksFrameworkLocalizationSource : ITransientDependency
    {
        private readonly ILocalizationManager _localizationManager;
        public static readonly string LocalizationSourceName = "BlocksFramework";

        public BlocksFrameworkLocalizationSource(ILocalizationManager LocalizationManager)
        {
            _localizationManager = LocalizationManager;
        }
        
        
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// </summary>
        public ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new AbpException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = _localizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
    }
}