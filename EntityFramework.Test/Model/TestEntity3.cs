using Blocks.Framework.Data.Entity;
using System;

namespace Blocks.BussnessEntityModule
{
    public partial class TestEntity3 : Entity
    {

        public Guid? TestEntityId { set; get; }

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