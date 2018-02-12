using System;
using Blocks.Framework.ApplicationServices;

namespace Blocks.BussnessApplicationModule.TestAppService
{
    public interface IMasterDataAppService : IAppService
    {
        string GetPageList(string a);

        string Add(string id);
    }
}