using Blocks.Framework.Localization.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blocks.LocalizationModule
{
    public class LocalizationProvider : ILocalizationProvider
    {
        public IDictionary<string, string> getLocalizationDicionary(string moduleName, string culture)
        {

            if (culture == "en")
            {
                return new Dictionary<string, string>() { { "MasterData", "MasterData" },
                { "TestException", "TestException" },
                { "Name", "TestException" },
                };
            }
            else if (culture == "zh-CN")
            {
                return new Dictionary<string, string>() { { "MasterData", "主数据" },
                { "TestException", "测试异常" },
                { "Name", "名称" },
                };
            }


            return new Dictionary<string, string>();


        }
    }
}