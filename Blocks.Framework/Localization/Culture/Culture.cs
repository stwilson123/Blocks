using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization.Culture
{
    public class Culture
    {
        private static List<string> cultures;
        private static Dictionary<string,string> culturesDics;

        static Culture()
        {
            cultures = new List<string>() {
                en,
                zhCN
            }; 
            culturesDics = new Dictionary<string, string>()
            {
                
                { "en", "English"}, 
                { "zh-CN", "简体中文"}

            };
        }
        const string en = "en";
        const string zhCN = "zh-CN";


        public static string[] getCultures() => cultures.ToArray();

        public static Dictionary<string, string> getCulturesDics() => culturesDics;
    }
}
