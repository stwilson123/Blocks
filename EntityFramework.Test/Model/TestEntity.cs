using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Blocks.Framework.Data.Entity;

namespace Blocks.BussnessEntityModule
{
    public partial class TestEntity : Entity
    {

        public string TestEntity2ID { set; get; }

        public TestEntity2 TestEntity2 { set; get; }

        public IList<TestEntity3> TestEntity3s { set; get; }

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