using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.FileSystems
{
    public class FileSystemsException : BlocksException
    {
        public FileSystemsException(StringLocal message) : base(message)
        {
        }
    }
}