using Blocks.BussnessEntityModule;
using Blocks.Framework.Data;

namespace Blocks.BussnessRespositoryModule
{
    public interface ITestRepository : IRepository<TestEntity>
    {
        string GetValue(string value);

    }
}