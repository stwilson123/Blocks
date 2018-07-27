using System.Reflection;
using Blocks.Framework.ApplicationServices.Controller;
using Blocks.Framework.ApplicationServices.Filters;
using Blocks.Framework.Web.Web.HttpMethod;

namespace Blocks.Framework.Web.Mvc.Controllers
{
    public class MvcControllerActionInfo : DefaultControllerActionInfo
    {
        /// <summary>
        /// The HTTP verb that is used to call this action.
        /// </summary>
        public HttpVerb Verb { get; private set; }
        
        public MvcControllerActionInfo(string actionName, HttpVerb verb, MethodInfo method, IFilter[] filters = null, bool? isApiExplorerEnabled = null) : base(actionName, method, filters, isApiExplorerEnabled)
        {

            this.Verb = verb;
        }
    }
}