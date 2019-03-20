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
            var rep =  Resolve<ITestRepository>();

            var firstData = rep.GetTestEntity2Text();

            var firstData1 = rep.GetTESTENTITY3s();

           // var firstData2 = Resolve<TestRepository3>().GetContextTable().SelectToDynamicList((TESTENTITY3 t) => t.TESTENTITY);
        }

    }
}