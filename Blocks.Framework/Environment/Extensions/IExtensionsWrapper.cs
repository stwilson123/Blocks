using Blocks.Framework.Ioc.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Environment.Extensions
{
    public interface IExtensionsWrapper : ISingletonDependency
    {
        IEnumerable<ExtensionInfo> AvailableExtensions();
    }



    public class ExtensionInfo
    {
        public string Name { get; set; }
    }
}
