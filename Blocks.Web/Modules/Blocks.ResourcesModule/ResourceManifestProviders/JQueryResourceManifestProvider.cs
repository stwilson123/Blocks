using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class JQueryResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineScript("jQuery").SetUrl(PathBuilder.BuilderScripts("jquery-1.9.1.min.js"),
                PathBuilder.BuilderScripts("jquery-1.9.1.js")).SetVersion("1.9.1");
            
            
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
