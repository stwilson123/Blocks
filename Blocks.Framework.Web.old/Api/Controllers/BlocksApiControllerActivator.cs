using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Abp.Dependency;

namespace Blocks.Framework.Web.Api.Controllers
{
    /// <summary>
    /// This class is used to use IOC system to create api controllers.
    /// It's used by ASP.NET system.
    /// </summary>
    public class BlocksApiControllerActivator : IHttpControllerActivator
    {
        private readonly IIocResolver _iocResolver;

        public BlocksApiControllerActivator(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = _iocResolver.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}