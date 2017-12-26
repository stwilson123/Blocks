using EntityFramework.Test.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EntityFramework.Test
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            using (var context = new BlocksEntities1())
            {
                var linq = context.AbpUsers.SqlQuery(@"SELECT  *
  FROM[Blocks].[dbo].[AbpUsers] a INNER JOIN dbo.AbpUsers b  ON a.CreatorUserId = b.CreatorUserId");
                var strLinq = linq.ToString();
                var dataLinq = linq.ToList();
            }
        }



        [Fact]
        public void Test2()
        {
            using (var context = new BlocksEntities1())
            {
                var linq = context.AbpUsers.Join(context.AbpUsers, a => a.Id, b => b.Id, (a, b) => new Dictionary<string,object> {
                  //  new KeyValuePair<string,object>("123", a)
                    });
//                var linq2 = context.AbpUsers.Join(linq, a => a.Id, b => b.b.CreatorUserId, (a, b) => new { a, b })
//                    .Select(t => t.a);

             var strLinq = linq.ToString();
                var dataLinq = linq.ToList();
            }
        }

        public static Int64 Get(AbpUsers a)
        {
            return a.Id;
        }
    }
}