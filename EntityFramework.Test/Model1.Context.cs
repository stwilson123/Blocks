﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity;
using System.Configuration;
using EntityFramework.Test.Model;

namespace EntityFramework.Test
{
    public partial class BlocksEntities : DbContext
    {
        public BlocksEntities()
            : base("name=BlocksEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var schema = ConfigurationManager.AppSettings.Get("Schema");
            modelBuilder.HasDefaultSchema(schema);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new TestEntityConfiguration());
            modelBuilder.Configurations.Add(new TestEntity2Configuration());
            modelBuilder.Configurations.Add(new TestEntity3Configuration());

            //modelBuilder.Entity<TestEntity>().HasMany(t => t.TestEntity3s);
        }

        //public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        //public virtual DbSet<AbpAuditLogs> AbpAuditLogs { get; set; }
        //public virtual DbSet<AbpBackgroundJobs> AbpBackgroundJobs { get; set; }
        //public virtual DbSet<AbpEditions> AbpEditions { get; set; }
        //public virtual DbSet<AbpFeatures> AbpFeatures { get; set; }
        //public virtual DbSet<AbpLanguages> AbpLanguages { get; set; }
        //public virtual DbSet<AbpLanguageTexts> AbpLanguageTexts { get; set; }
        //public virtual DbSet<AbpNotifications> AbpNotifications { get; set; }
        //public virtual DbSet<AbpNotificationSubscriptions> AbpNotificationSubscriptions { get; set; }
        //public virtual DbSet<AbpOrganizationUnits> AbpOrganizationUnits { get; set; }
        //public virtual DbSet<AbpPermissions> AbpPermissions { get; set; }
        //public virtual DbSet<AbpRoles> AbpRoles { get; set; }
        //public virtual DbSet<AbpSettings> AbpSettings { get; set; }
        //public virtual DbSet<AbpTenantNotifications> AbpTenantNotifications { get; set; }
        //public virtual DbSet<AbpTenants> AbpTenants { get; set; }
        //public virtual DbSet<AbpUserAccounts> AbpUserAccounts { get; set; }
        //public virtual DbSet<AbpUserClaims> AbpUserClaims { get; set; }
        //public virtual DbSet<AbpUserLoginAttempts> AbpUserLoginAttempts { get; set; }
        //public virtual DbSet<AbpUserLogins> AbpUserLogins { get; set; }
        //public virtual DbSet<AbpUserNotifications> AbpUserNotifications { get; set; }
        //public virtual DbSet<AbpUserOrganizationUnits> AbpUserOrganizationUnits { get; set; }
        //public virtual DbSet<AbpUserRoles> AbpUserRoles { get; set; }
        //public virtual DbSet<AbpUsers> AbpUsers { get; set; }
        //public virtual DbSet<TestEntities> TestEntities { get; set; }
        public virtual DbSet<TESTENTITY> TestEntity { get; set; }
        public virtual DbSet<TESTENTITY2> TestEntity2 { get; set; }
        public virtual DbSet<TESTENTITY3> TestEntity3 { get; set; }
    }
}
