using System;
using Blocks.Framework.Data.Entity;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity3 : Entity
    {
       public string TestId { set; get; }

       public Guid TestEntityId { set; get; }

        public TestEntity TestEntity { get; set; }
        
    }

    public class TestEntity3Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TestEntity3>
    {
        public TestEntity3Configuration()
            : this("dbo")
        {
        }

        public TestEntity3Configuration(string schema)
        {
            ToTable("TestEntity3", schema);
            HasKey(x => x.Id);
          
        }
    }
}