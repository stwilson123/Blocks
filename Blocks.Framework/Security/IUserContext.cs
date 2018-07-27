using System;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security
{
    public interface IUserContext : ISingletonDependency
    {
        IUserIdentifier GetCurrentUser();
    }
}