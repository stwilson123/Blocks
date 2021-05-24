using System.Threading;
using EntityFramework.Test.Model;
using Xunit;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Blocks.Framework.DBORM;

namespace EntityFramework.Test.FunctionTest
{
     
    public class RepositoryTest : BlocksTestBase
    {
        [Fact]
        public void BatchAddPerformance()
        {
            var rep = Resolve<ITestRepository>();
            Stopwatch stopwatch = new Stopwatch();
          
            stopwatch.Start();
            //var trans = rep.Context.Database.BeginTransaction();执行时间
            var listTestEntity = new List<TESTENTITY>();
            for (int i = 0; i < 10000; i++)
            {
                listTestEntity.Add(new TESTENTITY() { Id = Guid.NewGuid().ToString(),  COLNUMINT = i, UPDATER = "1", CREATER = "1", TESTENTITY2ID = "11",COMMENT = "112312321" });
            }
            rep.Insert(listTestEntity);
            stopwatch.Stop();
          
            Assert.True(false, "Total Milliseconds:" + stopwatch.ElapsedMilliseconds);
            //trans.Commit();
        }
        [Fact]
        public void syncQueryMethod()
        { 
            var rep =  Resolve<ITestRepository>();
            
            Stopwatch sw = Stopwatch.StartNew();
            var firstData = rep.GetAllList().FirstOrDefault();
            sw.Stop();
            var a = sw.ElapsedMilliseconds;
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
        public async void queryNotToThrowLongIdetifier()
        {
            var rep = Resolve<ITestRepository>();
          
            var qwertyuioasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnm = "123";
            rep.FirstOrDefault(t => t.Id == qwertyuioasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnm);


            rep.GetLongIdetifier();

        }
        [Fact]
        public void UpdateByModel()
        {
            var rep = Resolve<ITestRepository>();
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
            var guid = Guid.NewGuid().ToString();

            var now = DateTime.Now;
            var initData =new TESTENTITY() {Id = guid, TESTENTITY2ID = "guid" + "2", COLNUMINT = 1, ISACTIVE = 1} ;
            rep.Insert(initData);

            rep.Update(rr => rr.Id == guid, RR => new TESTENTITY()
            {
                TESTENTITY2ID ="guid" + "2"  ,

            });
            
            var constValue = "123";
            var id = rep.Update(rr => rr.Id == guid && rr.CREATEDATE <= now, RR => new TESTENTITY()
            {
                TESTENTITY2ID = RR.TESTENTITY2ID + constValue  ,
                COMMENT = "123321"
              
            });
            var updatedData = rep.FirstOrDefault(t => t.Id == guid);
            Assert.Equal(updatedData.TESTENTITY2ID,initData.TESTENTITY2ID + "123"  );
            
            var inputPlus = "inputPlus";
            var id1 = rep.Update(t => t.Id == guid, t => new TESTENTITY() { TESTENTITY2ID = t.TESTENTITY2ID + inputPlus });
            
            Assert.Equal(rep.FirstOrDefault(t => t.Id == guid).TESTENTITY2ID,updatedData.TESTENTITY2ID +inputPlus);

            var testEntity = rep.FirstOrDefault(t => t.Id == guid);
            var setGuid = Guid.NewGuid().ToString();
            testEntity.TESTENTITY2ID = setGuid;
            rep.Update(testEntity);
            Assert.Equal(testEntity.TESTENTITY2ID,setGuid);


        }
        
        [Fact]
        public void UpdateByExpressionFixbug()
        {
            var rep = Resolve<ITestRepository>();
            var guid = Guid.NewGuid().ToString();

            var now = DateTime.Now;
            var initData =new TESTENTITY() {Id = guid, TESTENTITY2ID = "guid" + "2", COLNUMINT = 1, ISACTIVE = 1} ;
            rep.Insert(initData);

            var constValue = "123";
            
            
          
            var id = rep.Update(rr => rr.Id == guid && rr.CREATEDATE <= now, RR => new TESTENTITY()
            {
                TESTENTITY2ID = RR.TESTENTITY2ID + initData.TESTENTITY2ID  ,
                COMMENT = "123321"
            });
            var updatedData = rep.FirstOrDefault(t => t.Id == guid);
            
            Assert.Equal(updatedData.TESTENTITY2ID,initData.TESTENTITY2ID + initData.TESTENTITY2ID  );
            
       


        }


        [Fact]
        public void queryCombination()
        {
            var rep = Resolve<ITestRepository>();
            var testEntity = rep.GetAllList();
            var tes =  rep.GetValue("123");

        }


        [Fact]
        public void QueryBySqlWithArbitaryType()
        {
            var rep = Resolve<ITestRepository>();

            var a = rep.FromSql();
        }

        [Fact]
        public void GetQueryAliasWithKeyword()
        {
            var rep = Resolve<ITestRepository>();

            Assert.Throws<BlocksDBORMException>(() => rep.GetQueryAliasWithKeyword());
        }

        [Fact]
        public void DeleteByExpression()
        {
            var rep = Resolve<ITestRepository>();
            var guid = Guid.NewGuid().ToString();
            rep.Insert(new TESTENTITY() {Id = guid, TESTENTITY2ID = guid, COLNUMINT = 1, ISACTIVE = 1});
            
            Assert.NotNull(rep.FirstOrDefault(t => t.Id == guid ));
             rep.Delete(t => t.Id == guid );
            
            Assert.Null(rep.FirstOrDefault(t => t.Id == guid ));


        }



        [Fact]
        public void MultRepoFetch()
        {
            var rep = Resolve<ITestRepository>();
            
            var rep3 = Resolve<ITestRepository3>();
            rep.FirstOrDefault(t => t.Id == "");

            rep3.FirstOrDefault(t => t.Id == "");
        }

        
        [Fact]
        public void PageNotToThrowExceptionInOracle11gandsqlserver2008()
        {
        
            var rep = Resolve<ITestRepository>();
            rep.SkipAndTakeFromSql(1, 10);

            rep.SkipAndTake(1,10);


        }


        [Fact]
        public void InsertOrUpdateTest()
        {

            var rep = Resolve<ITestRepository>();
            Assert.Throws<NotImplementedException>(() => { rep.InsertOrUpdate(new TESTENTITY() {Id = "123"}); });



        }
        
        
        
        [Fact]
        public  void ExcuteSql()
        {
            var rep = Resolve<ITestRepository>();
            Assert.Equal(0,rep.ExecuteSqlCommand(Guid.NewGuid().ToString()));
            var newId = Guid.NewGuid().ToString();
            rep.Insert(new TESTENTITY()
            {
                Id = newId
            });
            //var a = rep.FirstOrDefault(t => t.Id == newId);
            //var rb =  rep.Delete(t => t.Id == newId);
           
            Assert.Equal(1,rep.ExecuteSqlCommand(newId));


        }
        
        
          
        [Fact]
        public void TestUpdateMethodWhereExpression_listContainsExpression_throw_exception()
        {

            //            var rep = Resolve<ITestRepository>();
            //            var list = new List<string>(){ "123"};
            //            rep.Update(t => list.Contains(t.Id) && t.COMMENT == null, testentity => new TESTENTITY()
            //            {
            //                COLNUMINT = 1
            //
            //            });

            var a = "";
          
            var rep = Resolve<IWhMaterialBatchRespository>();
            var lotNos = new List<string>(){ "2019112000001-LX150001","TTT51877T" }.Distinct();
            rep.Update(t =>lotNos.Contains(t.MATERIAL_BATCH) && t.DATE_INSTORAGE == null, testentity => new WH_MATERIAL_BATCH()
            {
                DATE_INSTORAGE = DateTime.Now
            });
            
            
            lotNos = new List<string>(){ "2019112000001-LX150001","TTT51877T" };
            rep.Update(t =>lotNos.Contains(t.MATERIAL_BATCH) && t.DATE_INSTORAGE == null, testentity => new WH_MATERIAL_BATCH()
            {
                DATE_INSTORAGE = DateTime.Now
            });
        }



        [Fact]
        public void TestPageOrderBy()
        {

            //            var rep = Resolve<ITestRepository>();
            //            var list = new List<string>(){ "123"};
            //            rep.Update(t => list.Contains(t.Id) && t.COMMENT == null, testentity => new TESTENTITY()
            //            {
            //                COLNUMINT = 1
            //
            //            });

            var rep = Resolve<ITestRepository>();

            var a =  rep.GetTestPageOrderBy();
        }

    }

   
}