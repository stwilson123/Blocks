using System;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public interface ITestAppService : IAppService
    {
        string GetValue(string a);

        Guid Add(string id);
    }
}