using System;

namespace Blocks.Framework.Security.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute
    {
        
    }
}