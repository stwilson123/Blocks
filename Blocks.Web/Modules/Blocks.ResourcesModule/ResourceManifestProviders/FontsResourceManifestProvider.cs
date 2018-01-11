using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class FontsResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineStyle(ResourceName.roboto).SetUrl("fonts/fonts/roboto/roboto.css",
                "fonts/fonts/roboto/roboto.css").SetVersion("0.5");
            
            manifest.DefineStyle(ResourceName.font_awesome).SetUrl("lib/font-awesome/css/font-awesome.min.css",
                "lib/font-awesome/css/font-awesome.css").SetVersion("0.5");
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
