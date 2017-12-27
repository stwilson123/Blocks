﻿using System;
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
        public TestRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
        public string GetValue(string value)
        {
            var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
            var sql = GetAllIncluding(t => t.TestEntity2,t => t.TestEntity2.TestEntity3).Select(result => new  {
                Id = result.Id,
                 TestEntity2 = new { Id = result.TestEntity2.Id, b = result.TestEntity2.TestEntity3.TestId }
                }).ToString();
            return value;
        }
 
    }
}