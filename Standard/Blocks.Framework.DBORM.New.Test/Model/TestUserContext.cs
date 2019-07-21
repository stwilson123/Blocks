using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Security;

namespace EntityFramework.Test.Model
{
    public class TestUserContext : IUserContext,ISingletonDependency
    {
        public IUserIdentifier GetCurrentUser()
        {
            return UserIdentifier.CreateNull();
        }
    }
}