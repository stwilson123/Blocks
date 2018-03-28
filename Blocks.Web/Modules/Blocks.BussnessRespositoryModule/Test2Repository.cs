using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blocks.BussnessDTOModule;
using Blocks.BussnessDTOModule.Combobox;
using Blocks.BussnessDTOModule.MasterData;
using Blocks.BussnessEntityModule;
using Blocks.Framework.Data.Combobox;
using Blocks.Framework.Data.Paging;
using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Linq;
using Blocks.Framework.DBORM.Repository;
using Blocks.Framework.Utility.SafeConvert;

namespace Blocks.BussnessRespositoryModule
{
    public class Test2Repository : DBSqlRepositoryBase<TESTENTITY2>, ITest2Repository
    {
        public Test2Repository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }

        
      
        PageList<ComboboxData> ITest2Repository.GetPageList(SearchModel search)
        {
            return GetContextTable().Paging((TESTENTITY2 t) => new ComboboxData{
                Id = t.Id,
                Text = t.Text
            }, search.page);
        }
    }
}