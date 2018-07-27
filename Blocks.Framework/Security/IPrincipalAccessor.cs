using System.Security.Claims;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security
{
    public interface IPrincipalAccessor : ISingletonDependency
    {
        ClaimsPrincipal Principal { get; }
    }
}