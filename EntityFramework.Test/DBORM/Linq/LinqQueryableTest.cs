using Blocks.Framework.DBORM.Linq;
using EntityFramework.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Test.Model;
using Xunit;

namespace Blocks.Framework.Test.DBORM.Linq
{
    public class LinqQueryableTest : BlocksTestBase
    {
        [Fact]
        public void joinGenSql()
        {
            using (var context = new BlocksEntities())
            {
                var defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(),context);
                var testEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID, (TESTENTITY2 b) => b.Id);
                    
                var default1Sql = defaultLinqQuery.ToString();

                defaultLinqQuery = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testLeftJoinEntity = defaultLinqQuery
                    .InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id);
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
                
                var testLeftJoin2Entity = defaultLinqQuery
                    .LeftJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id);
                var default3Sql = defaultLinqQuery.ToString();
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
                    .Where((TESTENTITY t, TESTENTITY2 b) => (t.Id == constKeyId) ||(b.Id == constKeyId));
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
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
                    .SelectToDynamicList((TESTENTITY t) => new { Id =  t.Id, TestEntity2 = new { Id =  t.TESTENTITY2ID } });

                var default1Sql = defaultLinqQuery.ToString();

                var defaultLinqQuery1 = new DefaultLinqQueryable<TESTENTITY>(context.TestEntity.AsQueryable(), context);
                var testEntity1 = defaultLinqQuery1.InnerJoin((TESTENTITY t) => t.TESTENTITY2ID_NULLABLE, (TESTENTITY2 b) => b.Id)
                   .SelectToDynamicList((TESTENTITY t,TESTENTITY2 b) => new { Id = t.Id, TestEntity2 = new { Id = b.Id } });

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
                var testEntity1 = defaultLinqQuery1.OrderBy(t =>new { t.Id ,t.TESTENTITY2ID});

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
                    .OrderBy(t =>new { t.Id,t.TESTENTITY2ID });
                var default3Sql = defaultLinqQuery3.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
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
                   .Paging((TESTENTITY t) => new { Id = t.Id, TestEntity2 = new { Id = t.TESTENTITY2ID } }, new Data.Pager.Page()
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
                    .Where((TESTENTITY2 t) => t.Id == constKeyId);
                var default2Sql = defaultLinqQuery.ToString();
                //Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
            }
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

                var entityCount2 = testEntityLinq.SelectToDynamicList((TESTENTITY t,TESTENTITY2 b) => new{ t.TESTENTITY2ID, b.Id});
            }
        }
    }
}
