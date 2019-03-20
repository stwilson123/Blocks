using Blocks.Framework.Data;

namespace EntityFramework.Test.Model
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);
    }
    
    public interface ITestRepository3 : IRepository<TESTENTITY3>
    {
    }
}