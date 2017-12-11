using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Environment.Extensions
{
    public interface IFearue : IDependency
    {
        FeatureDescriptor Feature { get; }
    }
}