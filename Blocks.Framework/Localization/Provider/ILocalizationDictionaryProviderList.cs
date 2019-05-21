//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Policy;
//using Blocks.Framework.Localization.Dictionaries;
//
//namespace Blocks.Framework.Localization.Provider
//{
//    public class LocalizationDictionaryProviderList : List<ILocalizationDictionaryProvider>,
//        ICollection<ILocalizationDictionaryProvider>
//    {
//        static object locker = new object();
//
//        void ICollection<ILocalizationDictionaryProvider>.Add(ILocalizationDictionaryProvider item)
//        {
//            lock (locker)
//            {
//                if (this.Any(t => t.SourceName))
//            }
//        }
//    }
//}