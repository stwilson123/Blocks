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

            var firstData2 = rep.GetMultLeftJoin();

            
            
            var firstData = rep.GetTestEntity2Text();

            var firstData1 = rep.GetTESTENTITY3s();
            
         


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