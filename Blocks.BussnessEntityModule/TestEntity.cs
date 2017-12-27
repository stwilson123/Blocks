using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity : Entity<Guid>
    {
        
        public  Guid TestEntity2ID { set; get; }
        
        [ForeignKey("TestEntity2ID")]
        public virtual TestEntity2 TestEntity2{
            get;
            set;
        }
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