using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blocks.Framework.DBORM.DBContext
{
    class DynamicModelCacheKeyFactory : Microsoft.EntityFrameworkCore.Infrastructure.IModelCacheKeyFactory
    {
        public object Create(DbContext context)
        {
            return context is BlocksDbContext blocksDbContext
            ? (context.GetType(), blocksDbContext.ModuleName)
            : (object)context.GetType();
        }
    }
}
