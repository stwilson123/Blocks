using Blocks.Framework.Exceptions;

namespace Blocks.Framework.Configurations
{
    public class ConfigurationException : BlocksException
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}