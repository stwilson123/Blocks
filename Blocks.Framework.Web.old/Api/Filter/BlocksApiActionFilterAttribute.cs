﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Web.Result;

namespace Blocks.Framework.Web.Api.Filter
{
    public class BlocksApiActionFilterAttribute : ActionFilterAttribute, ITransientDependency
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var response = actionExecutedContext.Response;
            object resultObject;


            if (actionExecutedContext.ActionContext.ActionDescriptor.ReturnType == typeof(HttpResponseMessage))
                return;
            if (response != null && actionExecutedContext.Exception == null )
            {
                response.TryGetContentValue(out resultObject);
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    response.StatusCode == HttpStatusCode.NoContent && resultObject == null ? HttpStatusCode.OK : response.StatusCode,
                    new DataResult()
                    {
                        code = Result.ResultCode.Success,
                        content = resultObject,
                        //   msg = string.format(bex?.message.formatstr,bex?.message.formatargs),
                    }
                );

            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}