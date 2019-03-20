using System.Collections.Generic;
using Blocks.Framework.Data;

namespace EntityFramework.Test.Model
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);
        List<TESTENTITY> GetTestEntity2Text();

        List<TESTENTITY> GetTESTENTITY3s();
    }
    
    public interface ITestRepository3 : IRepository<TESTENTITY3>
    {
    }
}