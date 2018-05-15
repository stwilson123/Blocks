using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.FileSystems.VirtualPath
{
    public class VirtualPathException: BlocksException
    {
        public VirtualPathException(StringLocal message) : base(message)
        {
        }
    }
}