﻿using System;
using Abp.Domain.Entities;

namespace Blocks.BussnessEntityModule
{
    public class TestEntity2 : Entity<Guid>
    {
       
    }

    public class TestEntity2Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TestEntity>
    {
        public TestEntity2Configuration()
            : this("dbo")
        {
        }

        public TestEntity2Configuration(string schema)
        {
            ToTable("TestEntity2", schema);
            HasKey(x => x.Id);

        }
    }
}