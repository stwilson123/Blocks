using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Environment.Extensions
{
    public interface IFeature : IDependency
    {
        Lazy<FeatureDescriptor> Feature { get; }
    }
}