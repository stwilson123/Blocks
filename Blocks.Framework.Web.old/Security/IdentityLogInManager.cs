using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.Web.Security
{
    public class IdentityLogInManager : Ioc.Dependency.ITransientDependency
    {
        private readonly Framework.Security.Authorization.IUserPassword userPassword;
        private readonly Framework.Security.Authorization.IDentityUserStore dentityUserStore;

        public IdentityLogInManager(Framework.Security.Authorization.IUserPassword userPassword, Framework.Security.Authorization.IDentityUserStore dentityUserStore)
        {
            this.userPassword = userPassword;
            this.dentityUserStore = dentityUserStore;
        }

        public Task<LogInResult> LoginAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var userIdentity = dentityUserStore.GetUser(usernameOrEmailAddress);
            var logInResult = new LogInResult()
            {
                     Result = LoginResultType.Success,
                     User = userIdentity.ToIdentity()
            };
            if(!userPassword.validate(usernameOrEmailAddress, password))
            {
                logInResult.Result = LoginResultType.InvalidPassword;
            }

            dentityUserStore.CheckUserStatus(userIdentity);
            if(string.IsNullOrEmpty(logInResult.User.Id))
            {
                logInResult.Result = LoginResultType.InvalidUserNameOrEmailAddress;

            }
            return Task.FromResult(logInResult);
        }

    }

    public static class UserTransfer
    {
        public static IdentityUser ToIdentity(this Framework.Security.UserIdentifier userIdentifier)
        {
            return new IdentityUser(userIdentifier.UserId, userIdentifier.UserAccount,userIdentifier.RoleIds);
        }
    }


    public class LogInResult
    {
        public LoginResultType Result { get; set; }
        
        public IdentityUser User { get; set; }
        public System.Security.Claims.ClaimsIdentity Identity { get; set; }
    }

    public enum LoginResultType : byte
    {
        Success = 1,

        InvalidUserNameOrEmailAddress,

        InvalidPassword,

        //UserIsNotActive,

        //InvalidTenancyName,

        //TenantIsNotActive,

        //UserEmailIsNotConfirmed,

        //UnknownExternalLogin,

        //LockedOut,

        //UserPhoneNumberIsNotConfirmed,
    }
}
