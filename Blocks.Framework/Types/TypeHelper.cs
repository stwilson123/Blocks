using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Blocks.Framework.Types
{
    public static class TypeHelper
    {
        public static bool GetActualType(TypeInfo instanceType, out TypeInfo returnType)
        {
            returnType = instanceType;
            if (typeof(IProxyTargetAccessor).IsAssignableFrom(instanceType))
            {
                returnType = instanceType.BaseType.GetTypeInfo();
            }

            if (returnType == null)
                return true;
            return false;
        }
    }
}