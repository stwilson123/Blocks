using System;
using System.Linq;
using System.Data.Entity;
using System.Text;
using Blocks.BussnessEntityModule;
using EntityFramework.Test;
using Xunit;

namespace UnitTestProject1
{

    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {

          
            using (var context = new BlocksEntities())
            {

                //var linq = context.TestEntity
                //    .Join(context.TestEntity3,t => t.Id, t=> t.TestEntityId, (a,b) => new testClass<TestEntity, TestEntity3> { TestEntity= a, TestEntity3 = b } )
                //    .Select(t => new { a= new { t.TestEntity.Id, t.TestEntity.TestEntity2ID },t.TestEntity3 });
                     

                ////                var linq = context.AbpUsers.Join(context.AbpUsers, a => a.Id, expression,(a,b) => new {a.AbpUsers2, a.AbpUsers4, b});
                ////                var linq2 = linq.Join(contexdt.AbpUsers, a => expression.Compile()(a.AbpUsers4), b => b.Id,
                ////                    (a, b) => new {a.AbpUsers2, a.AbpUsers4, b});
                ////                var linq2 = context.AbpUsers.Join(linq, a => a.Id, b => b.b.CreatorUserId, (a, b) => new { a, b })
                ////                    .Select(t => t.a);

                //var strLinq = linq.ToString();
                //var dataLinq = linq.ToList();
            }
        }

        class testClass<T1,T2>
        {
            public T1 TestEntity { get; set; }

            public T2 TestEntity3 { get; set; }
        }
    }
}
