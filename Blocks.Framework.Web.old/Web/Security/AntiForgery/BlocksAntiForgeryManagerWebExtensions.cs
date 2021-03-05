using System.Reflection;
using Abp.Reflection;
using Blocks.Framework.Web.Web.HttpMethod;
using Blocks.Web.Security.AntiForgery;

namespace Blocks.Framework.Web.Web.Security.AntiForgery
{
    public static class BlocksAntiForgeryManagerWebExtensions
    {
        public static bool ShouldValidate(
            this IBlocksAntiForgeryManager manager,
            IBlocksAntiForgeryWebConfiguration antiForgeryWebConfiguration,
            MethodInfo methodInfo, 
            HttpVerb httpVerb, 
            bool defaultValue)
        {
            if (!antiForgeryWebConfiguration.IsEnabled)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(ValidateAbpAntiForgeryTokenAttribute), true))
            {
                return true;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableAbpAntiForgeryTokenValidationAttribute>(methodInfo) != null)
            {
                return false;
            }

            if (antiForgeryWebConfiguration.IgnoredHttpVerbs.Contains(httpVerb))
            {
                return false;
            }

            if (methodInfo.DeclaringType?.IsDefined(typeof(ValidateAbpAntiForgeryTokenAttribute), true) ?? false)
            {
                return true;
            }

            return defaultValue;
        }
    }
}
