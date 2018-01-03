using System;
using Blocks.Framework.DBORM.DBContext;

namespace Blocks.BussnessRespositoryModule.SqlserverModule
{
    public class TestRepository : Blocks.BussnessRespositoryModule.TestRepository
    {
        public TestRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
        public override string GetValue(string value)
        {
            value = "Sqlserver";
            return value;
        }
        
    }
}