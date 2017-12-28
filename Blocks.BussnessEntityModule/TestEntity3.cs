using System;
using Abp.Domain.Entities;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity3 : Entity<Guid>
    {
       public string TestId { set; get; }

       public Guid TestEntityId { set; get; }
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