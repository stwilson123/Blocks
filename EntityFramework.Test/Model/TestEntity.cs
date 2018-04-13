using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Blocks.Framework.Data.Entity;

namespace EntityFramework.Test.Model
{
    public partial class TESTENTITY   : Entity   
    {
 
        [Column("ID")]
        public override string Id { set ; get ; }
        public string TESTENTITY2ID { set; get; }
        public decimal COLNUMINT { set; get; }
        public string TESTENTITY2ID_NULLABLE { set; get; }
        
        public decimal? COLNUMINT_NULLABLE { set; get; }
        public string STRING { set; get; }
        public long ISACTIVE { set; get; }
        public string COMMENT { set; get; }
        public TESTENTITY2 TESTENTITY2 { set; get; }
        public ICollection<TESTENTITY3> TESTENTITY3s { set; get; }
    }

    public class TestEntityConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TESTENTITY>
    {
        public TestEntityConfiguration() 
        {
            ToTable("TESTENTITY");
            HasKey(x => x.Id);
            HasOptional(t => t.TESTENTITY2).WithMany().HasForeignKey(t => t.TESTENTITY2ID_NULLABLE);
            HasMany(t => t.TESTENTITY3s).WithOptional().HasForeignKey(t => t.TESTENTITYID1);

 
     
        }
        
        
 
    }
}