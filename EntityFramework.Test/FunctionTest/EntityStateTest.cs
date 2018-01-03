using System;
using System.Data.Entity;
using System.Linq;
using MyTest;
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
        public void CloseAutoDetect()
        {
            using (var context = new BlocksEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var testEntity = context.TestEntity.FirstOrDefault();
                
                testEntity.TestEntity2ID = Guid.NewGuid();
                var state = context.Entry(testEntity).State;
                Assert.Equal(state, EntityState.Unchanged);
            }
        }
        
        
        [Fact]
        public void OnceGetDataNoDetectIsDetached()
        {
            using (var context = new BlocksEntities())
            {
                var testEntity = context.TestEntity.AsNoTracking().FirstOrDefault();
                
                testEntity.TestEntity2ID = Guid.NewGuid();
                var state = context.Entry(testEntity).State;
                Assert.Equal(state, EntityState.Detached);
            }
        }
    }
}