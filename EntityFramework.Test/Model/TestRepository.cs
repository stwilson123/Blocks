using System;
using System.Runtime.InteropServices.ComTypes;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Repository;
using System.Collections.Generic;
using System.Linq;
using Blocks.Framework.DBORM.Linq;

namespace EntityFramework.Test.Model
{
    public class TestRepository : DBSqlRepositoryBase<TESTENTITY>,ITestRepository  
    {
        public TestRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
        public virtual string GetValue(string value)
        {
//            var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
                           //            var sql = GetAll().Select(result => new TestEntity() {
                           //                  Id = result.Id,
                           //                TestEntity2  = new TestEntity2() {  Id      = result.TestEntity2.Id},
                           //                   TestEntity3s =  result.TestEntity3s
                           //                });
                           //            return value;
           // var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
            //var guid = Guid.NewGuid().ToString();
            //var sql = GetContextTable()
            //    .Where(t => t.TestEntity2.Id == guid)
            //    .SelectToList(result => new {
            //    Id = result.Id,
            //    TestEntity2  = new  {   result.TestEntity2.Id},
            //    TestEntity3s =  result.TestEntity3s.Select(aa => new {  aa.Id })
            //});
            return value;
        }
        
        public List<TESTENTITY> GetTestEntity2Text()
        {
            return GetContextTable().SelectToDynamicList((TESTENTITY t) => new 
            {
                TESTENTITY2 = new 
                {
                    Text =  t.TESTENTITY2.Text
                }
                
            });
        }

        public List<TESTENTITY> GetTESTENTITY3s()
        {
            return GetContextTable().SelectToDynamicList((TESTENTITY t) =>
                new 
                {
                    TESTENTITY3s = t.TESTENTITY3s
                    
                });
        }
 
    }

    public class TestRepository3 : DBSqlRepositoryBase<TESTENTITY3>, ITestRepository3
    {
        public TestRepository3(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}