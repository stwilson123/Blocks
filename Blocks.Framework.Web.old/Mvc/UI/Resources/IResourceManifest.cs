using System.Collections.Generic;
using Blocks.Framework.Environment.Extensions;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public interface IResourceManifest{
        ResourceDefinition DefineResource(string resourceType, string resourceName);
        string Name { get; }
        string BasePath { get; }
        IDictionary<string, ResourceDefinition> GetResources(string resourceType);
    }
}
