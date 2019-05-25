using System;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Security
{
    public interface IUserContext : IUnitOfWorkDependency
    {
        IUserIdentifier GetCurrentUser();
    }
}