using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blocks.Framework.Localization.Dictionaries
{
    /// <summary>
    /// Used to get localization dictionaries (<see cref="ILocalizationDictionary"/>)
    /// for a <see cref="IDictionaryBasedLocalizationSource"/>.
    /// </summary>
    public interface ILocalizationDictionaryProvider
    {

        string SourceName { get; }
        ILocalizationDictionary DefaultDictionary { get; }

        IDictionary<string, ILocalizationDictionary> Dictionaries { get; }

        Task Initialize();
        
        void Extend(ILocalizationDictionary dictionary);
    }
}