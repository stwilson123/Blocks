using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class JSLoaderResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            
          
            manifest.DefineScript(ResourceName.requireJS).SetUrl(PathBuilder.BuilderScripts("require.js"),
                PathBuilder.BuilderScripts("require.js")).SetVersion("2.3.5");
   
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
