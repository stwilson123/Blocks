using Blocks.Framework.ApplicationServices;
using EntityFramework.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.New.Test.Model
{
    public class TransactionAppService : AppService, ITransactionAppService
    {
        private readonly ITestRepository testRepository;
        private readonly ITest2Repository test2Repository;

        public TransactionAppService(ITestRepository testRepository,ITest2Repository test2Repository)
        {
            this.testRepository = testRepository;
            this.test2Repository = test2Repository;
        }
        public int MultipleAction(TESTENTITY testEntity, TESTENTITY2 testEntity2)
        {
            var result = (this.testRepository.Insert(testEntity).Id != null ? 1 : 0);
            result += (this.test2Repository.Insert(testEntity2).Id != null ? 1 : 0);
            return result;
        }

        public int MultipleActionWhenException(TESTENTITY testEntity, TESTENTITY2 testEntity2)
        {
            var result = (this.testRepository.Insert(testEntity).Id != null ? 1 : 0);

            throw new TestTransactionException();

            //return (this.testRepository.Insert(testEntity).Id != null ? 1 : 0) + (this.test2Repository.Insert(testEntity2).Id != null ? 1 : 0);
        }
    }

    public interface ITransactionAppService : IAppService
    {
        int MultipleActionWhenException(TESTENTITY testEntity, TESTENTITY2 testEntity2);

        int MultipleAction(TESTENTITY testEntity, TESTENTITY2 testEntity2);

    }

    public class TestTransactionException : Exception
    {

    }
}
