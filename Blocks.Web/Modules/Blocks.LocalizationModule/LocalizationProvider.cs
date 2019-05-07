using Blocks.Framework.Localization.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Blocks.LocalizationModule
{
    public class LocalizationProvider : ILocalizationProvider
    {
        public Task<IDictionary<string, string>> getLocalizationDicionary(string moduleName, string culture)
        {
            var dicResult = default(IDictionary<string, string>);
            if (culture == "en")
            {
                dicResult = new Dictionary<string, string>() { { "MasterData", "MasterData" },
                    {"Tests","Tests" },
                { "TestException", "TestException" },
                { "Name", "TestException" },
                { "city", "City" },
                { "registerTime","RegisterTime"},
                   { "activation","Activation"},
                    {"comment" ,"Comment"},
                    { "add","Add"},
                    { "query","Query"},
                };
            }
            else if (culture == "zh-CN")
            {
                dicResult = new Dictionary<string, string>() { { "MasterData", "主数据" },
                { "TestException", "测试异常" },
                 {"Tests","测试" },
                { "Name", "名称" },
                { "city", "城市" },
                { "registerTime","注册时间"},
                    { "activation","激活"},
                    { "comment","备注"},
                    { "add","新增"},
                    { "query","查询"},


                };
            }


            return Task.FromResult(dicResult);


        }
    }
}