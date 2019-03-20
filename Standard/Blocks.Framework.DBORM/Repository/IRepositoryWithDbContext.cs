using Microsoft.EntityFrameworkCore;

namespace Blocks.Framework.DBORM.Repository
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}