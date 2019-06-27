using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Blocks.Framework.Auditing
{
    public interface IAuditingHelper
    {
        bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false);

        AuditInfo CreateAuditInfo(Type type, MethodInfo method, object[] arguments);

        AuditInfo CreateAuditInfo(Type type, MethodInfo method, IDictionary<Tuple<string,IEnumerable<Attribute>>, object> arguments);

        AuditInfo UpdateAuditInfo(AuditInfo auditInfo,Exception ex,object returnParams);
        
        void Save(AuditInfo auditInfo);

        Task SaveAsync(AuditInfo auditInfo);
    }
}