using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.License
{
    public class LicenseException : BlocksException
    {
        public LicenseException(StringLocal message) : base(message)
        {
        }
    }
}
