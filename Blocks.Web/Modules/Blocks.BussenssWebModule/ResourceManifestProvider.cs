 

using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.BussenssWebModule {
    public class ResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineScript("jQuery").SetUrl("jquery.min.js", "jquery.js").SetVersion("2.1.4").SetCdn(
                "//ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.4.min.js",
                "//ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.4.js", true);
           
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
