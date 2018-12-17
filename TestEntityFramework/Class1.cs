using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace TestEntityFramework
{
    public class Class1
    {
        public void Test()
        {
            
        }
    }

    public class aaa : IEntityTypeConfiguration<Class1>
    {
        public void Configure(EntityTypeBuilder<Class1> builder)
        {
            throw new NotImplementedException();
        }
    }




}
