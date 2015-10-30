using System;

namespace Common
{
    public class ConfigurationException : ApplicationException
    {
        public ConfigurationException()
        {

        }

        public ConfigurationException(string message)
        : base(message)
        {
        }

        public ConfigurationException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
