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
            return value;
        }
 
    }
}