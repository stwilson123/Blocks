using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Blocks.Framework.Web.Api.Validation
{
    public static class ActionDescriptorExtensions
    {
        public static MethodInfo GetMethodInfoOrNull(this HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedHttpActionDescriptor)
            {
                return actionDescriptor.As<ReflectedHttpActionDescriptor>().MethodInfo;
            }

            return null;
        }

        public static bool IsDynamicAbpAction(this HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor
                .ControllerDescriptor
                .Properties
                .ContainsKey("__AbpDynamicApiControllerInfo");
        }
    }
}
