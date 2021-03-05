using System;
using System.Collections.Generic;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplication2Module.TestAppService2
{
    public interface ITestAppService : IAppService
    {
        string GetValue(string a);

        string Add(string id);

    }
}