using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Environment.Exception
{
    public class ExtensionNotFoundException : BlocksException
    {
        public ExtensionNotFoundException(StringLocal message) : base(message)
        {
        }
    }
}