using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Localization
{
    public class LocalizationException : BlocksException
    {
        public LocalizationException(StringLocal message) : base(message)
        {
        }
    }
}