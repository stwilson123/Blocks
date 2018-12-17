using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization
{
    public interface ILanguageManager
    {
        LanguageInfo CurrentLanguage { get; }

        IReadOnlyList<LanguageInfo> GetLanguages();

    }
}
