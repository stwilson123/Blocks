using Abp;

namespace Blocks.Framework.Exceptions
{
    public class BlocksException : AbpException
    {
        public BlocksException(string message)
            : base(message)
        {

        }
    }
}