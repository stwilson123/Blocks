using Abp.Dependency;
using Blocks.Framework.Environment.Extensions;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public interface IResourceManifestProvider : ISingletonDependency, IFearue {
        void BuildManifests(ResourceManifestBuilder builder);
    }
}
