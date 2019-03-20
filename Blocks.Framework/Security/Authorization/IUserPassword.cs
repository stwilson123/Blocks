using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Security.Authorization
{
    public interface IUserPassword
    {
        bool validate(string userAccount,string password);

        void test();
    }
}
