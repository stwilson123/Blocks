using EntityFramework.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Test.Model;
using Xunit;
using Blocks.Framework.DBORM.Linq;
using Blocks.Framework.Services.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace Blocks.Framework.Test.DBORM.Linq
{
    public class LinqQueryableTest : BlocksTestBase
    {
        [Fact]
        public void joinGenSql()
        {
            using (var context = new BlocksEntities())
            {

                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID, (TESTENTITY2 b) => b.Id)
                    .SelectToList();

                var default1Sql = defaultLinqQuery.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                     .SelectToList();
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoin2Entity = defaultLinqQuery
                    .LeftJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                     .SelectToList();
                var default3Sql = defaultLinqQuery.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testJoinWithMultiFieldsEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => new { Id = t.TESTENTITY2ID_NULLABLE, ISACTIVE= t.ISACTIVE }, (TESTENTITY2 b) => new { Id= b.Id, ISACTIVE= 0L })
                     .SelectToList();
                var default4Sql = defaultLinqQuery.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                testJoinWithMultiFieldsEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => new { Id = t.TESTENTITY2ID_NULLABLE, ISACTIVE = t.ISACTIVE }, (TESTENTITY2 b) => new { Id = b.Id, ISACTIVE = b.DATAVERSION })
                     .SelectToList();
                var default5Sql = defaultLinqQuery.ToString();
            }
        }

        [Fact]
        public void whereGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var constKeyId = "123";
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery.Where((TESTENTITY t) => t.Id == constKeyId);

                var default1Sql = defaultLinqQuery.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t) => t.Id == constKeyId);
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
            }
        }
        [Fact]
        public void whereMultTableGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var constKeyId = "123";

                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t, TESTENTITY2 b) => (t.Id == constKeyId) || (b.Id == constKeyId));
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);

                var defaultLinqQuery1 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity1 = defaultLinqQuery1
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t, TESTENTITY2 b) => (t.Id == constKeyId) || (b.Id == constKeyId));
                var default2Sql1 = defaultLinqQuery.ToString();
            }
        }

        [Fact]
        public void selectGenSql()
        {
            using (var context = new BlocksEntities())
            {

                var constKeyId = "123";
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .SelectToDynamicList((TESTENTITY t) => new { Id = t.Id, TestEntity2 = new { Id = t.TESTENTITY2ID } });

                var default1Sql = defaultLinqQuery.ToString();

                var defaultLinqQuery1 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity1 = defaultLinqQuery1.InnerJoin((TESTENTITY t) => new { Id = t.TESTENTITY2ID_NULLABLE, Code = t.COLNUMINT }, (TESTENTITY2 b) => new { b.Id, Code = (decimal)1 })
                   .SelectToDynamicList((TESTENTITY t, TESTENTITY2 b) => new { Id = t.Id, TestEntity2 = new { Id = b.Id } });

                var default1Sql1 = defaultLinqQuery1.ToString();


                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 t) => t.Id == constKeyId);
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
            }
        }

        [Fact]
        public void orderByGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var constKeyId = "123";

                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery.OrderBy(t => t.Id);

                var default1Sql = defaultLinqQuery.ToString();


                var defaultLinqQuery1 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity1 = defaultLinqQuery1.OrderBy(t => new { t.Id, t.TESTENTITY2ID });

                var default1Sql1 = defaultLinqQuery1.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 t) => t.Id == constKeyId)
                    .OrderBy(t => t.Id);
                var default2Sql = defaultLinqQuery.ToString();


                var defaultLinqQuery3 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity3 = defaultLinqQuery3
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t) => t.Id == constKeyId)
                    .OrderBy(t => new { t.Id, t.TESTENTITY2ID });
                var default3Sql = defaultLinqQuery3.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);


                var defaultLinqQuery4 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity4 = defaultLinqQuery4
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t) => t.Id == constKeyId)
                    .OrderByDescending(t => new { t.Id, t.TESTENTITY2ID });
                var default4Sql = defaultLinqQuery4.ToString();


                var defaultLinqQuery5 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity5 = defaultLinqQuery5
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t) => t.Id == constKeyId)
                    .OrderBy(t => new { t.Id, t.TESTENTITY2ID })
                    .ThenBy(t => new { t.CREATER });
                var default5Sql = defaultLinqQuery5.ToString();



                var defaultLinqQuery6 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity6 = defaultLinqQuery6
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t) => t.Id == constKeyId)
                    .OrderBy(t => new { t.Id, t.TESTENTITY2ID })
                    .ThenByDescending(t => new { t.CREATER });
                var default6Sql = defaultLinqQuery6.ToString();
            }



        }
        class Dto
        {
            public string Id { get; set; }

            public long ACTIVITY { get; set; }

            public int Count { get; set; }

            public string Text { get; set; }
        }
        [Fact]
        public void GroupByGenSql()
        {

            using (var context = new BlocksEntities())
            {
                var constKeyId = "123";
                IEnumerable<TESTENTITY2> aList = new List<TESTENTITY2>();
                IEnumerable<TESTENTITY> bList = new List<TESTENTITY>();
                bList.Join(aList, k => k.TESTENTITY2ID, s => s.Id, (s, k) => new { s, k }).GroupBy(g => new { g.s.Id, g.k.Text }).Select(s => new { s.Key.Id, c = s.Sum(ttt => ttt.k.DATAVERSION) });
                bList.GroupBy(g => new { g.Id, g.ISACTIVE }).OrderBy(t => new { t.Key, c = t.Count() }).Select(s => new { s.Key.Id, count = s.Count(t => t.Id != null) });
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                // var testEntity = defaultLinqQuery.GroupBy((TESTENTITY t) => new { t.ACTIVITY }).SelectToList<IEnumerable<TESTENTITY>, Dto>((key, testEntities) => new Dto { ACTIVITY = key.ACTIVITY, Count = (int)testEntities.Sum(t => t.ACTIVITY) });

                //   var default1Sql = defaultLinqQuery.ToString();


                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 b) => b.Id == constKeyId)
                    .GroupBy((TESTENTITY t, TESTENTITY2 b) => new { t.Id, b.Text })
                    .SelectToList<IEnumerable<TESTENTITY>, Dto>((key, t) => new Dto { Text = key.Text, Count = (int)t.Sum(t => t.ISACTIVE) });


                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 b) => b.Id == constKeyId)
                    .OrderBy((TESTENTITY2 t) => t.CREATEDATE)
                    .Take(10)
                    .GroupBy((TESTENTITY t, TESTENTITY2 b) => new { t.Id, b.Text })
                    .SelectToList<IEnumerable<TESTENTITY>, Dto>((key, t) => new Dto { Text = key.Text, Count = (int)t.Sum(t => t.ISACTIVE) });


                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 b) => b.Id == constKeyId)
                    .GroupBy((TESTENTITY t, TESTENTITY2 b) => new { t.Id, b.Text })
                    .OrderBy<IEnumerable<TESTENTITY>>((key, t) => t.Count())
                    .ThenBy<IEnumerable<TESTENTITY2>>((key, b) => b.Sum(b => b.DATAVERSION))
                    .SelectToList<IEnumerable<TESTENTITY>, Dto>((key, t) => new Dto { Text = key.Text, Count = (int)t.Sum(t => t.ISACTIVE) });


                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 b) => b.Id == constKeyId)
                    .GroupBy((TESTENTITY t, TESTENTITY2 b) => new { t.Id, b.Text })
                    .Where<IEnumerable<TESTENTITY>>((key, t) => key.Id != null && t.Count() > 0)
                    .OrderBy<IEnumerable<TESTENTITY>>((key, t) => t.Count())
                    .ThenBy<IEnumerable<TESTENTITY2>>((key, b) => b.Sum(b => b.DATAVERSION))
                    .SelectToList<IEnumerable<TESTENTITY>, Dto>((key, t) => new Dto { Text = key.Text, Count = (int)t.Sum(t => t.ISACTIVE) });



            }



        }
        [Fact]
        public void pageGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var constKeyId = "123";
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .Paging((TESTENTITY t, TESTENTITY2 b) => new { Id = t.Id, TestEntity2 = new TESTENTITY2 { Text = b.Text } }, new Data.Pager.Page()
                   {
                       page = 2,
                       pageSize = 10,
                       sortColumn = "Id",
                       sortOrder = "asc"

                   });

                var default1Sql = defaultLinqQuery.ToString();

                var defaultLinqQuery1 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity1 = defaultLinqQuery1.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .SelectToDynamicList((TESTENTITY t, TESTENTITY2 b) => new { Id = t.Id, TestEntity2 = new { Id = b.Id } });

                var default1Sql1 = defaultLinqQuery1.ToString();


                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY2 t) => t.Id == constKeyId)
                    .SelectToDynamicList((TESTENTITY t, TESTENTITY2 b) => new { Id = t.Id, TestEntity2 = new { Id = b.Id } });

                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity1111 = defaultLinqQuery.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .Paging((TESTENTITY t) => new { Id = t.Id, TESTENTITY2ID = t.TESTENTITY2ID, TestEntity2 = new { Id = t.TESTENTITY2ID } }, new Data.Pager.Page()
                   {
                       page = 2,
                       pageSize = 10,
                       sortColumn = "Id Asc,TESTENTITY2ID desc"
                       //  sortOrder = "asc"
                   });
            }
        }

        [Fact]
        public async void selectToListAsync()
        {
            using (var context = new BlocksEntities())
            {


                var testEntity = await new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context).InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .SelectToListAsync((TESTENTITY t) => new { Id = t.Id, TestEntity2 = new { Id = t.TESTENTITY2ID } });
                IList<Task<object>> list = new List<Task<object>>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(testMethod(context));
                }
                foreach (var task in list)
                {
                    var bbbb = await testMethod(context);
                }
            }
        }


        public async Task<object> testMethod(BlocksEntities context)
        {
            var obj = await new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context).InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .SelectToListAsync((TESTENTITY t) => new { Id = t.Id, TestEntity2 = new { Id = t.TESTENTITY2ID } });
            return obj;
            // return await context.TestEntity.Select(t => t.Id).ToListAsync();
        }

        [Fact]
        public void countGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var constKeyId = "123";
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntityLinq = defaultLinqQuery.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID, (TESTENTITY2 b) => b.Id);

                var entityCount = testEntityLinq.Count();

                var entityCount2 = testEntityLinq.SelectToList((TESTENTITY t, TESTENTITY2 b) => new { b.Id, t.TESTENTITY2ID });

            }
        }


        [Fact]
        public void joinManyGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID, (TESTENTITY2 b) => b.Id);

                var default1Sql = defaultLinqQuery.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id);
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);

                var testLeftJoin2Entity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 t2) => t2.Id)
                    .InnerJoin((TESTENTITY2 t2) => t2.Id, (TESTENTITY3 t3) => t3.Id)
                    .SelectToList((TESTENTITY t, TESTENTITY2 t2, TESTENTITY3 t3) =>
                    new testDTO
                    {
                        Id = t.Id,
                        Text = t2.Text,
                        TESTENTITYID = t3.TESTENTITYID
                    });
                var default3Sql = defaultLinqQuery.ToString();
            }
        }

        [Fact]
        public void joinWhere()
        {
            using (var context = new BlocksEntities())
            {
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID, (TESTENTITY2 b) => b.Id)
                    .Where((TESTENTITY t) => t.TESTENTITY2ID == "123")
                     .SelectToList((TESTENTITY b) => new TESTENTITY() { Id = b.Id });

            }
        }
        class testDTO
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public string TESTENTITYID { get; set; }

        }
    }
}
