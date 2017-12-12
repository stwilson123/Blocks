using System;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Ioc.Dependency;

namespace Blocks.Framework.Web.Mvc.UI.Resources {
    public interface IResourceManifestProvider : ISingletonDependency , IFeature{
        void BuildManifests(ResourceManifestBuilder builder);
       
    }
}
