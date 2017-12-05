using System.Collections.Generic;
using Abp.Dependency;
using Blocks.Framework.Environment.Extensions.Models;

namespace Blocks.Framework.Environment.Extensions.Folders
{
    public interface IExtensionHarvester : ISingletonDependency {
        IEnumerable<ExtensionDescriptor> HarvestExtensions(IEnumerable<string> paths, string extensionType, string manifestName, bool manifestIsOptional);
    }
}