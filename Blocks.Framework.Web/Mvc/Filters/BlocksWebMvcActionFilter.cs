using System.Web.Mvc;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Web.Mvc.Filters
{
    public class BlocksWebMvcActionFilter : IActionFilter, ITransientDependency
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
             
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
           // throw new System.NotImplementedException();
        }
    }
}