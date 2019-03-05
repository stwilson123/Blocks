using Blocks.Framework.Security;

namespace EntityFramework.Test.Model
{
    public class TestUserContext : IUserContext
    {
        public IUserIdentifier GetCurrentUser()
        {
            return UserIdentifier.CreateNull();
        }
    }
}