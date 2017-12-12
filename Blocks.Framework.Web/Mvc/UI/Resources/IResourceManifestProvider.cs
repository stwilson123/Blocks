using System;
using Abp.Dependency;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public interface IResourceManifestProvider : ISingletonDependency , IFeature{
        void BuildManifests(ResourceManifestBuilder builder);
       
    }
}
