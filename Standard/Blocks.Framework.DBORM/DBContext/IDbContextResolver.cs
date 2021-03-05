using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM.DBContext
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(string connectionString,string moduleName)
            where TDbContext : DbContext;

        TDbContext Resolve<TDbContext>(DbConnection existingConnection, string moduleName, bool contextOwnsConnection)
            where TDbContext : DbContext;
    }
}
