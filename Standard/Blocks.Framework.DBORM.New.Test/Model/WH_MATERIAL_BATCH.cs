using System;
using System.ComponentModel.DataAnnotations.Schema;
using Blocks.Framework.Data.Entity;

namespace EntityFramework.Test.Model
{
    public partial class WH_MATERIAL_BATCH   : Entity   
    {
 
        [Column("ID")]
        public override string Id { set ; get ; }
        public string MATERIAL_BATCH { set; get; }
        public string MATERIAL_ID { set; get; }
        public string MATERIAL_CODE { set; get; }
        public string MATERIAL_NAME { set; get; }
        public string SUPPLIER_ID { set; get; }
        public string SUPPLIER_CODE { set; get; }
        public string SUPPLIER_NAME { set; get; }
        public DateTime? DATE_RECEIVE { set; get; }
        public DateTime? DATE_INSPECT { set; get; }
        public DateTime? DATE_PRODUCT { set; get; }
        public DateTime? DATE_INSTORAGE { set; get; }
        public decimal? MATERIAL_WEIGHT { set; get; }
        public string WEIGHT_UNIT_ID { set; get; }
        public decimal? MATERIAL_VOLUME { set; get; }
        public string VOLUME_UNIT_ID { set; get; }
        public decimal? SHELF_LIFE { set; get; }
        public decimal? MATERIAL_BATCH_TYPE { set; get; }
        public long? ACTIVITY { set; get; }
        public long? ISUSED { set; get; }
        public string SOURCE_CODE { set; get; }
    
    }
    
    public partial class WH_MATERIAL_BATCHConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<WH_MATERIAL_BATCH>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WH_MATERIAL_BATCH> builder)
        {

            builder.HasKey(x => x.Id);

        }

    }

}