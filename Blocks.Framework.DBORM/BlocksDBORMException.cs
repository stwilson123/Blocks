using Blocks.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Framework.DBORM
{
    public class BlocksDBORMException : BlocksException
    {
        public BlocksDBORMException(string message) : base(message)
        {
        }
    }
}
