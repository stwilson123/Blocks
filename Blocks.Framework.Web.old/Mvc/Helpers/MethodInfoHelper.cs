using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blocks.Framework.Web.Mvc.Helpers
{
    internal static class MethodInfoHelper
    {
        public static bool IsJsonResult(MethodInfo method)
        {
            return typeof(JsonResult).IsAssignableFrom(method.ReturnType) ||
                   typeof(Task<JsonResult>).IsAssignableFrom(method.ReturnType);
        }

        public static bool IsJsonResult(ActionResult action)
        {
            return typeof(JsonResult).IsAssignableFrom(action.GetType()) ||
                   typeof(Task<JsonResult>).IsAssignableFrom(action.GetType());
        }
    }
}
