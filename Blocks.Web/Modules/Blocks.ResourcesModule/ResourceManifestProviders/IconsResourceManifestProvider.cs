using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class IconsResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineStyle(ResourceName.material_icons).SetUrl("fonts/fonts/material-icons/materialicons.css",
                "fonts/fonts/material-icons/materialicons.css").SetVersion("0.5");
            
            
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
