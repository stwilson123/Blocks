using System.Data.Entity;

namespace Blocks.Framework.DBORM.Repository
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}