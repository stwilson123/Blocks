using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Blocks.Framework.Data.Entity;

namespace Blocks.BussnessEntityModule
{
    public partial class TESTENTITY : Entity
    {
        [Column("ID")]
        public override string Id { get; set; }

        public string TESTENTITY2ID { set; get; }


        public string TESTENTITY2ID_NULLABLE { set; get; }


        public int COLNUMINT { get; set; }

        public int COLNUMINT_NULLABLE { get; set; }
        public TESTENTITY2 TestEntity2 { set; get; }

        public IList<TESTENTITY3> TestEntity3s { set; get; }

    }

    public class TestEntityConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TESTENTITY>
    {
        public TestEntityConfiguration() 
        {
        }
        
        

        public TestEntityConfiguration(string schema)
        {
            ToTable("TestEntity", schema);
            HasKey(x => x.Id);
           

        }
    }
}