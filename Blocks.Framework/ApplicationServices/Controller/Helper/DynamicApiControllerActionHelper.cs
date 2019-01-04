﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blocks.Framework.ApplicationServices.Controller.Helper
{
    internal static class DynamicApiControllerActionHelper
    {
        public static IEnumerable<MethodInfo> GetMethodsOfType(Type type) 
        {
            var allMethods = new List<MethodInfo>();

            FillMethodsRecursively(type, BindingFlags.Public | BindingFlags.Instance, allMethods);

            return allMethods.Where(
                method => method.DeclaringType != typeof (object) &&
                        //  method.DeclaringType != typeof (IgnoreT) &&
                          !IsPropertyAccessor(method)
            );
        }

        public static bool IsMethodOfType(MethodInfo methodInfo, Type type)
        {
            if (type.IsAssignableFrom(methodInfo.DeclaringType))
            {
                return true;
            }

            if (!type.IsInterface)
            {
                return false;
            }

            return type.GetInterfaces().Any(interfaceType => IsMethodOfType(methodInfo, interfaceType));
        }

        private static void FillMethodsRecursively(Type type, BindingFlags flags, List<MethodInfo> members)
        {
            members.AddRange(type.GetMethods(flags));

            foreach (var interfaceType in type.GetInterfaces())
            {
                FillMethodsRecursively(interfaceType, flags, members);
            }
        }

        private static bool IsPropertyAccessor(MethodInfo method)
        {
            return method.IsSpecialName && (method.Attributes & MethodAttributes.HideBySig) != 0;
        }
    }
}