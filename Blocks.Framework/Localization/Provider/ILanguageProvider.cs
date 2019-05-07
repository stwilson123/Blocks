using System.Collections.Generic;

namespace Blocks.Framework.Localization.Provider
{
    public interface ILanguageProvider
    {
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}