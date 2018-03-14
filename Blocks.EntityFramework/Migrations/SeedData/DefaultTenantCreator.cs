using System.Linq;
using Blocks.EntityFramework;
using Blocks.MultiTenancy;

namespace Blocks.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly BlocksDbContext _context;

        public DefaultTenantCreator(BlocksDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
