using System.Reflection;

namespace Blocks.Framework.Web.Api.Controllers.Dynamic.Builders
{
    public interface IDynamicApiControllerBuilder
    {
        IApiControllerBuilder<T> For<T>(string serviceName);

        IBatchApiControllerBuilder<T> ForAll<T>(Assembly assembly, string servicePrefix);
    }
}