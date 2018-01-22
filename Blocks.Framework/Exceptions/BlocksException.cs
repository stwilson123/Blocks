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


    public class BlocksBussnessException : AbpException
    {
        public BlocksBussnessException(string code,string message)
            : base(message)
        {

        }
    }
}