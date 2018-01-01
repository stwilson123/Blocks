﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity  
    {
        public Guid Id { set; get; }
        
        public  Guid TestEntity2ID { set; get; }
        
        public virtual TestEntity2 TestEntity2{
            get;
            set;
        }

        
        public virtual ICollection<TestEntity3> TestEntity3s
        {
            get;
            set;
        }
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