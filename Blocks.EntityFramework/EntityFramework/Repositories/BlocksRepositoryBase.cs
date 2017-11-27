using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Blocks.EntityFramework.Repositories
{
    public abstract class BlocksRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BlocksDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BlocksRepositoryBase(IDbContextProvider<BlocksDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class BlocksRepositoryBase<TEntity> : BlocksRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BlocksRepositoryBase(IDbContextProvider<BlocksDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
