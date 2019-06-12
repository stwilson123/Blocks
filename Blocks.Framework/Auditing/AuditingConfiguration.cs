using System;
using System.Collections.Generic;

namespace Blocks.Framework.Auditing
{
    internal class AuditingConfiguration : IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }

        public bool IsEnabledForAnonymousUsers { get; set; }

        public IAuditingSelectorList Selectors { get; }

        public List<Type> IgnoredTypes { get; }
        
        public Dictionary<Type, Func<object, string>>  TypeConverts { get; }

        public AuditingConfiguration()
        {
            IsEnabled = true;
            Selectors = new AuditingSelectorList();
            IgnoredTypes = new List<Type>();
            TypeConverts = new Dictionary<Type, Func<object, string>> ();
        }
    }
}