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
    public partial class TESTENTITY2Configuration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TESTENTITY2>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TESTENTITY2> builder)
        {
           
           // builder.HasKey(x => x.Id);

        }

    }
}