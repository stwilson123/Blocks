﻿using Blocks.Framework.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blocks.BussnessEntityModule
{
    public partial class TESTENTITY3 : Entity
    {
        [Column("ID")]
        public override string Id { get; set; }
        public Guid? TestEntityId { set; get; }

    }

    public class TestEntity3Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TESTENTITY3>
    {
        public TestEntity3Configuration() 
        {
        }

        public TestEntity3Configuration(string schema)
        {
            ToTable("TestEntity3", schema);
            HasKey(x => x.Id);
          
        }
    }
}