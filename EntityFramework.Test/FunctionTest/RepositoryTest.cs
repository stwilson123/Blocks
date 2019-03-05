using System.Threading;
using EntityFramework.Test.Model;
using Xunit;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFramework.Test.FunctionTest
{
     
    public class RepositoryTest : BlocksTestBase
    {
        [Fact]
        public void BatchAddPerformance()
        {
            var rep = Resolve<TestRepository>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //var trans = rep.Context.Database.BeginTransaction();执行时间
            var listTestEntity = new List<TESTENTITY>();
            for (int i = 0; i < 10000; i++)
            {
                listTestEntity.Add(new TESTENTITY() { COLNUMINT = i, UPDATER = "1", CREATER = "1", TESTENTITY2ID = "11" });
            }
            rep.Insert(listTestEntity);
            stopwatch.Stop();
            Assert.True(false, "Total Milliseconds:" + stopwatch.ElapsedMilliseconds);
            //trans.Commit();
        }
        [Fact]
        public void syncQueryMethod()
        { 
            var rep =  Resolve<TestRepository>();
            var firstData = rep.GetAllList().FirstOrDefault();
            if(firstData != null )
            {
                Assert.True(firstData.Id == rep.Get(firstData.Id).Id);
                Assert.True(firstData.Id == rep.FirstOrDefault(firstData.Id).Id);
                Assert.True(firstData.Id == rep.FirstOrDefault(t => t.Id == firstData.Id).Id);

                Assert.True(firstData.Id == rep.Single(t => t.Id == firstData.Id).Id);

                Assert.True(1 == rep.Count(t => t.Id == firstData.Id));

                Assert.True(1 == rep.LongCount(t => t.Id == firstData.Id));

                

            }



        }

        [Fact]
        public async void asyncQueryMethod()
        {
            var rep = Resolve<TestRepository>();
            var firstData =  (await rep.GetAllListAsync()).FirstOrDefault();
            if (firstData != null)
            {
                Assert.True(firstData.Id == (await rep.GetAsync(firstData.Id)).Id);
                Assert.True(firstData.Id == (await rep.FirstOrDefaultAsync(firstData.Id)).Id);
                Assert.True(firstData.Id == (await rep.FirstOrDefaultAsync(t => t.Id == firstData.Id)).Id);

                Assert.True(firstData.Id == (await rep.SingleAsync(t => t.Id == firstData.Id)).Id);
                Assert.True(1 == (await rep.CountAsync(t => t.Id == firstData.Id)));
                Assert.True(1 == (await rep.LongCountAsync(t => t.Id == firstData.Id)));
            }
        }
        [Fact]
        public void UpdateByModel()
        {
            var rep = Resolve<TestRepository>();
            var id= rep.InsertAndGetId(new TESTENTITY(){ Id = Guid.NewGuid().ToString(), TESTENTITY2ID = Guid.NewGuid().ToString()});
            var testEntity = rep.FirstOrDefault(t => t.Id != null);
            var setGuid = Guid.NewGuid().ToString();
            testEntity.TESTENTITY2ID = setGuid;
            rep.Update(testEntity);
           
        }

        [Fact]
        public void UpdateByExpression()
        {
            var rep = Resolve<ITestRepository>();
            var keyId = "123";
            var id = rep.Update(t => t.Id == keyId, t => new TESTENTITY()
            {
                TESTENTITY2ID = t.TESTENTITY2ID + "123" + t.Id,
            });
            var inputPlus = "inputPlus";
            var id1 = rep.Update(t => t.Id == keyId, t => new TESTENTITY() { TESTENTITY2ID = t.TESTENTITY2ID + inputPlus });

            var testEntity = rep.FirstOrDefault(t => t.Id != null);
            var setGuid = Guid.NewGuid().ToString();
            testEntity.TESTENTITY2ID = setGuid;
            rep.Update(testEntity);

        }

        [Fact]
        public void queryCombination()
        {
            var rep = Resolve<TestRepository>();
            var testEntity = rep.GetAllList();
            var tes =  rep.GetValue("123");

        }


        [Fact]
        public void sqlQuery()
        {
            var rep = Resolve<TestRepository>();

            var a = rep.SqlQueryPaging<TESTENTITY>(new Blocks.Framework.Data.Pager.Page() { page = 10, pageSize = 10 }, @"SELECT * FROM TESTENTITY " +
            "WHERE ID = '1'"  );
        }


        [Fact]
        public void DeleteByExpression()
        {
            var rep = Resolve<TestRepository>();
            var keyId = "123";
             rep.Delete(t => t.Id == keyId);
           

        }
    }
}