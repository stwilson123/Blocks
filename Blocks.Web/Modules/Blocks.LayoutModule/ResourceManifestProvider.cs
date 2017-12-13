using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.LayoutModule {
    public class ResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            
            manifest.DefineResource("template","_LayoutRoot").SetUrl("_LayoutRoot.cshtml", "_LayoutRoot.cshtml").SetVersion("0.1");
            manifest.DefineResource("template","_LayoutFirst").SetUrl("_LayoutFirst.cshtml", "_LayoutFirst.cshtml").SetVersion("0.1");

        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
