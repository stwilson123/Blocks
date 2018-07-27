using System;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Core
{
    public class BlocksCoreException : BlocksException
    {
        public BlocksCoreException(StringLocal message) : base(message)
        {
        }

        public BlocksCoreException(StringLocal message, Exception innerException) : base(message, innerException)
        {
        }
    }
}