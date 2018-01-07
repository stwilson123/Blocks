﻿using System;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace EntityFramework.Test.FunctionTest
{
    public class EntityUpdateTest
    {
        [Fact]
        public void DefaultConfigIsDetectChanges()
        {
            using (var context = new BlocksEntities())
            {
               var testEntity = context.TestEntity.FirstOrDefault();
                
                testEntity.TestEntity2ID = Guid.NewGuid();
                Assert.Equal(context.Entry(testEntity).State, EntityState.Modified);
 

            }
        }
        
        
        [Fact]
        public void CloseAutoDetectAllModifyIsunchaned_ButCanManalDetect()
        {
            using (var context = new BlocksEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var testEntities = context.TestEntity.Take(2).ToList();
                var newGuid = Guid.NewGuid();
                testEntities[0].TestEntity2ID = newGuid;
                var dbEntry = context.Entry(testEntities[0]);
                Assert.Equal(dbEntry.State, EntityState.Unchanged);
              


                context.ChangeTracker.DetectChanges();
                Assert.Equal(context.Entry(testEntities[0]).State, EntityState.Modified);
       

            }
        }
        
        
        [Fact]
        public void GetDataWithNoTrackingIsDetached_notCache_notUpdate()
        {
            var id = Guid.Empty;
            var newGuid = Guid.Empty;
            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault();
                newGuid = Guid.NewGuid();
                testEntity.TestEntity2ID = newGuid;
                var EntityEntry = context.Entry(testEntity);
                Assert.Equal(EntityEntry.State, EntityState.Detached);

                id = testEntity.Id;
                context.SaveChanges();
                var newEntityNoTracking = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);
                Assert.NotEqual(newEntityNoTracking.TestEntity2ID, newGuid);

                var newEntity = context.TestEntity.FirstOrDefault(t => t.Id == id);
                Assert.NotEqual(newEntity.TestEntity2ID, newGuid);

            }

            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);

                Assert.NotEqual(testEntity.TestEntity2ID, newGuid);
            }
        }


        [Fact]
        public void GetDataWithNoTrackingAttach()
        {
            var id = Guid.Empty;
            var newGuid = Guid.Empty;
            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault();
                newGuid = Guid.NewGuid();
                testEntity.TestEntity2ID = newGuid;
                var EntityEntry = context.Entry(testEntity);
                Assert.Equal(EntityEntry.State, EntityState.Detached);
                id = testEntity.Id;
                context.SaveChanges();
                var newEntity = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);
                Assert.NotEqual(newEntity.TestEntity2ID, newGuid);
                 
            }

            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault(t => t.Id == id);

                Assert.NotEqual(testEntity.TestEntity2ID, newGuid);
            }
        }
    }
}