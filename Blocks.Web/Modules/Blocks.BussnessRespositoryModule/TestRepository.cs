using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.BussnessEntityModule;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Linq;
using Blocks.Framework.DBORM.Repository;
using Blocks.Framework.Utility.SafeConvert;

namespace Blocks.BussnessRespositoryModule
{
    public class TestRepository : DBSqlRepositoryBase<TESTENTITY>, ITestRepository
    {
        public TestRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        public string GetValue(string value)
        {
            //            var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
            //            var sql = GetAll().Select(result => new TestEntity() {
            //                  Id = result.Id,
            //                TestEntity2  = new TestEntity2() {  Id      = result.TestEntity2.Id},
            //                   TestEntity3s =  result.TestEntity3s
            //                });
            //            return value;
            // var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
            //var sql = GetContextTable()

            //    .SelectToList(result => new {
            //        Id = result.Id,
            //        TestEntity2 = new { result.TESTENTITY2.Id },
            //    TestEntity3s = result.TESTENTITY3s
            //});
            return value;
            
        }

        public virtual string GetValueOverride(string value)
        {
            throw new NotImplementedException();
        }

        public PageList<PageResult> GetPageList(SearchModel search)
        {
            return GetContextTable()
                .Paging((TESTENTITY testEntity) => new PageResult
                {
                    Id = testEntity.Id,
                    comboboxText = testEntity.TESTENTITY2.Text,
                    city = testEntity.STRING,
                    isActive = testEntity.ISACTIVE,
                    comment = testEntity.COMMENT,
                    registerTime =   testEntity.REGISTERTIME
                }, search.page);
//            return GetContextTable().InnerJoin((TESTENTITY testEntity) => testEntity.TESTENTITY2ID,
//                    (TESTENTITY2 testEntity2) => testEntity2.Id)
//                .Paging((TESTENTITY testEntity,TESTENTITY2 testEntity2) => new PageResult
//                {
//                    Id = testEntity.Id,
//                    comboboxText = testEntity2.Text,
//                    city = testEntity.STRING,
//                    isActive = testEntity.ISACTIVE,
//                    comment = testEntity.COMMENT,
//                     registerTime =   testEntity.REGISTERTIME
//                }, search.page);
        }
    }
}