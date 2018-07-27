namespace Blocks.Framework.Security
{
    public interface IAuthenticationService
    {
        void SignIn(IUserIdentifier user);
        void SignOut();
        IUserIdentifier GetAuthenticatedUser();
    }
}