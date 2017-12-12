using System.Collections.Generic;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Environment.Extensions.Folders
{
    public interface IExtensionHarvester : ISingletonDependency {
        IEnumerable<ExtensionDescriptor> HarvestExtensions(IEnumerable<string> paths, string extensionType, string manifestName, bool manifestIsOptional);
    }
}