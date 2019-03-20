using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blocks.Framework.Data.Entity;

namespace EntityFramework.Test
{
    public class EntityConfig : IEntityConfiguration
    {
        public string EntityModule { get; } = "EntityFramework.Test";
        public Type[] extend { get; }
    }
}
