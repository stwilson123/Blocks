using Blocks.Framework.DBORM.DBContext;
using Blocks.Framework.DBORM.Repository;

namespace EntityFramework.Test.Model
{
    public class WhMaterialBatchRespository : DBSqlRepositoryBase<WH_MATERIAL_BATCH>,IWhMaterialBatchRespository
    {
        public WhMaterialBatchRespository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}