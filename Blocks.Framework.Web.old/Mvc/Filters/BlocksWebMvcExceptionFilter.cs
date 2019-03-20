using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Navigation.Manager;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization;
using Blocks.Framework.Security.Authorization.Permission;
using Blocks.Framework.Web.Mvc.Route;

namespace Blocks.Framework.Web.Mvc.Filters
{
    public class BlocksWebMvcExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly INavigationManager _navigationManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContext _userContext;


        public BlocksWebMvcExceptionFilter(INavigationManager navigationManager, IAuthorizationService authorizationService,IUserContext userContext
            )
        {
            this._navigationManager = navigationManager;
            this._authorizationService = authorizationService;
            this._userContext = userContext;
        }

        public void OnException(ExceptionContext filterContext)
        {
            var context = filterContext;
            //if (context.Exception is HttpException)
            //{
            //    var httpException = context.Exception as HttpException;
            //    var httpStatusCode = (HttpStatusCode)httpException.GetHttpCode();

            //    context.Response = context.Request.CreateResponse(
            //        httpStatusCode,
            //        new DataResult()
            //        {
            //            code = ResultCode.Fail,
            //            msg = httpException.Message,
            //            Error = new ErrorInfo(httpException.Message),
            //            UnAuthorizedRequest = httpStatusCode == HttpStatusCode.Unauthorized ||
            //                                  httpStatusCode == HttpStatusCode.Forbidden
            //        }
            //    );
            //}
            //else
            //{
            //    var bEx = context.Exception is BlocksException ? (BlocksException)context.Exception : null;

            //    context.Response = context.Request.CreateResponse(
            //        GetStatusCode(context),
            //        new DataResult()
            //        {
            //            code = bEx?.Code ?? ResultCode.Fail,
            //            content = bEx?.Content,
            //            msg = bEx?.Message?.ToString() ?? bEx?.LMessage?.Localize(_localizationContext) ?? context.Exception.Message,
            //            Error = SingletonDependency<IErrorInfoBuilder>.Instance.BuildForException(context.Exception),
            //            UnAuthorizedRequest = context.Exception is Abp.Authorization.AbpAuthorizationException,

            //        }
            //    );
            //}
        }
    }
     
}