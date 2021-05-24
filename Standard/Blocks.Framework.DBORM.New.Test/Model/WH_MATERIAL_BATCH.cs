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



    public partial class WPP_WORKHOUR_ITEM_D : Entity
    {

        [Column("ID")]
        public override string Id { set; get; }
        public string FROM_ID { set; get; }
        public string EQP_NO { set; get; }
        public decimal SETUP_TIME { set; get; }
        public decimal? BATCH_QTY { set; get; }
        public decimal? PROCESS_TIME { set; get; }
        public long ACTIVITY { set; get; }
    }
    public partial class WH_MATERIAL_BATCHConfiguration1 : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<WPP_WORKHOUR_ITEM_D>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WPP_WORKHOUR_ITEM_D> builder)
        {

            builder.HasKey(x => x.Id);

        }

    }
    public partial class EQP_BAS : Entity
    {

        [Column("ID")]
        public override string Id { set; get; }
        public string EQP_NATURE_GUID { set; get; }
        public string EQP_TYPE { set; get; }
        public string EQP_SPEC { set; get; }
        public string EQP_STATUS { set; get; }
        public string EQP_CONTRACT_NO { set; get; }
        public string EQP_LEVEL { set; get; }
        public string EQP_SERIALNO { set; get; }
        public DateTime? EQP_FACTORY_MADE_DATE { set; get; }
        public DateTime? EQP_IN_FACTORY_DATE { set; get; }
        public DateTime? EQP_INSTAL_DATE { set; get; }
        public DateTime? EQP_INUSE_DATE { set; get; }
        public string EQP_POWER { set; get; }
        public long? EQP_PRICE { set; get; }
        public long? EQP_ORIGIN_PRICE { set; get; }
        public long? EQP_LIMIT_YEAR { set; get; }
        public long? EQP_NET_SALVAGE_RATE { set; get; }
        public long? EQP_YEAR_DEPRE_RATE { set; get; }
        public DateTime? EQP_DEPRECIATION_START_DATE { set; get; }
        public string STOVE_SPEC { set; get; }
        public string STOVE_NO { set; get; }
        public string OLD_EQP_NO { set; get; }
        public long? COLLECT_GAS { set; get; }
        public long? COLLECT_POWER { set; get; }
        public long ACTIVITY { set; get; }
        public string EQP_NO { set; get; }
        public string EQP_NAME { set; get; }
    }
    public partial class WH_MATERIAL_BATCHConfiguration2 : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<EQP_BAS>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EQP_BAS> builder)
        {

            builder.HasKey(x => x.Id);

        }

    }

}