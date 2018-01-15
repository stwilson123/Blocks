using System.Threading;
using EntityFramework.Test.Model;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Test.FunctionTest
{
    public class RepositoryTest : BlocksTestBase
    {
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
        public void Update()
        {
            var rep = Resolve<TestRepository>();
            var testEntity = rep.GetAllList();
            rep.GetAll();

        }


        [Fact]
        public void queryCombination()
        {
            var rep = Resolve<TestRepository>();
            var testEntity = rep.GetAllList();
            var tes =  rep.GetValue("123");

        }
    }
}