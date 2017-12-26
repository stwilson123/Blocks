//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFramework.Test.Properties
{
    using System;
    using System.Collections.Generic;
    
    public partial class AbpTenants
    {
        public int Id { get; set; }
        public Nullable<int> EditionId { get; set; }
        public string Name { get; set; }
        public string TenancyName { get; set; }
        public string ConnectionString { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<long> DeleterUserId { get; set; }
        public Nullable<System.DateTime> DeletionTime { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<long> LastModifierUserId { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Nullable<long> CreatorUserId { get; set; }
    
        public virtual AbpEditions AbpEditions { get; set; }
        public virtual AbpUsers AbpUsers { get; set; }
        public virtual AbpUsers AbpUsers1 { get; set; }
        public virtual AbpUsers AbpUsers2 { get; set; }
    }
}
