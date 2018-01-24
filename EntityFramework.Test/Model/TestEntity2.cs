using Blocks.Framework.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blocks.BussnessEntityModule
{
    public partial class TESTENTITY2 : Entity
    {

    }

    public class TestEntity2Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TESTENTITY2>
    {
        public TestEntity2Configuration() 
        {
        }

        public TestEntity2Configuration(string schema)
        {
            ToTable("TestEntity2", schema);
            HasKey(x => x.Id);
           
        }
    }
}