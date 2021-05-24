using System;
using System.Runtime.InteropServices.ComTypes;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using Blocks.Framework.Data.Entity;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.DBORM.Linq;
using EntityFramework.Test.FunctionTest;
using Microsoft.EntityFrameworkCore;
using Blocks.Framework.DBORM.New.Test.Model;
using System.ComponentModel.DataAnnotations.Schema;
using Blocks.Framework.Services.DataTransfer;

namespace EntityFramework.Test.Model
{
    public class TestRepository : DBSqlRepositoryBase<TESTENTITY>, ITestRepository
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
            //var guid = Guid.NewGuid().ToString();
            //var sql = GetContextTable()
            //    .Where(t => t.TestEntity2.Id == guid)
            //    .SelectToList(result => new {
            //    Id = result.Id,
            //    TestEntity2  = new  {   result.TestEntity2.Id},
            //    TestEntity3s =  result.TestEntity3s.Select(aa => new {  aa.Id })
            //});


            return value;
        }

        public List<TESTENTITY> GetTestEntity2Text()
        {
            return GetContextTable().SelectToDynamicList((TESTENTITY t) => new TESTENTITY
            {
                TESTENTITY2 = new TESTENTITY2()
                {
                    Text = t.TESTENTITY2.Text
                }
            });
        }

        public PageList<TESTENTITY> GetTestEntityDistinct()
        {
            return GetContextTable().Paging((TESTENTITY t) => new TESTENTITY
            {
                TESTENTITY2 = new TESTENTITY2()
                {
                    Text = t.TESTENTITY2.Text
                }
            }, new Page() {page = 1, pageSize = 10}, true);
        }
        
        public PageList<DtoModel> GetTestPageContainsEmptyString()
        {
            return GetContextTable().Paging((TESTENTITY t) => new DtoModel
            {
                 Id =  t.Id,
                  COLNUMINT = t.COLNUMINT
            }, new Page() {page = 2, pageSize = 10,filters = new Group(){ groupOp = "And",rules = new List<Rule>(){ new Rule(){ field = "Id", data = "['1','2']", op = "cn"}} }});
        }

        public PageList<ProcessPageResult> GetTestPageOrderBy()
        {
            return GetContextTable()
                //.OrderBy(process => process.COLNUMINT_NULLABLE)
                .Paging((TESTENTITY process) => new ProcessPageResult
                {
                    Id = process.Id,
                    OP_NO = process.STRING,
        
                    IS_CHECK = process.ISACTIVE,
            
                }, new Page() { page = 2, pageSize = 10, sortColumn = "OP_NO", sortOrder = "asc" });
        }

        public List<TESTENTITY> GetTESTENTITY3s()
        {
            return GetContextTable().SelectToDynamicList((TESTENTITY t) =>
                new TESTENTITY()
                {
                    TESTENTITY3s = t.TESTENTITY3s
                });
        }


        public class WorkHourItemPageResult : IDataTransferObject
        {
            /// <summary>
            /// 主键
            /// </summary>
            public string Id { set; get; }
            /// <summary>
            /// 主档表GUID
            /// </summary>
            public string FROM_ID { set; get; }
            /// <summary>
            /// 设备编号
            /// </summary>
            public string EQP_NO { set; get; }
            /// <summary>
            /// 固定工时
            /// </summary>
            public decimal SETUP_TIME { set; get; }
            /// <summary>
            /// 单位数量
            /// </summary>
            public decimal? BATCH_QTY { set; get; }
            /// <summary>
            /// 加工工时
            /// </summary>
            public decimal? PROCESS_TIME { set; get; }
            /// <summary>
            /// 是否删除(软删除)
            /// </summary>
            public long ACTIVITY { set; get; }
            /// <summary>
            /// 设备名称
            /// </summary>
            public string EQP_NAME { get; set; }
        }
        public object FromSql()
        {
          
            var takeNum = 1;
            var skipNum = 0;

            return Context.Set<TestDto>()
                .FromSqlRaw("SELECT TESTENTITY.ID FROM TESTENTITY  INNER JOIN TESTENTITY2  " +
                         "ON TESTENTITY.TESTENTITY2ID = TESTENTITY2.ID ")
                .Skip(skipNum).Take(takeNum)
                .ToList();

        }

        public object GetQueryAliasWithKeyword()
        {
            return GetContextTable()
               .SelectToList((TESTENTITY item) =>
                   new TESTENTITY()
                   {
                       Id = item.Id
                   });
        }

        public int ExecuteSqlCommand(string id)
        {
            var a=  this.SqlQuery<TestDto>("SELECt TO_CHAR(count(*)) as ID  FROM TESTENTITY WHERE ID = {0}",id);
            return this.ExecuteSqlCommand("DELETE FROM TESTENTITY WHERE ID = {0}", id);
        }

        public object GetLongIdetifier()
        {
            return GetContextTable().OrderBy(qwertyuioasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnm =>
                    qwertyuioasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnm.Id)
                .SelectToDynamicList((TESTENTITY qwertyuioasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnm) =>
                    new TESTENTITY()
                    {
                        Id = qwertyuioasdfghjklzxcvbnmqwertyuioasdfghjklzxcvbnm.Id
                    });
        }

        public object SkipAndTake(int page, int pageSize)
        {
            return GetContextTable().Paging((TESTENTITY t) => new TESTENTITY() {Id = t.Id}, new Page()
            {
                page = page,
                pageSize = pageSize
            });
        }

        public object SkipAndTakeFromSql(int page, int pageSize)
        {
            return SqlQueryPaging<TestDto>(new Page()
                {
                    pageSize = pageSize,
                    page = page
                }, "SELECT TESTENTITY.ID FROM TESTENTITY  INNER JOIN TESTENTITY2  " +
                   "ON TESTENTITY.TESTENTITY2ID = TESTENTITY2.ID ");
        }

        public List<TESTENTITY> GetMultLeftJoin()
        {
            var testEntities = new List<TESTENTITY>();
            var testEntitiy3s = new List<TESTENTITY3>();
           // testEntities.Join(testEntitiy3s, t => new { testEntityId = t.Id }, testEntity3 => new { testEntityId = testEntity3.TESTENTITYID },(a,b) => new {b })

         
            return GetContextTable()
                //.InnerJoin((TESTENTITY t) => t.Id , (TESTENTITY3 testEntity3) => testEntity3.TESTENTITYID )
                //.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID, (TESTENTITY2 testEntity2) => testEntity2.Id)
                .InnerJoin((TESTENTITY t) => new { testEntityId = t.Id, no = t.STRING }, (TESTENTITY3 testEntity3) => new { testEntityId = testEntity3.TESTENTITYID, no = testEntity3.CREATER })
                .LeftJoin((TESTENTITY t) => new { testEntityId = t.TESTENTITY2ID, no1 = t.STRING }, (TESTENTITY2 testEntity2) => new { testEntityId = testEntity2.Id, no1 = testEntity2.UPDATER })

                .SelectToList((TESTENTITY t,TESTENTITY2 testEntity2,TESTENTITY3 testEntity3) => new TESTENTITY()
                {
                    Id = t.Id, 
                    COLNUMINT = t.COLNUMINT,
                    TESTENTITY2 = new TESTENTITY2()
                    {
                        Id = testEntity2.Id,
                        CREATER = testEntity2.CREATER
                    } ,
                    TESTENTITY3s = new List<TESTENTITY3>()
                    {
                        new TESTENTITY3()
                        {
                            Id = testEntity3.Id,
                            CREATER = testEntity3.CREATER
                        }
                    }
//                    dtoModel3s = new List<DtoModel3>(){
//                        new DtoModel3()
//                        {
//                            Id = testEntity3.Id,
//                            CREATER = testEntity3.CREATER
//                        }
//                    }
                });
        }


        private IQueryable<TEntity> FromSqlTemp<TEntity>(IQueryable q, RawSqlString sql,
            params object[] parameters) where TEntity : class
        {
            return q.Provider.CreateQuery<TEntity>(
                Expression.Call(
                    null,
                    FromSqlMethodInfo.MakeGenericMethod(typeof(TEntity)),
                    q.Expression,
                    Expression.Constant(sql),
                    Expression.Constant(parameters)));
        }

        internal static readonly MethodInfo FromSqlMethodInfo
            = typeof(RelationalQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(FromSql))
                .Single(mi => mi.GetParameters().Length == 3);
    }


    public class Test2Repository : DBSqlRepositoryBase<TESTENTITY2>, ITest2Repository
    {
        public Test2Repository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
    public class TestRepository3 : DBSqlRepositoryBase<TESTENTITY3>, ITestRepository3
    {
        public TestRepository3(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    class TestDto : IQueryEntity
    {
        public string ID { get; set; }
    }
}