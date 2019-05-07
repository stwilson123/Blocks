using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Blocks.Framework.Localization.Setting;

namespace Blocks.Framework.Localization.Provider
{
    public class LanguageProvider: ILanguageProvider
    {
        private ISettingManager _settingManager;

        public LanguageProvider(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }


        
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
//            var languageInfos = AsyncHelper.RunSync(() => _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
//                .OrderBy(l => l.DisplayName)
//                .Select(l => l.ToLanguageInfo())
//                .ToList();

            var languageInfos = Blocks.Framework.Localization.Culture.Culture.getCulturesDics()
                .Select(l => new LanguageInfo(l.Key, l.Value)).ToList();
             SetDefaultLanguage(languageInfos);

            return languageInfos;
        }

        private void SetDefaultLanguage(List<LanguageInfo> languageInfos)
        {
            if (languageInfos.Count <= 0)
            {
                return;
            }
           

            var defaultLanguage =    _settingManager.GetSettingValueForApplicationAsync(LocalizationSettingNames.DefaultLanguage,true).Result;
            if (defaultLanguage == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }
            
            var languageInfo = languageInfos.FirstOrDefault(l => l.Name == defaultLanguage);
            if (languageInfo == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }

            languageInfo.IsDefault = true;
            
            
        }
    }
}