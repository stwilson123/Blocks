using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Web.Mvc.Controllers.Results;
using Blocks.Framework.Web.Mvc.Extensions;
using Blocks.Framework.Web.Mvc.Helpers;
using Blocks.Framework.Web.Mvc.Route;

namespace Blocks.Framework.Web.Mvc.Filters
{
    public class BlocksWebMvcAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        private readonly INavigationManager _navigationManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContext _userContext;
        private readonly IAuditingHelper _auditingHelper;


        public BlocksWebMvcAuthorizeFilter(INavigationManager navigationManager,
            IAuthorizationService authorizationService, IUserContext userContext, IAuditingHelper auditingHelper
        )
        {
            this._navigationManager = navigationManager;
            this._authorizationService = authorizationService;
            this._userContext = userContext;
            _auditingHelper = auditingHelper;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
            var methodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            if (_userContext.GetCurrentUser() == null &&
                _auditingHelper.ShouldSaveAudit(methodInfo))
                HandleUnauthorizedRequest(filterContext, methodInfo)
                    ;
        }


        protected virtual void HandleUnauthorizedRequest(
            AuthorizationContext filterContext,
            MethodInfo methodInfo
            // AbpAuthorizationException ex
        )
        {
            filterContext.HttpContext.Response.StatusCode =
                filterContext.RequestContext.HttpContext.User?.Identity?.IsAuthenticated ?? false
                    ? (int) HttpStatusCode.Forbidden
                    : (int) HttpStatusCode.Unauthorized;

            var isJsonResult = MethodInfoHelper.IsJsonResult(methodInfo);

            if (isJsonResult)
            {
                //filterContext.Result = CreateUnAuthorizedJsonResult(ex);
                filterContext.Result = new AbpJsonResult();
            }
            else
            {
               // filterContext.Result = CreateUnAuthorizedNonJsonResult(filterContext, ex);
                filterContext.Result = new HttpUnauthorizedResult();

            }

            if (isJsonResult || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
            //_eventBus.Trigger(this, new AbpHandledExceptionData(ex));
        }
        
  

    }
}