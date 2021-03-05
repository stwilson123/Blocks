using Blocks.BussnessEntity2Module;
using Blocks.Framework.Data;
using Blocks.Framework.Data.Paging;
using System.Collections.Generic;

namespace Blocks.BussnessRespository2Module
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);

        string GetValueOverride(string value);

    }
}