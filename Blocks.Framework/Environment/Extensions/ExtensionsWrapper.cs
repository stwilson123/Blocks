using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks.Framework.Environment.Extensions
{
    public class ExtensionsWrapper : IExtensionsWrapper
    {
        readonly IExtensionManager _extensionManager;
        public ExtensionsWrapper(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
        }
        public IEnumerable<ExtensionInfo> AvailableExtensions()
        {
            return _extensionManager.AvailableExtensions().Select(t => new ExtensionInfo() { Name = t.Name });

        }
    }
}
