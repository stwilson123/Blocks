using System;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity2 : Entity<Guid>
    {
       public Guid TestEntity3ID { set; get; }

        [ForeignKey("TestEntity3ID")]
        public virtual TestEntity3 TestEntity3
        {
            get;
            set;
        }
    }

    public class TestEntity2Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TestEntity2>
    {
        public TestEntity2Configuration()
            : this("dbo")
        {
        }

        public TestEntity2Configuration(string schema)
        {
            ToTable("TestEntity2", schema);
            HasKey(x => x.Id);

        }
    }
}