using System.Collections.Generic;
using Abp.Dependency;
using Blocks.Framework.Environment.Extensions.Models;

namespace Blocks.Framework.Environment.Extensions.Folders
{
    public interface IExtensionFolders  {
        IEnumerable<ExtensionDescriptor> AvailableExtensions();
    }
}