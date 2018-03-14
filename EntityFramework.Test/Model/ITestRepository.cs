using Blocks.BussnessEntityModule;
using Blocks.Framework.Data;

namespace Blocks.BussnessRespositoryModule
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);
    }
}