using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Security.Authorization
{
    public class BlocksAuthorizeException : BlocksException
    {
        public BlocksAuthorizeException(StringLocal message) : base(message)
        {
        }
    }
}