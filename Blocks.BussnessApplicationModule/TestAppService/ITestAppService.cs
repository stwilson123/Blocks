﻿using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public interface ITestAppService : IAppService
    {
        string GetValue(string a);
    }
}