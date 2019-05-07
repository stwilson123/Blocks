using System.Reflection;

namespace Blocks.Framework.Security.Authorization
{
    public class AuditingHelper : IAuditingHelper
    {
        
        public bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = true)
        {
//            if (!_configuration.IsEnabled)
//            {
//                return false;
//            }
//
//            if (!_configuration.IsEnabledForAnonymousUsers && (AbpSession?.UserId == null))
//            {
//                return false;
//            }

            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

//            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
//            {
//                return true;
//            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
//                if (classType.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
//                {
//                    return true;
//                }

                if (classType.GetTypeInfo().IsDefined(typeof(DisableAuditingAttribute), true))
                {
                    return false;
                }

//                if (_configuration.Selectors.Any(selector => selector.Predicate(classType)))
//                {
//                    return true;
//                }
            }

            return defaultValue;
        }

    }
}