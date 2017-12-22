using System;
using Abp.Domain.Entities;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity : Entity<Guid>
    {
       
    }

    public class TestEntityConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TestEntity>
    {
        public TestEntityConfiguration()
            : this("dbo")
        {
        }

        public TestEntityConfiguration(string schema)
        {
            ToTable("TestEntity", schema);
            HasKey(x => x.Id);

        }
    }
}