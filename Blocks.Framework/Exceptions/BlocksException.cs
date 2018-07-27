using System;
using Abp;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Exceptions
{
    public class BlocksException : AbpException
    {
        public string Code { protected set; get; }
        public StringLocal Message { protected set; get; }
        public object Content { protected set; get; }
        public BlocksException(StringLocal message)
            : base(message.ToString())
        {
            Message = message;
        }
        public BlocksException(StringLocal message,Exception innerException)
            : base(message.ToString(),innerException)
        {
            Message = message;
        }
    }


    public class BlocksBussnessException : BlocksException
    {
        public BlocksBussnessException(string code,StringLocal message,object content):base(message)
        {
            Code = code;
            Content = content;
        }
    }
}