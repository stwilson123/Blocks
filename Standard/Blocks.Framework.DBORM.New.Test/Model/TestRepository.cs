using System;
using System.Runtime.InteropServices.ComTypes;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Blocks.Framework.Data.Entity;
using Blocks.Framework.Data.Pager;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.DBORM.Linq;
using EntityFramework.Test.FunctionTest;
using Microsoft.EntityFrameworkCore;

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
            },new Page(){ page = 1,pageSize = 10},true);
        }

        public List<TESTENTITY> GetTESTENTITY3s()
        {
            return GetContextTable().SelectToDynamicList((TESTENTITY t) =>
                new TESTENTITY()
                {
                    TESTENTITY3s = t.TESTENTITY3s
                });
        }


        public object FromSql()
        {
            var takeNum = 1;
            var skipNum = 0;

            return Context.Query<TestDto>()
                .FromSql("SELECT TESTENTITY.ID FROM TESTENTITY  INNER JOIN TESTENTITY2  " +
                         "ON TESTENTITY.TESTENTITY2ID = TESTENTITY2.ID ")
                .Skip(skipNum).Take(takeNum)
                .ToList();


//            return FromSqlTemp<TestDto>(this.Table,"SELECT * FROM TESTENTITY  INNER JOIN TESTENTITY2 t2 " +
//                                      "ON TESTENTITY.TESTENTITY2ID = TESTENTITY2.ID WHERE TESTENTITY.ID = '12'")
//               
//                .Skip(10).Take(10).ToList();
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
            return GetContextTable().Paging((TESTENTITY t) => new TESTENTITY(){ Id = t.Id}, new Page()
            {
                  page = page,
                  pageSize = pageSize
                
            } );
        }
        
        public object SkipAndTakeFromSql(int page, int pageSize)
        {


            return SqlQueryPaging<TestDto>(new Page()
                {
                     pageSize = pageSize,
                    page = page
                    
                },"SELECT TESTENTITY.ID FROM TESTENTITY  INNER JOIN TESTENTITY2  " +
                                           "ON TESTENTITY.TESTENTITY2ID = TESTENTITY2.ID ");
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