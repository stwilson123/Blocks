using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Test.Model;
using Xunit;
using  Blocks.Framework.DBORM.Linq;
namespace EntityFramework.Test.FunctionTest
{
    public class RelationShipTest: BlocksTestBase
    {
       [Fact]
        public void OneToOneMethod()
        {



            var list = new List<MyClassModel>();
            var list2 = new List<MyClassModel2>();
            
//            var groupList = list.GroupJoin(list2,t => t.MyClass2Id, p => p.id, (inner, outer) => new { inner, outer} )
//                .SelectMany(joinResult => joinResult.outer.DefaultIfEmpty(),
//                    (a,b) => {
//                        a.inner,
//                       b
//            }
//            )


            var rep =  Resolve<ITestRepository>();
            var rep2 = Resolve<ITest2Repository>();
            var rep3 = Resolve<ITestRepository3>();

            var relationStr = "testEntityStr";
            var testEntity2 = new TESTENTITY2()
            {
                Id = System.Guid.NewGuid().ToString(),
                CREATEDATE = DateTime.Now,
                CREATER = "testEntity2",
                UPDATER = relationStr
            };
           
            var testEntity = new TESTENTITY() {
                Id = System.Guid.NewGuid().ToString(),
                TESTENTITY2ID = testEntity2.Id,
                STRING = relationStr,
                CREATEDATE = DateTime.Now,
            };
            var testEntity3 = new TESTENTITY3()
            {
                Id = System.Guid.NewGuid().ToString(),
                TESTENTITYID = testEntity.Id,
                TESTENTITYID1 = "2",
                CREATER = relationStr,
                CREATEDATE = DateTime.Now,
            };
            var testEntity31 = new TESTENTITY3()
            {
                Id = System.Guid.NewGuid().ToString(),
                TESTENTITYID = testEntity.Id,
                TESTENTITYID1 = "1",
                CREATER = relationStr,
                CREATEDATE = DateTime.Now,
            };
            rep.Insert(testEntity);
            rep2.Insert(testEntity2);
            rep3.Insert(testEntity3);
            rep3.Insert(testEntity31);

             var firstData2 = rep.GetMultLeftJoin();

            
            
            var firstData = rep.GetTestEntity2Text();

            var firstData1 = rep.GetTESTENTITY3s();

            rep.Delete(testEntity);
            rep2.Delete(testEntity2);
            rep3.Delete(testEntity3);



            // var firstData2 = Resolve<TestRepository3>().GetContextTable().SelectToDynamicList((TESTENTITY3 t) => t.TESTENTITY);
        }

    }

    class MyClassModel
    {
        public string id { get; set; }

        public string MyClass2Id { get; set; }
        
    }

    class MyClassModel2
    
    {
        public string id { get; set; }
        
    }
}