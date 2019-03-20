using Blocks.Framework.Data;

namespace Blocks.Framework.DBORM.Repository
{
    public static class EfAutoRepositoryTypes
    {
        public static AutoRepositoryTypesAttribute Default { get; }

        static EfAutoRepositoryTypes()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof(Blocks.Framework.Data.IRepository<>),
                typeof(IRepository<,>),
                typeof(DBSqlRepositoryBase<>),
                typeof(DBSqlRepositoryBase<,,>)
                 
            );
        }
    }
}