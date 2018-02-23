using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Configurations
{
    public class ConfigurationException : BlocksException
    {
        public ConfigurationException(StringLocal message) : base(message)
        {
        }
    }
}