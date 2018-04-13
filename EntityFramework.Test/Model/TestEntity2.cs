using System.ComponentModel.DataAnnotations.Schema;
using Blocks.Framework.Data.Entity;

namespace EntityFramework.Test.Model
{
    public partial class TESTENTITY2   : Entity   
    {
 
        [Column("ID")]
        public override string Id { set ; get ; }
        public string Text { set; get; }
    }
    public class TestEntity2Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TESTENTITY2>
    {
        public TestEntity2Configuration() 
        {
            ToTable("TESTENTITY2");
            HasKey(x => x.Id);
        }

       
    }
}