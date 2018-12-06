using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization
{
    public class LocalizeNamed
    {
        public static string GetName(string inputName,string language)
        {
            return $"{inputName}-{language}";
        }
    }
}
