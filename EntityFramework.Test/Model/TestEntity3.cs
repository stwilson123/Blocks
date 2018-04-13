using System.ComponentModel.DataAnnotations.Schema;
using Blocks.Framework.Data.Entity;

namespace EntityFramework.Test.Model
{
    public partial class TESTENTITY3 : Entity
    {
        [Column("ID")]
        public override string Id { get; set; }
        public string TESTENTITYID { set; get; }
        public string TESTENTITYID1 { get; set; }

        public TESTENTITY TESTENTITY { get; set; }
    }

    public class TestEntity3Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TESTENTITY3>
    {
        public TestEntity3Configuration() 
        {
            ToTable("TESTENTITY3");
            HasKey(x => x.Id);
            HasOptional(t => t.TESTENTITY).WithMany().HasForeignKey(t => t.TESTENTITYID1);
        }

     
    }
}