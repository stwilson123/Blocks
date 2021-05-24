using Blocks.Framework.DBORM.Intercepter;
using EntityFramework.Test.Model;
using LinqToDB;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace EntityFramework.Test.FunctionTest
{
    public class EntityUpdateTest
    {
        public EntityUpdateTest()
        {
     //       System.Diagnostics.DiagnosticListener.AllListeners.Subscribe(new CommandListener());
            LinqToDBForEFTools.Initialize();

            //BatchUpdateManager.BatchUpdateBuilder = builder =>
            //{
            //    builder.Executing = (command) =>
            //    {
            //        Trace.TraceInformation("\r\n执行时间:{0} 毫秒 \r\n -->CommandExecuted.Command:\r\n{1}\r\nParamter:{2}", "", command.CommandText,
            //           string.Join(",", command.Parameters.Cast<IDbDataParameter>().Select(t => string.Format("{0}:{1}:{2};", t.ParameterName, t.DbType, t.Value)))
            //           );
            //    };

            //};
        }
        [Fact]
        public void DefaultConfigIsDetectChanges()
        {


            using (var context = new BlocksEntities())
            {

                //   var ttt = context.TestEntity.GroupBy(t => new { t.ACTIVITY }).Where(t => t.Count() > 0).Select(a =>  new { a.Key.ACTIVITY, c = a.Count() }).ToList();

                // var ttt = context.TestEntity.Join(context.TestEntity2,t => t.Id, t =>  t.Text,(t, t1) => new { t, t1 }).GroupBy(t => new { t.t.Id, t.t1.Text }).Where(t => t.Count() > 0).Select(a => new { a.Key.Id, c = a.Sum(s => s.t.IS_CHECK) }).ToList();
                //var a = context.TestEntity.Where(t => t.Id == "61a8c4ca-7f3f-4688-834c-8524a817e046").OrderBy(t => new { t.Id, t.ISACTIVE }).ToArray();

                //var b = context.TestEntity.Where(t => t.Id == "61a8c4ca-7f3f-4688-834c-8524a817e046").ToLinqToDB().Update(t1 => new TESTENTITY{ COLNUMINT = t1.COLNUMINT + 1 });

                var testEntity = context.TestEntity.Skip(0).Take(1).FirstOrDefault();

                testEntity.TESTENTITY2ID = Guid.NewGuid().ToString();
                Assert.Equal(context.Entry(testEntity).State, EntityState.Modified);
            }
        }
        
        
        [Fact]
        public void CloseAutoDetectAllModifyIsunchaned_ButCanManalDetect()
        {
            
            using (var context = new BlocksEntities(false))
            {
                context.ChangeTracker.AutoDetectChangesEnabled = false;
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
               // context.Configuration.AutoDetectChangesEnabled = false;
                var testEntities = context.TestEntity.Skip(0).Take(2).ToList();
                var newGuid = Guid.NewGuid().ToString();
                testEntities[0].TESTENTITY2ID = newGuid;
                var dbEntry = context.Entry(testEntities[0]);
                Assert.Equal(EntityState.Detached, dbEntry.State);

                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                testEntities = context.TestEntity.Skip(0).Take(2).ToList();
                Assert.Equal(EntityState.Unchanged, context.Entry(testEntities[0]).State);

                testEntities[0].COMMENT = "123";
                context.ChangeTracker.DetectChanges();
                Assert.Equal(EntityState.Modified, context.Entry(testEntities[0]).State);
       

            }
        }
        
        
        [Fact]
        public void GetDataWithNoTrackingIsDetached_notCache_notUpdate()
        {
            var id = String.Empty;
            var newGuid = String.Empty;
            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault();
                newGuid = Guid.NewGuid().ToString();
                testEntity.TESTENTITY2ID = newGuid;
                var EntityEntry = context.Entry(testEntity);
                Assert.Equal(EntityEntry.State, EntityState.Detached);

                id = testEntity.Id;
                context.SaveChanges();
                var newEntityNoTracking = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);
                Assert.NotEqual(newEntityNoTracking.TESTENTITY2ID, newGuid);

                var newEntity = context.TestEntity.FirstOrDefault(t => t.Id == id);
                Assert.NotEqual(newEntity.TESTENTITY2ID, newGuid);

            }

            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);

                Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
            }
        }


        [Fact]
        public void GetDataWithNoTrackingAttach()
        {
            var id = String.Empty;
            var newGuid = String.Empty;
            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault();
                newGuid = Guid.NewGuid().ToString();
                testEntity.TESTENTITY2ID = newGuid;
                var EntityEntry = context.Entry(testEntity);
                Assert.Equal(EntityEntry.State, EntityState.Detached);
                id = testEntity.Id;
                context.SaveChanges();
                var newEntity = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);
                Assert.NotEqual(newEntity.TESTENTITY2ID, newGuid);


            }

            using (var context = new BlocksEntities())
            {
                 
                var testEntity = context.TestEntity.FirstOrDefault(t => t.Id == id);

                Assert.NotEqual(testEntity.TESTENTITY2ID, newGuid);
            }
        }


   
    }


   
}