using System.Net;
using System.Reflection;
using System.Web.Mvc;
using Abp.Dependency;
using Abp.Web;
using Abp.Web.Models;
using Blocks.Framework.Web.Mvc.Configuration;
using Blocks.Framework.Web.Mvc.Controllers.Results;
using Blocks.Framework.Web.Mvc.Extensions;
using Blocks.Framework.Web.Mvc.Helpers;
using Blocks.Framework.Web.Web.Helper;
using Blocks.Framework.Web.Web.Result;
using Blocks.Framework.Web.Web.Security.AntiForgery;
using Blocks.Web.Models;
using Blocks.Web.Security.AntiForgery;
using Castle.Core.Logging;

namespace Blocks.Framework.Web.Mvc.Security.AntiForgery
{
    public class BlocksAntiForgeryMvcFilter: IAuthorizationFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IBlocksAntiForgeryManager _blocksAntiForgeryManager;
        private readonly IAbpMvcConfiguration _mvcConfiguration;
        private readonly IBlocksAntiForgeryWebConfiguration _antiForgeryWebConfiguration;

        public BlocksAntiForgeryMvcFilter(
            IBlocksAntiForgeryManager abpAntiForgeryManager, 
            IAbpMvcConfiguration mvcConfiguration,
            IBlocksAntiForgeryWebConfiguration antiForgeryWebConfiguration)
        {
            _blocksAntiForgeryManager = abpAntiForgeryManager;
            _mvcConfiguration = mvcConfiguration;
            _antiForgeryWebConfiguration = antiForgeryWebConfiguration;
            Logger = NullLogger.Instance;
        }

        public void OnAuthorization(AuthorizationContext context)
        {
            var methodInfo = context.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            var httpVerb = HttpVerbHelper.Create(context.HttpContext.Request.HttpMethod);
            if (!_blocksAntiForgeryManager.ShouldValidate(_antiForgeryWebConfiguration, methodInfo, httpVerb, _mvcConfiguration.IsAutomaticAntiForgeryValidationEnabled))
            {
                return;
            }

            if (!_blocksAntiForgeryManager.IsValid(context.HttpContext))
            {
                CreateErrorResponse(context, methodInfo, "Empty or invalid anti forgery header token.");
            }
        }

        private void CreateErrorResponse(
            AuthorizationContext context, 
            MethodInfo methodInfo, 
            string message)
        {
            Logger.Warn(message);
            Logger.Warn("Requested URI: " + context.HttpContext.Request.Url);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.StatusDescription = message;

            var isJsonResult = MethodInfoHelper.IsJsonResult(methodInfo);

            if (isJsonResult)
            {
                context.Result = CreateUnAuthorizedJsonResult(message);
            }
            else
            {
                context.Result = CreateUnAuthorizedNonJsonResult(context, message);
            }

            if (isJsonResult || context.HttpContext.Request.IsAjaxRequest())
            {
                context.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
        }

        protected virtual AbpJsonResult CreateUnAuthorizedJsonResult(string message)
        {
            return new AbpJsonResult(new AjaxResponse(new ErrorInfo(message), true))
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected virtual HttpStatusCodeResult CreateUnAuthorizedNonJsonResult(AuthorizationContext filterContext, string message)
        {
            return new HttpStatusCodeResult(filterContext.HttpContext.Response.StatusCode, message);
        }
    }
}
