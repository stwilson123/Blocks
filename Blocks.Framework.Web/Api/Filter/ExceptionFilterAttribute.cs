﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Logging;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Abp.Web.Models;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers;
using Abp.WebApi.ExceptionHandling;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Web.Result;
using Castle.Core.Logging;
using ITransientDependency = Blocks.Framework.Ioc.Dependency.ITransientDependency;


namespace Blocks.Framework.Web.Api.Filter
{
    /// <summary>
    /// Used to handle exceptions on web api controllers.
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        /// <summary>
        /// Reference to the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to the <see cref="IEventBus"/>.
        /// </summary>
     //   public IEventBus EventBus { get; set; }

        public IAbpSession AbpSession { get; set; }

        protected IAbpWebApiConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpApiExceptionFilterAttribute"/> class.
        /// </summary>
        public ApiExceptionFilterAttribute(IAbpWebApiConfiguration configuration)
        {
            Configuration = configuration;
            Logger = NullLogger.Instance;
          //  EventBus = NullEventBus.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var wrapResultAttribute = HttpActionDescriptorHelper
                .GetWrapResultAttributeOrNull(context.ActionContext.ActionDescriptor) ??
                Configuration.DefaultWrapResultAttribute;

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }

            if (IsIgnoredUrl(context.Request.RequestUri))
            {

                    return;
            }

            if (context.Exception is HttpException)
            {
                var httpException = context.Exception as HttpException;
                var httpStatusCode = (HttpStatusCode) httpException.GetHttpCode();

                context.Response = context.Request.CreateResponse(
                    httpStatusCode,
                    new AjaxResponse(
                        new ErrorInfo(httpException.Message),
                        httpStatusCode == HttpStatusCode.Unauthorized || httpStatusCode == HttpStatusCode.Forbidden
                    )
                );
            }
            else
            {
                var bEx = context.Exception is BlocksException ? (BlocksException) context.Exception : null;

                context.Response = context.Request.CreateResponse(
                    GetStatusCode(context),
                    new DataResult()
                    {
                        code = bEx?.Code,
                        content = bEx?.Content,
                        msg =  bEx?.Message?.ToString(),
                        Error = SingletonDependency<IErrorInfoBuilder>.Instance.BuildForException(context.Exception),
                        UnAuthorizedRequest = context.Exception is Abp.Authorization.AbpAuthorizationException,
                        
                    }
                );
            }

          //  EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
        }

        protected virtual HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception is Abp.Authorization.AbpAuthorizationException)
            {
                return AbpSession.UserId.HasValue
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

        protected virtual bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || string.IsNullOrEmpty(uri.AbsolutePath))
            {
                return false;
            }

            return Configuration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}