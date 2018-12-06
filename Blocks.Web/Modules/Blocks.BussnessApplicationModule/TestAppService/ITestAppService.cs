using System;
using System.Collections.Generic;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public interface ITestAppService : IAppService
    {
        string GetValue(string a);

        string Add(string id);

        List<string> ProxFunction(ProxModel input);
    }
}