using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blocks.Framework.Data.Entity;
using EntityFramework.Test.Model;

namespace EntityFramework.Test
{
    public class EntityConfig : IEntityConfiguration
    {
        public string EntityModule
        {
            get
            {
                return this.GetType().Assembly.GetName().Name;
            }
        }

        public Type[] extend { get; } = new[] {typeof(TestDto)};
    }
}
