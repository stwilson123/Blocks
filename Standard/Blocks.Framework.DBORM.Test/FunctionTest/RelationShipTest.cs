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
            var rep =  Resolve<TestRepository>();
            var firstData = rep.GetContextTable().SelectToDynamicList((TESTENTITY t) => t.TESTENTITY2.Text);

            var firstData1 = rep.GetContextTable().SelectToDynamicList((TESTENTITY t) => t.TESTENTITY3s);

            var firstData2 = Resolve<TestRepository3>().GetContextTable().SelectToDynamicList((TESTENTITY3 t) => t.TESTENTITY);
        }

    }
}