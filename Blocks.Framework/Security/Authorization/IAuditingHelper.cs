using System.Reflection;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security.Authorization
{
    public interface IAuditingHelper : ISingletonDependency
    {
        bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = true);

    }
}