using Blocks.Framework.Domain.Uow;
using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Web.Api.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Blocks.Framework.Web.Api.Uow
{
    public class BlocksApiUowFilter : IActionFilter, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
     //   private readonly IAbpWebApiConfiguration _webApiConfiguration;
        private readonly IUnitOfWorkDefaultOptions _unitOfWorkDefaultOptions;

        public bool AllowMultiple => false;

        public BlocksApiUowFilter(
            IUnitOfWorkManager unitOfWorkManager,
           // IAbpWebApiConfiguration webApiConfiguration,
            IUnitOfWorkDefaultOptions unitOfWorkDefaultOptions)
        {
            _unitOfWorkManager = unitOfWorkManager;
           // _webApiConfiguration = webApiConfiguration;
            _unitOfWorkDefaultOptions = unitOfWorkDefaultOptions;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicAbpAction())
            {
                return await continuation();
            }

            //var unitOfWorkAttr = _unitOfWorkDefaultOptions.GetUnitOfWorkAttributeOrNull(methodInfo) ??
            //                     _webApiConfiguration.DefaultUnitOfWorkAttribute;
            var unitOfWorkAttr = _unitOfWorkDefaultOptions.GetUnitOfWorkAttributeOrNull(methodInfo);
            if (unitOfWorkAttr.IsDisabled)
            {
                return await continuation();
            }

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                var result = await continuation();
                await uow.CompleteAsync();
                return result;
            }
        }
    }
}
