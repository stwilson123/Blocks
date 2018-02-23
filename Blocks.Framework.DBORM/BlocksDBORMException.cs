using Blocks.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blocks.Framework.Localization;

namespace Blocks.Framework.DBORM
{
    public class BlocksDBORMException : BlocksException
    {
        public BlocksDBORMException(StringLocal message) : base(message)
        {
        }
    }
}
