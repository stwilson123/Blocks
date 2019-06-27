﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blocks.Framework.Auditing
{
    /// <summary>
    /// Used to configure auditing.
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing system.
        /// Default: true. Set false to completely disable it.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Set true to enable saving audit logs if current user is not logged in.
        /// Default: false.
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// List of selectors to select classes/interfaces which should be audited as default.
        /// </summary>
        IAuditingSelectorList Selectors { get; }

        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        List<Type> IgnoredTypes { get; }
        
        
        List<Func<object,bool>> IgnoredFunc { get; }

        Dictionary<Type, Func<object, string>> TypeConverts { get; }
    }
}