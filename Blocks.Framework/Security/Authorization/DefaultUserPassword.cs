using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Security.Authorization
{
    public class DefaultUserPassword : IUserPassword, Ioc.Dependency.ITransientDependency
    {
        public virtual void test()
        {

        }
         

        public virtual bool validate(string userAccount, string password)
        {
            return true;
        }


         
    }
}
