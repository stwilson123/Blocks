using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EntityFramework.Test.Cache
{
    public class QueryCacheTest : BlocksTestBase
    {
        [Fact]
        public void DoubleSingleOrDefault()
        {
            
            using (var context = new BlocksEntities())
            {
                var id = Guid.NewGuid().ToString();
                context.TestEntity.FirstOrDefault(t => t.Id == id);

                context.TestEntity.FirstOrDefault(t => t.Id == id);

            }
        }



        [Fact]
        public void DoubleFind()
        {
            using (var context = new BlocksEntities())
            {
                Guid id = Guid.NewGuid();
         

                context.TestEntity.Find(id);
                context.TestEntity.Find(id);

            }
        }
    }
}
