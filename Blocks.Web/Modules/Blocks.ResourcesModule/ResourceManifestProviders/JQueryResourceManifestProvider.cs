using System;
using Blocks.Framework.Environment.Extensions.Models;
using Blocks.Framework.Web.Mvc.UI.Resources;

namespace Blocks.ResourcesModule.ResourceManifestProviders {
    public class JQueryResourceManifestProvider : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // jQuery.
            manifest.DefineScript(ResourceName.jquery).SetUrl(PathBuilder.BuilderScripts("jquery-1.9.1.min.js"),
                PathBuilder.BuilderScripts("jquery-1.9.1.js")).SetVersion("1.9.1").SetAMD();
            
            
            manifest.DefineScript(ResourceName.jquery_validate).SetUrl("lib/jquery-validation/dist/jquery.validate.js",
                "lib/jquery-validation/dist/jquery.validate.js").SetVersion("1.9.1").SetAMD();
            manifest.DefineScript(ResourceName.jquery_cookies).SetUrl(PathBuilder.BuilderScripts("jquery.cookie.js"),
                PathBuilder.BuilderScripts("jquery.cookie-1.4.1.min.js")).SetDependencies(ResourceName.jquery).SetVersion("1.4.1");
            
            
        }

        public Lazy<FeatureDescriptor> Feature { get; set; }
    }
}
