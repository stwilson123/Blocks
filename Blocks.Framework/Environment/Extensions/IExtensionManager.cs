using System.Collections.Generic;
using Abp.Application.Features;
using Abp.Dependency;
using Blocks.Framework.Environment.Extensions.Models;

namespace Blocks.Framework.Environment.Extensions
{
    public interface IExtensionManager : ISingletonDependency {
        IEnumerable<ExtensionDescriptor> AvailableExtensions();
        IEnumerable<FeatureDescriptor> AvailableFeatures();

        ExtensionDescriptor GetExtension(string id);

        //IEnumerable<Feature> LoadFeatures(IEnumerable<FeatureDescriptor> featureDescriptors);
    }
}