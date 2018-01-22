using System.Data.Common;
using Abp.Zero.EntityFramework;
using Blocks.Authorization.Roles;
using Blocks.Authorization.Users;
using Blocks.MultiTenancy;
using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.Migrations.History;

namespace Blocks.EntityFramework
{
    public class BlocksDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public BlocksDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in BlocksDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of BlocksDbContext since ABP automatically handles it.
         */
        public BlocksDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public BlocksDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public BlocksDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var schema = ConfigurationManager.AppSettings.Get("Schema");
            modelBuilder.HasDefaultSchema(schema);
            modelBuilder.Entity<HistoryRow>().ToTable(tableName: "MigrationHistory", schemaName: schema);
            modelBuilder.Entity<HistoryRow>().Property(p => p.MigrationId).HasColumnName("Migration_ID");
            modelBuilder.Entity<HistoryRow>().HasKey(p => p.MigrationId);
            modelBuilder.Entity<HistoryRow>().HasKey(p => p.ContextKey);

            base.OnModelCreating(modelBuilder);
        }
    }
}
