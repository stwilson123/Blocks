using Blocks.Framework.Ioc.Dependency;
using Blocks.Framework.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Utility.Encryption
{
    public class EncryptionUtility : ITransientDependency
    {
        private IUserContext userContext;
        public EncryptionUtility(IUserContext userContext)
        {
            this.userContext = userContext;
        }
        public string Hash(string str)
        {
            return null;        
//return new PasswordHasher().HashPassword(str);
        }
    }
}
