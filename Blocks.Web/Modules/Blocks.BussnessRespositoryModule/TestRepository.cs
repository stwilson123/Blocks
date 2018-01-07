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
        public virtual string GetValue(string value)
        {
//            var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
                           //            var sql = GetAll().Select(result => new TestEntity() {
                           //                  Id = result.Id,
                           //                TestEntity2  = new TestEntity2() {  Id      = result.TestEntity2.Id},
                           //                   TestEntity3s =  result.TestEntity3s
                           //                });
                           //            return value;
           // var id = Guid.Parse("DDE679DA-AA68-426D-A6C3-FE66D9725490");
            var sql = GetContextTable().SelectToList(result => new {
                Id = result.Id,
                TestEntity2  = new  {   result.TestEntity2.Id},
              //  TestEntity3s =  result.TestEntity3s
            });
            return value;
        }
 
    }
}