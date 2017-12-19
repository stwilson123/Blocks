using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blocks.BussnessEntityModule;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Repository;

namespace Blocks.BussnessRespositoryModule
{
    public class TestRepository : DBSqlRepositoryBase<DbContext,TestEntity>, ITestRepository
    {
        public TestRepository(IDbContextProvider<DbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }
        public string GetValue(string value)
        {
            return value;
        }
 
    }
}