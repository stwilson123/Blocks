using System;

namespace Blocks.Framework.Localization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class LocalizedDescriptionAttribute : Attribute
    {
        public string Name { get; }

        public LocalizedDescriptionAttribute(string name)
        {
            Name = name;
        }
        
    }
}