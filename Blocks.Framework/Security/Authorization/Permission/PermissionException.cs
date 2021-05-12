using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public class PermissionException : BlocksException
    {
        public PermissionException(StringLocal message) : base(message)
        {
        }
    }
}
