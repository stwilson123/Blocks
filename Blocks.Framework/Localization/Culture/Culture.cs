using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization.Culture
{
    public class Culture
    {
        public static List<string> cultures;
        static Culture()
        {
            cultures = new List<string>() {
                en,
                zhCN
            }; 
        }
        const string en = "en";
        const string zhCN = "zh-CN";



        public static string[] getCultures() => cultures.ToArray();
    }
}
