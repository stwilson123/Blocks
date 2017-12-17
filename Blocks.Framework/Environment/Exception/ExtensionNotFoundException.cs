using Blocks.Framework.Exceptions;

namespace Blocks.Framework.Environment.Exception
{
    public class ExtensionNotFoundException : BlocksException
    {
        public ExtensionNotFoundException(string message) : base(message)
        {
        }
    }
}