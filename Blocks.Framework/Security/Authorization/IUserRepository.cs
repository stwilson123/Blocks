using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Security.Authorization
{
    public interface IDentityUserStore : Ioc.Dependency.ITransientDependency
    {
        UserIdentifier GetUser(string UserAccount);

    }
}
