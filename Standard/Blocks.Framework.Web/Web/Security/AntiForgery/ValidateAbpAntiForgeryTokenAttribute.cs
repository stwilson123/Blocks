using System;

namespace Blocks.Framework.Web.Web.Security.AntiForgery
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class ValidateAbpAntiForgeryTokenAttribute : Attribute
    {

    }
}