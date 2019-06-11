using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;
using Abp.Web.Models;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Web.Mvc.Helpers;
using Blocks.Framework.Web.Mvc.Route;
using Blocks.Framework.Web.Result;
using Blocks.Framework.Exceptions;
using Abp.Runtime.Validation;
using Abp.Domain.Entities;
using Newtonsoft.Json;
using Abp.Web.Mvc.Models;
using Blocks.Framework.Localization;
using Blocks.Framework.Logging;
using Castle.Core.Logging;

namespace Blocks.Framework.Web.Mvc.Filters
{
    public class BlocksWebMvcExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly INavigationManager _navigationManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContext _userContext;
        private IErrorInfoBuilder _errorInfoBuilder;

        public ILogger Logger { get; set; }
        protected ILocalizationContext _localizationContext { get; }

        public BlocksWebMvcExceptionFilter(IErrorInfoBuilder errorInfoBuilder, IAuthorizationService authorizationService, IUserContext userContext, ILocalizationContext localizationContext
            )
        {
            this._errorInfoBuilder = errorInfoBuilder;
            this._authorizationService = authorizationService;
            this._userContext = userContext;
            _localizationContext = localizationContext;

        }

        public void OnException(ExceptionContext filterContext)
        {
            var context = filterContext;
            
            LogHelper.LogException(Logger, filterContext.Exception);
            if (context.Exception is HttpException)
            {
                var httpException = context.Exception as HttpException;
                var httpStatusCode = (HttpStatusCode)httpException.GetHttpCode();
                context.HttpContext.Response.StatusCode = (int)httpStatusCode;
                context.Result = false//MethodInfoHelper.IsJsonResult(context.Result)
                    ? GenerateJsonExceptionResult(context)
                    : GenerateNonJsonExceptionResult(context);
            }
            else
            {
                var bEx = context.Exception is BlocksException ? (BlocksException)context.Exception : null;
                context.HttpContext.Response.StatusCode = (int)GetStatusCode(filterContext);
                context.Result = true //MethodInfoHelper.IsJsonResult(context.Result)
                   ? GenerateJsonExceptionResult(context)
                   : GenerateNonJsonExceptionResult(context);

            }

            filterContext.ExceptionHandled = true;
        }
        protected virtual HttpStatusCode GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is Abp.Authorization.AbpAuthorizationException)
            {
                return _userContext.GetCurrentUser().UserId == null
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            if (context.Exception is AbpValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }
            if (context.Exception is BlocksException)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.InternalServerError;
        }
        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            if(!context.HttpContext.Items.Contains("IgnoreJsonRequestBehaviorDenyGet"))
                context.HttpContext.Items.Add("IgnoreJsonRequestBehaviorDenyGet", "true");
            var msg = context.Exception is BlocksException ?
               ((BlocksException)context.Exception)?.LMessage?.Localize(_localizationContext) : context.Exception.Message;
            var result = new DataResult()
            {
                code = "101",
                msg = msg,
                error = new Blocks.Framework.Web.Web.Result.ErrorInfo(101, msg),
                success = false,
                UnAuthorizedRequest = context.Exception is AbpAuthorizationException
            };
            context.HttpContext.Response.ContentType = "application/json";

            return new JsonResult
            {
                Data = result
            };
        }

        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(_errorInfoBuilder.BuildForException(context.Exception), context.Exception)),
                TempData = context.Controller.TempData
            };
        }

    }

}