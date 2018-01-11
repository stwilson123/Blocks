using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class JQueryResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineScript(ResourceName.jQuery).SetUrl(PathBuilder.BuilderScripts("jquery-1.9.1.min.js"),
                PathBuilder.BuilderScripts("jquery-1.9.1.js")).SetVersion("1.9.1");
            
            
            manifest.DefineScript(ResourceName.jquery_validate).SetUrl("lib/jquery-validation/dist/jquery.validate.js",
                "lib/jquery-validation/dist/jquery.validate.js").SetVersion("1.9.1");
           
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
