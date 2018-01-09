using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule {
    public class BootStrapResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineStyle("bootstrap").SetUrl("jquery-1.9.1.min.js", "jquery-1.9.1.js").SetVersion("1.9.1");

        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
