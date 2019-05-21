using System.Globalization;
using Abp;
using Blocks.Framework.Localization.Configuartions;
using Blocks.Framework.Logging;
using Blocks.Framework.Utility.Extensions;
using Castle.Core.Logging;

namespace Blocks.Framework.Localization.Source
{
    public static class LocalizationSourceHelper
    {
        public static string ReturnGivenNameOrThrowException(
            ILocalizationConfiguration configuration,
            string sourceName, 
            string name, 
            CultureInfo culture,
            ILogger logger = null)
        {
            var exceptionMessage = $"Can not find '{name}' in localization source '{sourceName}'!";

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new AbpException(exceptionMessage);
            }

            if (configuration.LogWarnMessageIfNotFound)
            {
                (logger ?? LogHelper.logger).Warn(exceptionMessage);
            }

            var notFoundText = configuration.HumanizeTextIfNotFound
                ? name.ToSentenceCase(culture)
                : name;

            return configuration.WrapGivenTextIfNotFound
                ? $"[{notFoundText}]"
                : notFoundText;
        }
    }
}