using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blocks.LayoutModule.ViewModels
{
    public class LanguageSelectionViewModel
    {

        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }
    }


    public class LanguageInfo
    {
        public LanguageInfo(Abp.Localization.LanguageInfo languageInfo)
        {
            this.Name = languageInfo.Name;
            this.DisplayName = languageInfo.DisplayName;
            this.Icon = languageInfo.Icon;
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
    }
}