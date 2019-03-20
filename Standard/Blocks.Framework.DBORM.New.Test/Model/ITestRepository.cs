using System.Collections.Generic;
using Blocks.Framework.Data;

namespace EntityFramework.Test.Model
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);

        List<TESTENTITY> GetTestEntity2Text();

        List<TESTENTITY> GetTESTENTITY3s();
        object FromSql();

        object GetLongIdetifier();

        object SkipAndTake(int page, int pageSize);

        object SkipAndTakeFromSql(int page, int pageSize);
    }
    
    
    public interface ITest2Repository : IRepository<TESTENTITY2>
    {
         
    }
    public interface ITestRepository3 : IRepository<TESTENTITY3>
    {
    }
}