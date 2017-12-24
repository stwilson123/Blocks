using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blocks.BussnessEntityModule;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Repository;

namespace Blocks.BussnessRespositoryModule
{
    public class TestRepository : DBSqlRepositoryBase<TestEntity>, ITestRepository
    {
        public TestRepository(IDbContextProvider<BlocksDbContext<TestEntity>> dbContextProvider) : base(dbContextProvider)
        {
        }
        public string GetValue(string value)
        {
            var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
            var sql = GetAll().Join(Context.Set<TestEntity2>(), a => a.Id, b => b.Id, (a, b) => new {a, b}).Select(result => new { result.a.Id});
            return value;
        }
 
    }
}