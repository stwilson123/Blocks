using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Blocks.Framework.DBORM.Test
{
   public  class ConfigurationManagerWrapper
    {

        public static Dictionary<string, ConnectionStringSettings> ConnectionStrings =
            new Dictionary<string, ConnectionStringSettings>() {
                { "BlocksEntities", new ConnectionStringSettings("BlocksEntities",
                "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.40.3.223)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=BOWAYORCL)));User ID=boway;Password=boway;Pooling=true;Max Pool Size=500;Min Pool Size=0;Persist Security Info=true;Enlist=false",
                "Oracle.ManagedDataAccess.Client")}

            };
    }
}
