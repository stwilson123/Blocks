using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization
{
    public interface ILanguagesManager
    {
        LanguageInfo CurrentLanguage { get; }

        IReadOnlyList<LanguageInfo> GetLanguages();

    }
}
