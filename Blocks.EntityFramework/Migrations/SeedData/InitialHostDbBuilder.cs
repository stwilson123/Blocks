using Blocks.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Blocks.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly BlocksDbContext _context;

        public InitialHostDbBuilder(BlocksDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
