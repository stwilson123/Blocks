using Blocks.BussnessEntityModule;
using Blocks.BussnessEntityModule.QueryEntity;
using Blocks.Framework.DBORM.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.BussnessRespositoryModule.OracleModule
{
    public class TestRepository : Blocks.BussnessRespositoryModule.TestRepository
    {
        public TestRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
        public override string GetValueOverride(string value)
        {
            this.ExecuteSqlCommand($"DELETE FROM TESTENTITY WHERE ID={0}", "123");
            var a = this.SqlQueryPaging<QueryEntity>(new Framework.Data.Pager.Page() { page = 10, pageSize = 10 }, @"SELECT * FROM TESTENTITY " +
              "WHERE ID = '1'"

              );
        
            value = "Oracle";
            return value;
        }

    }
}
