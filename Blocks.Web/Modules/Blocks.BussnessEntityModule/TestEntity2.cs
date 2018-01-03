using Blocks.Framework.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity2 : Entity
    {
       public Guid TestEntity3ID { set; get; }

      
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