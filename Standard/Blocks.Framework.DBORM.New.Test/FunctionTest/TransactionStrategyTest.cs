using Blocks.Framework.DBORM.New.Test.Model;
using EntityFramework.Test;
using EntityFramework.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blocks.Framework.DBORM.New.Test.FunctionTest
{
    public class TransactionStrategyTest : BlocksTestBase
    {

        [Fact]
        //Test in tow module????
        public void TransactionInMultipleRepository()
        {
            var rep = Resolve<ITestRepository>();
            var rep2 = Resolve<ITest2Repository>();

            var appService = Resolve<ITransactionAppService>();
            var guid = Guid.NewGuid().ToString();
            var testEntity = new TESTENTITY() { Id = guid, TESTENTITY2ID = guid, COLNUMINT = 1, ISACTIVE = 1 };
            var testEntity2 = new TESTENTITY2() { Id = guid, CREATEDATE = DateTime.Now };

            appService.MultipleAction(testEntity, testEntity2);

            Assert.NotNull(rep.FirstOrDefault(t => t.Id == guid));
            Assert.NotNull(rep2.FirstOrDefault(t => t.Id == guid));
            rep.Delete(testEntity);
            rep2.Delete(testEntity2);

            guid = Guid.NewGuid().ToString();
            testEntity.Id = guid;
            testEntity2.Id = guid;


            Assert.Throws<TestTransactionException>(() => appService.MultipleActionWhenException(testEntity, testEntity2));

            Assert.Null(rep.FirstOrDefault(t => t.Id == guid));
            //rep.Delete(testEntity);
            //rep2.Delete(testEntity2);

        }
    }
}
