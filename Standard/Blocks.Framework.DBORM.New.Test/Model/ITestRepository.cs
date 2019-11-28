using System.Collections.Generic;
using Blocks.Framework.Data;
using Blocks.Framework.Data.Paging;

namespace EntityFramework.Test.Model
{
    public interface ITestRepository : IRepository<TESTENTITY>
    {
        string GetValue(string value);

        List<TESTENTITY> GetTestEntity2Text();
        
        PageList<TESTENTITY> GetTestEntityDistinct();


        List<TESTENTITY> GetTESTENTITY3s();
        object FromSql();

        object GetLongIdetifier();

        object SkipAndTake(int page, int pageSize);

        object SkipAndTakeFromSql(int page, int pageSize);
        
        List<TESTENTITY> GetMultLeftJoin();
    }
    
    
    public interface ITest2Repository : IRepository<TESTENTITY2>
    {
         
    }
    public interface ITestRepository3 : IRepository<TESTENTITY3>
    {
    }
}